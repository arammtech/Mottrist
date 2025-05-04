using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Domain.Common.IUnitOfWork;
using AutoMapper;
using Mottrist.Domain.Global;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Mottrist.Utilities.Identity;
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Domain.LookupEntities;
using Microsoft.EntityFrameworkCore;
using Mottrist.Service.Features.General;
using System.Linq.Expressions;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Cars.Interfaces;
using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;
using static Mottrist.Utilities.Global.GlobalFunctions;
using Mottrist.Domain.Enums;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Languages.Interfaces;
using Mottrist.Service.Features.Countries.Interfaces;
using Mottrist.Service.Features.Cities.Interfaces;
using AutoMapper.QueryableExtensions;
using Mottrist.Service.Features.General.Images.Interface;

namespace Mottrist.Service.Features.Drivers.Services
{
    /// <summary>
    /// Provides services for managing driver-related operations, including user management,
    /// car details handling, and database transactions.
    /// </summary>
    public class DriverService : BaseService, IDriverService
    {

        #region Dependencies

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ICarService _carService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILanguageService _languageService;

        private readonly ICountryService _countryService;

        private readonly ICityService _cityService;
        private readonly IImageService _imageService;

        #endregion

        public DriverService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICarService carService,
            UserManager<ApplicationUser> userManager,
            ILanguageService languageService,
            ICountryService countryService,
            ICityService cityService,
            IImageService imageService)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _languageService = languageService;
            _countryService = countryService;
            _cityService = cityService;
            _imageService = imageService;
            _carService = carService;
        }

        #region Driver Get Operations
        public async Task<DataResult<DriverDto>?> GetAllAsync(Expression<Func<Driver, bool>>? filter = null)
        {
            try
            {
                var driverQuery = _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                        .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions)
                    .AsQueryable();
                    

                // Apply filter if provided
                if (filter != null)
                {
                    driverQuery = driverQuery.Where(filter);
                }

               var drivers = await driverQuery.ProjectTo<DriverDto>(_mapper.ConfigurationProvider).ToListAsync();

                return new DataResult<DriverDto>
                {
                    Data = drivers.Any() ? drivers : Enumerable.Empty<DriverDto>()
                };
            }
            catch (Exception)
            {
                // Log the exception for debugging
                return null;

            }
        }

        public async Task<DataResult<DriverDto>?> GetTopRatedAsync(int topCount = 3)
        {
            try
            {
                var driverQuery = _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                        .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions) // Include interactions for aggregation
                    .AsQueryable();

                // Aggregate likes from DriverInteractions
                var topDrivers = await driverQuery
                    .Select(driver => new
                    {
                        Driver = driver,
                        TotalLikes = driver.DriverInteractions.Count(interaction => interaction.IsLiked == true) // Count likes
                    })
                    .OrderByDescending(x => x.TotalLikes) // Order by most likes
                    .Take(topCount) // Get top N drivers
                    .Select(x => x.Driver) // Extract Driver entity
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new DataResult<DriverDto>
                {
                    Data = topDrivers.Any() ? topDrivers : Enumerable.Empty<DriverDto>()
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DriverDto?> GetByIdAsync(int driverId)
        {
            if (driverId <= 0)
                return null;

            try
            {
                // Fetch all required details in a single database request
                var driverDetails = await _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                            .ThenInclude(x=> x.Language)
                    .Include(x => x.DriverInteractions)
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == driverId);

                // Check if driver exists
                if (driverDetails == null)
                    return null;

                return _mapper.Map<DriverDto>(driverDetails);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginatedResult<DriverDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Driver, bool>>? filter = null)
        {
            try
            {

                var driverQuery = _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                            .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions)
                    .AsQueryable();

                // Apply filter if provided
                if (filter != null)
                {
                    driverQuery = driverQuery.Where(filter);
                }

                // Calculate total count for pagination metadata
                var totalRecordsCount = await driverQuery.CountAsync();

                // Apply pagination (skip and take)
                var paginatedDrivers = await driverQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Create paginated result object
                PaginatedResult<DriverDto> paginatedDriverDto = new PaginatedResult<DriverDto>()
                {
                    Data = paginatedDrivers,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = paginatedDrivers.Count,
                    TotalRecordsCount = totalRecordsCount
                };

                return paginatedDriverDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<DriverDto?> GetAsync(Expression<Func<Driver, bool>> filter)
        {
            // Validate the filter
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "The filter expression cannot be null.");
            }

            try
            {
                // Fetch the driver using the filter
                var driver = await _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                            .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions)
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                // Return the result
                return driver;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<DriverFormFieldsDto?> GetAllDriverFormFields()
        {
            try
            {
                DriverFormFieldsDto driverFormFieldsDto = new DriverFormFieldsDto
                {
                    CarFieldsDto = await _carService.GetAllCarFieldsAsync(),
                    Languages = await _languageService.GetAllAsync(),
                    Countries = await _countryService.GetAllAsync(),
                    Cities = await _cityService.GetAllAsync()

                };

                return driverFormFieldsDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DataResult<DriverDto>?> GetByCountryAsync(int countryId)
        {
            return await _GetByLocationAndDateAsync(countryId);
        }

        public async Task<DataResult<DriverDto>?> GetByCountryAndCityAsync(int countryId, int cityId)
        {
            return await _GetByLocationAndDateAsync(countryId, cityId);
        }
        public async Task<DataResult<DriverDto>?> GetByCountryCityAndDateAsync(int countryId,int cityId, DateTime date)
        {
            return await _GetByLocationAndDateAsync(countryId, cityId,date);
        }

        public async Task<PaginatedResult<DriverDto>?> GetByCountryWithPaginationAsync(
            int countryId,
            int page = 1,
            int pageSize = 10)
        {
            return await _GetByLocationAndDateWithPaginationAsync(countryId,page:page,pageSize:pageSize);
        }

        public async Task<PaginatedResult<DriverDto>?> GetByCountryAndCityWithPaginationAsync(
            int countryId,
            int cityId,
            int page = 1,
            int pageSize = 10)
        {
            
            return await _GetByLocationAndDateWithPaginationAsync(countryId,cityId, page: page, pageSize: pageSize);
        }
        public async Task<PaginatedResult<DriverDto>?> GetByCountryCityAndDateWithPaginationAsync(
            int countryId,
            int cityId,
            DateTime date,
            int page = 1,
            int pageSize = 10)
        {
            return await _GetByLocationAndDateWithPaginationAsync(countryId,cityId,date, page: page, pageSize: pageSize);
        }
     
        private async Task<DataResult<DriverDto>?> _GetByLocationAndDateAsync(
            int countryId,
            int? cityId = null,
            DateTime? date = null)
        {
            // Validate parameters: countryId is required and must be > 0;
            // if cityId is provided, it must also be > 0.
            if (countryId < 1 || (cityId.HasValue && cityId < 1))
            {
                return null;
            }

            try
            {
                // Build the base query with required includes and no-tracking for performance.
                var query = _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                            .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions)
                    .AsQueryable();

                // Filter by country using the driver's associated countries and "CoverNow" status.
                query = query.Where(d => d.DriverCountries
                                         .Any(dc => dc.Country.Id == countryId && dc.WorkStatus == WorkStatus.CoverNow));

                // If a city ID is provided, filter by driver's associated cities with "CoverNow" status.
                if (cityId.HasValue)
                {
                    query = query.Where(d => d.DriverCities.Any(dc => dc.City.Id == cityId && dc.WorkStatus == WorkStatus.CoverNow));
                }

                // If a date is provided, include drivers available during that date or marked as available all time.
                if (date.HasValue)
                {
                    query = query.Where(d =>
                        (d.AvailableFrom.HasValue && d.AvailableFrom.Value <= date.Value &&
                         d.AvailableTo.HasValue && d.AvailableTo.Value >= date.Value)
                         || d.IsAvailableAllTime);
                }
                else
                {
                    query = query.Where(d => d.IsAvailableAllTime);
                }

                var drivers = await query
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x=> x.AvailableFrom)
                    .ToListAsync();

                // Return the result with a fallback to an empty list if no drivers are found.
                return new DataResult<DriverDto>
                {
                    Data = drivers.Any() ? drivers : new List<DriverDto>()
                };
            }
            catch (Exception ex)
            {
                // Optionally log the exception (ex)
                return null;
            }
        }

        private async Task<PaginatedResult<DriverDto>?> _GetByLocationAndDateWithPaginationAsync(
            int countryId,
            int? cityId = null,
            DateTime? date = null,
            int page = 1,
            int pageSize = 10)
        {

            try
            {
                // Build the base query with required includes and no-tracking for performance.
                var query = _unitOfWork.Repository<Driver>().Table
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Country)
                    .Include(x => x.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(x => x.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(x => x.DriverCountries)
                           .ThenInclude(x => x.Country)
                    .Include(x => x.DriverLanguages)
                            .ThenInclude(x => x.Language)
                    .Include(x => x.DriverInteractions)
                    .AsQueryable();

                // Filter by country using the driver's associated countries with "CoverNow" status.
                query = query.Where(d => d.DriverCountries.Any(dc => dc.Country.Id == countryId && dc.WorkStatus == WorkStatus.CoverNow));

                // If a city ID is provided, filter by driver's associated cities with "CoverNow" status.
                if (cityId.HasValue)
                {
                    query = query.Where(d => d.DriverCities.Any(dc => dc.City.Id == cityId && dc.WorkStatus == WorkStatus.CoverNow));
                }

                // If a date is provided, include drivers available during that date or marked as available all time.
                if (date.HasValue)
                {
                    query = query.Where(d =>
                        (d.AvailableFrom.HasValue && d.AvailableFrom.Value <= date.Value &&
                         d.AvailableTo.HasValue && d.AvailableTo.Value >= date.Value)
                         || d.IsAvailableAllTime);
                }
                else
                {
                    query = query.Where(d => d.IsAvailableAllTime);
                }

                // Get total records count for pagination metadata
                var totalRecords = await query.CountAsync();

                // Apply pagination
                var paginatedDrivers = await query
                    .OrderBy(x => x.AvailableFrom)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return paginated result with metadata
                return new PaginatedResult<DriverDto>
                {
                    Data = paginatedDrivers.Any() ? paginatedDrivers : new List<DriverDto>(),
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalRecordsCount = totalRecords
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region Driver Addition Operations
        public async Task<Result<DriverDto>> AddAsync(AddDriverDto driverDto)
        {
            await _unitOfWork.StartTransactionAsync();

            try
            {

                // Create user and assign driver role
                var user = _mapper.Map<ApplicationUser>(driverDto);
                var userResult = await _AddUserAsync(driverDto, user);

                if (!userResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<DriverDto>.Failure("failed to save user");
                }

                var roleResult = await _AssignUserRoleAsync(user, AppUserRoles.RoleDriver);
                if (!roleResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<DriverDto>.Failure("failed to  user's role");
                }


                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id;

                // Process all driver-related images
                await _ProcessDriverImagesAsync(driverDto, driverEntity);


                driverEntity.IsAvailableAllTime = !(driverDto.AvailableFrom.HasValue && driverDto.AvailableTo.HasValue);
     
                // Add driver entity to the repository
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var saveDriverResult = await _unitOfWork.SaveChangesAsync();
                if (!saveDriverResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<DriverDto>.Failure("Failed to save the driver details.");
                }

                ////// Add all associations in one batch

                // Add car details if HasCar is true
                if (driverDto.HasCar)
                {
                    var carResult = await _AddCarAsync(driverDto, driverEntity);
                    if (!carResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<DriverDto>.Failure("Failed to save the car details.");
                    }
                }

               await _unitOfWork.CommitAsync();

                return Result<DriverDto>.Success(await GetByIdAsync(driverEntity.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<DriverDto>.Failure($"An unexpected error occurred: {ex.Message}");
            }
        }

        #region User Addition Operations
        private async Task<Result> _AddUserAsync(AddDriverDto driverDto, ApplicationUser user)
        {
            // Attempt to create the user
            var addUserResult = await _userManager.CreateAsync(user, driverDto.Password);
            if (!addUserResult.Succeeded)
            {
                // Extract and combine error codes into a single error message
                var errors = addUserResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors));
            }

            return Result.Success();
        }
        private async Task<Result> _AssignUserRoleAsync(ApplicationUser user, string role)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                // Extract and combine error codes into a single error message
                var errors = roleResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors));
            }

            return Result.Success();
        }
        #endregion

        #region Car Addition Operations
        private async Task<Result> _AddCarAsync(AddDriverDto driverDto, Driver driverEntity)
        {
            if(driverDto.Car == null)
            {
                return Result.Failure("Car details are required.");
            }

            // Add the car via the CarService.
            var carAddResult = await _carService.AddAsync(driverDto.Car);

            // Update the driver's CarId with the new car's id.
            driverEntity.CarId = carAddResult.Data?.Id;

            return Result.Success();
        }

        #endregion

        #endregion

        #region Driver Image Processing Operations
        private async Task _ProcessDriverImagesAsync(AddDriverDto driverDto, Driver driver)
        {
            // Process profile image.
            if (driverDto.ProfileImage != null)
            {
                driver.ProfileImageUrl = await _imageService.SaveImageAsync(driverDto.ProfileImage, ImageCategory.Profiles);
            }
            

            // Process license image.
            if (driverDto.LicenseImage != null)
            {
                driver.LicenseImageUrl = await _imageService.SaveImageAsync(driverDto.LicenseImage, ImageCategory.Documents) ?? throw new ArgumentNullException();
            }

            // Process passport image.
            if (driverDto.PassportImage != null)
            {
                driver.PassportImageUrl = await _imageService.SaveImageAsync(driverDto.PassportImage, ImageCategory.Documents) ?? throw new ArgumentNullException();
            }
        }

        #endregion

        #region Driver Update Operations
        public async Task<Result<DriverDto>> UpdateAsync(UpdateDriverDto updateDriverDto)
        {
            var result = await _unitOfWork.StartTransactionAsync();
            if(!result.IsSuccess)
            {
                return Result<DriverDto>.Failure("Failed to start transaction.");
            }
            try
            {
                // Validate the existence of the driver.
                var existingDriver = await _unitOfWork.Repository<Driver>().Table
                    .Include(x=> x.DriverCities)
                    .Include(x => x.DriverCountries)
                    .Include(x => x.DriverLanguages)
                    .FirstOrDefaultAsync(d => d.Id == updateDriverDto.Id);

                if (existingDriver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<DriverDto>.Failure("Driver not found.");
                }

                var imageUpdateResult = await _UpdateProfileImageAsync(updateDriverDto, existingDriver);
                string? savedImageUrl = existingDriver?.ProfileImageUrl;
                if (!imageUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<DriverDto>.Failure("Image not saved.");
                }
                // Step 3: Update driver associations (cities, countries, languages).
                if (existingDriver.DriverCities.Any())
                {
                    await _unitOfWork.Repository<DriverCity>().DeleteRangeAsync(existingDriver.DriverCities);
                }

                if (existingDriver.DriverCountries.Any())
                {
                    await _unitOfWork.Repository<DriverCountry>().DeleteRangeAsync(existingDriver.DriverCountries);
                }

                if (existingDriver.DriverLanguages.Any())
                {
                    await _unitOfWork.Repository<DriverLanguage>().DeleteRangeAsync(existingDriver.DriverLanguages);
                }

                // Step 1: Update driver details.
                _mapper.Map(updateDriverDto, existingDriver);

                // Step 2: Update associated user details.
                var userUpdateResult = await _UpdateUserDetailsAsync(updateDriverDto, existingDriver.UserId);
                if (!userUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    await _imageService.DeleteImageAsync(savedImageUrl);
                    return Result<DriverDto>.Failure($"Failed to update user details: {userUpdateResult.Errors.FirstOrDefault()}");
                }

                existingDriver.IsAvailableAllTime = !(updateDriverDto.AvailableFrom.HasValue && updateDriverDto.AvailableTo.HasValue);

                // Step 5: Save updated driver details.
                await _unitOfWork.Repository<Driver>().UpdateAsync(existingDriver);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    await _imageService.DeleteImageAsync(savedImageUrl);
                    return Result<DriverDto>.Failure("Failed to save driver updates.");
                }

                if(updateDriverDto.Car != null)
                {
                    var carUpdateResult = await _UpdateOrAddCarDetailsAsync(updateDriverDto.Car, existingDriver);

                    if (!carUpdateResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        await _imageService.DeleteImageAsync(savedImageUrl);
                        return Result<DriverDto>.Failure($"Failed to update car details: {carUpdateResult.Errors.FirstOrDefault()}");
                    }
                }

                // Commit the transaction.
                var resultCommit =  await _unitOfWork.CommitAsync();
                if (!resultCommit.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    await _imageService.DeleteImageAsync(savedImageUrl);
                    return Result<DriverDto>.Failure("Failed to commit transaction.");
                }

                return Result<DriverDto>.Success(await GetByIdAsync(updateDriverDto.Id));
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any exception.
                await _unitOfWork.RollbackAsync();
                return Result<DriverDto>.Failure($"Unexpected error occurred during driver update: {ex.Message}");
            }
        }

        private async Task<Result> _UpdateProfileImageAsync(UpdateDriverDto driverDto, Driver existingDriver)
        {
            if(driverDto.ProfileImage != null)
            {
                existingDriver.ProfileImageUrl = await _imageService.ReplaceImageAsync(driverDto.ProfileImage, existingDriver.ProfileImageUrl, ImageCategory.Profiles);
            }

            return Result.Success();
        }
        public async Task<Result> UpdateAvailabilityAsync(int driverId, DateTime? availableFrom, DateTime? availableTo, bool availableAllTime)
        {
            // Validate driver ID
            if (driverId < 1)
                return Result.Failure("Invalid driver ID.");

            try
            {
                // Retrieve the driver entity from the database
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(x => x.Id == driverId);
                if (driver == null)
                    return Result.Failure("Driver not found.");

                // Update availability fields (ensuring only the date portion is stored)
                driver.AvailableFrom = availableFrom?.Date;
                driver.AvailableTo = availableTo?.Date;
                driver.IsAvailableAllTime = availableAllTime && !(availableTo.HasValue && availableFrom.HasValue);

                // Save changes
                await _unitOfWork.Repository<Driver>().UpdateAsync(driver);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                return saveResult ?? Result.Failure("Failed to save changes.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error occurred: {ex.Message}");
            }
        }
        public async Task<Result> UpdatePriceAsync(int driverId, decimal newPricePerHour)
        {
            // Validate parameters
            if (driverId < 1 || newPricePerHour <= 0)
                return Result.Failure("Invalid driver ID or price.");

            try
            {
                // Retrieve the driver from the database
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(x => x.Id == driverId);
                if (driver == null)
                    return Result.Failure("Driver not found.");

                // Update the price per hour
                driver.PricePerHour = newPricePerHour;

                // Save changes
                await _unitOfWork.Repository<Driver>().UpdateAsync(driver);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                return saveResult ?? Result.Failure("Failed to save price update.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error updating price: {ex.Message}");
            }
        }

        #region Update Helper Functions

        private async Task _DeleteSavedImageAsync(string? imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    // Execute synchronous deletion without blocking async flow
                    _imageService.DeleteImage(imageUrl);
                }
                catch (Exception ex)
                {
                    // Handle failure gracefully
                   // Console.WriteLine($"Error deleting image: {ex.Message}");
                }
            }

            await Task.CompletedTask; // Maintain async method structure
        }

        private async Task<Result> _UpdateUserDetailsAsync(UpdateDriverDto driverDto, int userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString());
            if (existingUser == null)
            {
                return Result.Failure("Associated user not found.");
            }

            _mapper.Map(driverDto, existingUser);
            existingUser.Id = userId; // Ensure user ID consistency.
            var userUpdateResult = await _userManager.UpdateAsync(existingUser);
            if (!userUpdateResult.Succeeded)
            {
                return Result.Failure("Failed to update user details.");
            }

            return Result.Success();
        }

        private async Task<Result> _UpdateOrAddCarDetailsAsync(UpdateCarDto? updateCarDto, Driver existingDriver)
        {
            try
            {
                if (existingDriver.CarId.HasValue && updateCarDto != null)
                {
                    var carUpdateResult = await _carService.UpdateAsync(updateCarDto, existingDriver.CarId.Value);
                    if (!carUpdateResult.IsSuccess)
                    {
                        return Result.Failure("Failed to update car details.");
                    }
                }
                else
                {
                    var carDto = _mapper.Map<AddCarDto>(updateCarDto);

                    var carAddResult = await _carService.AddAsync(carDto);
                    if (!carAddResult.IsSuccess)
                    {
                        return Result.Failure("Failed to add car details.");
                    }
                    existingDriver.CarId = carAddResult.Data?.Id;
                    existingDriver.Status = DriverStatus.Pending;

                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating or adding car details: {ex.Message}");
            }
        }

        #endregion

        #endregion

        #region Driver Deletion Operations
        /// <summary>
        /// Deletes a driver by their ID, along with associated car details (if applicable).
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver to be deleted.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating whether the operation was successful or failed.
        /// </returns>
        public async Task<Result> DeleteAsync(int driverId)
        {
            try
            {
                //// Start a transaction for atomicity.
                //var transaction = await _unitOfWork.StartTransactionAsync();
                //if (!transaction.IsSuccess)
                //    return Result.Failure("Failed to start the transaction.");

                // Retrieve the driver.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null)
                {
                    //await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                //// Delete associated car and its images.
                //var carDeletionResult = await _DeleteCarAndImagesAsync(driver);
                //if (!carDeletionResult.IsSuccess)
                //{
                //    await _unitOfWork.RollbackAsync();
                //    return carDeletionResult;
                //}


                //// Delete the driver record.
                //var associatetionDeletionResult = await _DeleteDriverAssociationsAsync(driverId);
                //if (!associatetionDeletionResult.IsSuccess)
                //{
                //    await _unitOfWork.RollbackAsync();
                //    return associatetionDeletionResult;
                //}


                // Delete the driver record.
                var driverDeletionResult = await _DeleteDriverRecordAsync(driver);
                if (!driverDeletionResult.IsSuccess)
                {
                    //await _unitOfWork.RollbackAsync();
                    return driverDeletionResult;
                }

                //// Delete the associated user.
                //var userDeletionResult = await _DeleteAssociatedUserAsync(driver);
                //if (!userDeletionResult.IsSuccess)
                //{
                //    await _unitOfWork.RollbackAsync();
                //    return userDeletionResult;
                //}

                // Commit the transaction if all operations succeed.
                //var commitResult = await _unitOfWork.SaveChangesAsync();
                //if (!commitResult.IsSuccess)
                //{
                //    //await _unitOfWork.RollbackAsync();
                //    return Result.Failure("Failed to commit the transaction.");
                //}

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback the transaction on error.
                //await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error deleting driver: {ex.Message}");
            }
        }

        #region Deletion Helper Functions

        /// <summary>
        /// Deletes all associations for a specified driver, including languages, cities (worked on and currently covered), 
        /// and countries (worked on and currently covered).
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver whose associations need to be deleted.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating success or failure:
        /// - Success: All associations for the specified driver have been deleted.
        /// - Failure: An error occurred while attempting to delete the associations, with an error message.
        /// </returns>
        /// <remarks>
        /// This method processes multiple types of driver associations:
        /// 1. Languages associated with the driver.
        /// 2. Cities that the driver has worked in or currently covers.
        /// 3. Countries that the driver has worked in or currently covers.
        /// 
        /// Each category is deleted individually, and database changes are committed after each deletion.
        /// If any deletion fails, the method returns a failure result with a descriptive error message.
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code>
        /// var result = await _DeleteDriverAssociationsAsync(1234);
        /// if (result.IsSuccess)
        /// {
        ///     Console.WriteLine("Associations deleted successfully.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine($"Failed to delete associations: {result.ErrorMessage}");
        /// }
        /// </code>
        /// </example>

        private async Task<Result> _DeleteDriverAssociationsAsync(
    int driverId)
        {
            try
            {
                // --- Languages ---
                var existingLanguages = await _unitOfWork.Repository<DriverLanguage>().GetAllAsync(dl => dl.DriverId == driverId);
                if (existingLanguages != null && existingLanguages.Any())
                {
                    await _unitOfWork.Repository<DriverLanguage>().DeleteRangeAsync(existingLanguages);
                    var deleteLangResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteLangResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete driver languages.");
                    }
                }

                // --- Cities Worked On ---
                var existingWorkedOnCities = await _unitOfWork.Repository<DriverCity>()
                    .GetAllAsync(dc => dc.DriverId == driverId && dc.WorkStatus == WorkStatus.WorkedOn);
                if (existingWorkedOnCities != null && existingWorkedOnCities.Any())
                {
                    await _unitOfWork.Repository<DriverCity>().DeleteRangeAsync(existingWorkedOnCities);
                    var deleteWorkedOnCitiesResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteWorkedOnCitiesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete worked-on cities for the driver.");
                    }
                }

                // --- Cities Cover Now ---
                var existingCoverNowCities = await _unitOfWork.Repository<DriverCity>()
                    .GetAllAsync(dc => dc.DriverId == driverId && dc.WorkStatus == WorkStatus.CoverNow);
                if (existingCoverNowCities != null && existingCoverNowCities.Any())
                {
                    await _unitOfWork.Repository<DriverCity>().DeleteRangeAsync(existingCoverNowCities);
                    var deleteCoverNowCitiesResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteCoverNowCitiesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete currently covered cities for the driver.");
                    }
                }

                // --- Countries Worked On ---
                var existingWorkedOnCountries = await _unitOfWork.Repository<DriverCountry>()
                    .GetAllAsync(dc => dc.DriverId == driverId && dc.WorkStatus == WorkStatus.WorkedOn);
                if (existingWorkedOnCountries != null && existingWorkedOnCountries.Any())
                {
                    await _unitOfWork.Repository<DriverCountry>().DeleteRangeAsync(existingWorkedOnCountries);
                    var deleteWorkedOnCountriesResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteWorkedOnCountriesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete worked-on countries for the driver.");
                    }
                }

                // --- Countries Cover Now ---
                var existingCoverNowCountries = await _unitOfWork.Repository<DriverCountry>()
                    .GetAllAsync(dc => dc.DriverId == driverId && dc.WorkStatus == WorkStatus.CoverNow);
                if (existingCoverNowCountries != null && existingCoverNowCountries.Any())
                {
                    await _unitOfWork.Repository<DriverCountry>().DeleteRangeAsync(existingCoverNowCountries);
                    var deleteCoverNowCountriesResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteCoverNowCountriesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete currently covered countries for the driver.");
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to delete driver associations: {ex.Message}");
            }
        }


        /// <summary>
        /// Deletes the car and its associated images, if they exist, for the specified driver.
        /// </summary>
        private async Task<Result> _DeleteCarAndImagesAsync(Driver driver)
        {
            if (!driver.CarId.HasValue)
                return Result.Success();

            var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == driver.CarId.Value);
            if (car != null)
            {
                // Delete associated car images.
                var carImages = await _unitOfWork.Repository<CarImage>().GetAllAsync(ci => ci.CarId == car.Id);
                if (carImages?.Any() == true)
                {
                    await _unitOfWork.Repository<CarImage>().DeleteRangeAsync(carImages);
                    var carImagesResult = await _unitOfWork.SaveChangesAsync();
                    if (!carImagesResult.IsSuccess)
                        return Result.Failure("Failed to delete associated car images.");
                }

                // Delete the car.
                await _unitOfWork.Repository<Car>().DeleteAsync(car);
                var carSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!carSaveResult.IsSuccess)
                    return Result.Failure("Failed to delete associated car details.");
            }

            return Result.Success();
        }

        /// <summary>
        /// Deletes the driver record from the repository.
        /// </summary>
        private async Task<Result> _DeleteDriverRecordAsync(Driver driver)
        {
            await _unitOfWork.Repository<Driver>().DeleteAsync(driver);
            var driverSaveResult = await _unitOfWork.SaveChangesAsync();
            if (!driverSaveResult.IsSuccess)
                return Result.Failure("Failed to delete the driver.");

            return Result.Success();
        }

        /// <summary>
        /// Deletes the associated user from the identity system.
        /// </summary>
        private async Task<Result> _DeleteAssociatedUserAsync(Driver driver)
        {
            var user = await _userManager.FindByIdAsync(driver.UserId.ToString());
            var userDeletionResult = await _userManager.DeleteAsync(user);
            if (!userDeletionResult.Succeeded)
                return Result.Failure("Failed to delete associated user.");

            return Result.Success();
        }

        #endregion

        #endregion

        #region Driver Status Operations

        /// <summary>
        /// Updates the status of a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver to update.</param>
        /// <param name="newStatus">The new status to assign to the driver.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> UpdateStatusAsync(int driverId, DriverStatus newStatus)
        {
            try
            {
                // Validate the driver exists
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null)
                {
                    return Result.Failure("Driver not found.");
                }

                // Update the driver status
                driver.Status = newStatus;
                await _unitOfWork.Repository<Driver>().UpdateAsync(driver);

                // Commit the changes
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    return Result.Failure("Failed to update driver status.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return Result.Failure($"An error occurred while updating driver status: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the current status of a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <returns>
        /// The current <see cref="DriverStatus"/> of the driver, or null if the driver does not exist.
        /// </returns>
        public async Task<DriverStatus?> GetStatusAsync(int driverId)
        {
            try
            {
                // Fetch the driver
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null)
                {
                    return null;
                }

                return driver.Status;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving driver status: {ex.Message}", ex);
            }
        }

        #endregion

        #region Existence Checks

        /// <summary>
        /// Checks whether a driver exists in the database by their unique identifier.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver. Must be greater than 0.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// Returns <c>true</c> if the driver exists; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// (Optional) Can be thrown if <paramref name="driverId"/> is less than or equal to 0.
        /// </exception>
        /// <remarks>
        /// This method queries the database asynchronously to determine the existence of the driver.
        /// </remarks>
        public async Task<bool> DoesExistByIdAsync(int driverId, CancellationToken cancellationToken = default)
        {
            // Validate input parameter.
            if (driverId <= 0)
            {
                // You could throw an exception here if desired:
                // throw new ArgumentException("Driver ID must be greater than zero.", nameof(driverId));
                return false;
            }

            try
            {
                // Query the driver repository to determine if any driver has the given ID.
                return await _unitOfWork.Repository<Driver>()
                                        .Table
                                        .AnyAsync(x => x.Id == driverId, cancellationToken);
            }
            catch (Exception)
            {
                // Optionally log the exception here.
                return false;
            }
        }

        /// <summary>
        /// Asynchronously checks whether a user with the specified email exists,
        /// ensuring email uniqueness across all user types managed by the identity system.
        /// </summary>
        /// <param name="email">The email address to search for in the user table.</param>
        /// <param name="cancellationToken">
        /// A token that can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is <c>true</c> if a user
        /// with the specified email exists; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> DoesExistByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            // Validate input parameter.
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Check for the user's existence using _userManager.
                var user = await _userManager.FindByEmailAsync(email);
                return user != null;
            }
            catch (Exception)
            {
                // Optionally log the exception here.
                return false;
            }
        }

        #endregion

        #region Car Image Operations

        /// <summary>
        /// Adds a new car image for a driver's car.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be added.</param>
        /// <param name="imageFile">The image file to be uploaded and added.</param>
        /// <param name="isMain">Specifies whether the new image should be marked as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> AddCarImageAsync(int driverId, IFormFile imageFile, bool isMain = false)
        {
            if (driverId <= 0)
            {
                return Result.Failure("Driver ID must be greater than zero.");
            }

            if (imageFile == null || imageFile.Length <= 0)
            {
                return Result.Failure("Image file cannot be null or empty.");
            }

            try
            {
                // Retrieve the driver and verify the car association.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null || driver.CarId == null)
                {
                    return Result.Failure("Driver does not have an associated car.");
                }

                // Save the image file and get its URL.
                var imageUrl = await SaveImageAsync(imageFile, "car-images");

                // Create a DTO for the car image addition.
                var carImageDto = new CarImageDto
                {
                    CarId = driver.CarId.Value,
                    ImageUrl = imageUrl,
                    IsMain = isMain
                };

                // Add the car image via CarService.
                var result = await _carService.AddCarImageAsync(carImageDto);
                if (!result.IsSuccess)
                {
                    return Result.Failure($"Failed to add car image. Reason: {result.Errors?.FirstOrDefault()}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific car image associated with a driver's car.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be deleted.</param>
        /// <param name="imageUrl">The URL of the image to be deleted.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> DeleteCarImageAsync(int driverId, string imageUrl)
        {
            if (driverId <= 0)
            {
                return Result.Failure("Driver ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Result.Failure("Image URL cannot be null or empty.");
            }

            try
            {
                // Retrieve the driver to get the associated car ID.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null || driver.CarId == null)
                {
                    return Result.Failure("Driver does not have an associated car.");
                }

                // Remove the car image via CarService.
                var result = await _carService.RemoveCarImageAsync(imageUrl, driver.CarId.Value);
                if (!result.IsSuccess)
                {
                    return Result.Failure($"Failed to delete car image. Reason: {result.Errors?.FirstOrDefault()}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets a specific car image as the main image for a driver's car.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be set as the main image.</param>
        /// <param name="imageUrl">The URL of the image to set as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> SetMainCarImageAsync(int driverId, string imageUrl)
        {
            if (driverId <= 0)
            {
                return Result.Failure("Driver ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Result.Failure("Image URL cannot be null or empty.");
            }

            try
            {
                // Retrieve the driver and verify the associated car.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null || driver.CarId == null)
                {
                    return Result.Failure("Driver does not have an associated car.");
                }

                // Set the specified image as the main image.
                var result = await _carService.SetMainImageAsync(driver.CarId.Value, imageUrl);
                if (!result.IsSuccess)
                {
                    return Result.Failure($"Failed to set main car image. Reason: {result.Errors?.FirstOrDefault()}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a car image by either replacing the image file or updating metadata.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be updated.</param>
        /// <param name="imageUrl">Optional: The URL of the existing image for metadata update.</param>
        /// <param name="newImageFile">Optional: A new image file to replace the existing image.</param>
        /// <param name="isMain">Specifies whether the image should be marked as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> UpdateCarImageAsync(int driverId, string? imageUrl, IFormFile? newImageFile, bool isMain)
        {
            if (driverId <= 0)
            {
                return Result.Failure("Driver ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl) && newImageFile == null)
            {
                return Result.Failure("Either imageUrl or newImageFile must be provided.");
            }

            try
            {
                // Retrieve the driver to verify the associated car.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null || driver.CarId == null)
                {
                    return Result.Failure("Driver does not have an associated car.");
                }

                // If an image URL is provided, update the metadata.
                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    var result = await _carService.UpdateImageAsync(driver.CarId.Value, imageUrl, null, isMain);
                    if (!result.IsSuccess)
                    {
                        return Result.Failure($"Failed to update car image metadata. Reason: {result.Errors?.FirstOrDefault()}");
                    }
                }

                // If a new image file is provided, replace the current image.
                if (newImageFile != null)
                {
                    var result = await _carService.UpdateImageAsync(driver.CarId.Value, null, newImageFile, isMain);
                    if (!result.IsSuccess)
                    {
                        return Result.Failure($"Failed to replace car image. Reason: {result.Errors?.FirstOrDefault()}");
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred: {ex.Message}");
            }
        }


        #endregion

        #region Driver Interaction Operations
        /// <summary>
        /// Updates the like/dislike status for a driver by a logged-in user.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user making the reaction.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="isLiked">
        /// The reaction type: 
        /// - `true` for Like.
        /// - `false` for Dislike.
        /// - `null` to remove the reaction.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the update was successful.
        /// - HTTP 200 OK if the reaction is updated successfully.
        /// - HTTP 400 Bad Request if the parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        public async Task<Result> LikeOrDislikeAsync(int driverId, int userId, bool? isLiked)
        {
            // Validate input parameters
            if (driverId < 1 || userId < 1)
                return Result.Failure("Invalid driver or user ID.");

            try
            {
                // Fetch existing interaction
                var interaction = await _unitOfWork.Repository<DriverInteraction>()
                    .GetAsync(x => x.DriverId == driverId && x.UserId == userId);

                if (interaction != null)
                {
                    // If the reaction remains unchanged, avoid redundant updates
                    if (interaction.IsLiked == isLiked)
                        return Result.Success();

                    // Update the existing reaction
                    interaction.IsLiked = isLiked;
                   await _unitOfWork.Repository<DriverInteraction>().UpdateAsync(interaction);
                }
                else
                {
                    // Create new interaction entry
                    interaction = new DriverInteraction
                    {
                        DriverId = driverId,
                        UserId = userId,
                        IsLiked = isLiked,
                        ViewsCount = 1

                    };
                    await _unitOfWork.Repository<DriverInteraction>().AddAsync(interaction);
                }


                var result = await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error while updating reaction: {ex.Message}");
            }
        }

        /// <summary>
        /// Records a user's first view of a driver, ensuring each user views a driver only once.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver being viewed.
        /// Must be greater than 0.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user viewing the driver.
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> object indicating whether the view was successfully recorded or if the user has already viewed the driver.
        /// - HTTP 200 OK if the view is recorded successfully.
        /// - HTTP 400 Bad Request if input parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        public async Task<Result> IncrementViewCountAsync(int driverId, int userId)
        {
            // Validate input parameters
            if (driverId < 1 || userId < 1)
                return Result.Failure("Invalid driver or user ID.");

            try
            {
                // Check if the user has already viewed this driver
                var interactionExists = await _unitOfWork.Repository<DriverInteraction>()
                    .Table.AnyAsync(x => x.DriverId == driverId && x.UserId == userId);

                if (interactionExists)
                    return Result.Success();

                // Create new interaction entry for the first view
                var interaction = new DriverInteraction
                {
                    DriverId = driverId,
                    UserId = userId,
                    ViewsCount = 1
                };

                await _unitOfWork.Repository<DriverInteraction>().AddAsync(interaction);
                var result =  await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error updating views: {ex.Message}");
            }
        }


        #endregion
    }
}
