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
using static Mottrist.Service.Features.Drivers.Helpers.UserHelper;
using static Mottrist.Utilities.Global.GlobalFunctions;
using Mottrist.Domain.Enums;
using Mottrist.Service.Features.Cities.Dtos;

namespace Mottrist.Service.Features.Drivers.Services
{
    /// <summary>
    /// Provides services for managing driver-related operations, including user management,
    /// car details handling, and database transactions.
    /// </summary>
    public class DriverService : BaseService, IDriverService
    {
        #region Dependencies

        /// <summary>
        /// Unit of work for managing database transactions and repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Mapper for handling object-to-object mapping.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Service for managing car-related operations.
        /// </summary>
        private readonly ICarService _carService;

        /// <summary>
        /// User manager for handling user-related operations, such as creating and updating users.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for managing database interactions.</param>
        /// <param name="mapper">The mapper instance for object mapping.</param>
        /// <param name="carService">The service for managing car operations.</param>
        /// <param name="userManager">The user manager for handling user-related operations.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the injected dependencies are null.
        /// </exception>
        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, ICarService carService, UserManager<ApplicationUser> userManager)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        }

        #region Driver Get Operations

        /// <summary>
        /// Retrieves a set of drivers with their detailed information, including car and image details, optionally filtered by the specified criteria.
        /// </summary>
        /// <param name="filter">Optional: A filter expression to apply to the query. Defaults to null.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains:
        /// - A <see cref="DataResult{DriverDto}"/> object containing the drivers with their details.
        /// - An empty result if no matching drivers are found.
        /// </returns>
        public async Task<DataResult<DriverDto>?> GetAllAsync(Expression<Func<Driver, bool>>? filter = null)
        {
            try
            {
                // Build the base query with necessary includes
                var driverQuery = _unitOfWork.Repository<Driver>().Query()
                    .Include(d => d.User) // Include related User entity
                    .Include(d => d.Country) // Include related Country entity
                    .Include(d => d.Car) // Include related Car entity
                        .ThenInclude(c => c.CarImages).AsQueryable(); // Include CarImages within Car

                // Apply filter if provided
                if (filter != null)
                {
                    driverQuery = driverQuery.Where(filter);
                }

                // Transform to DriverDto
                var drivers = await driverQuery.Select(driver => new DriverDto
                {
                    Id = driver.Id,
                    WhatsAppNumber = driver.WhatsAppNumber,
                    Nationality = driver.Country.Name ?? "Unknown", // Handle nullable Country
                    LicenseImageUrl = driver.LicenseImageUrl,
                    YearsOfExperience = driver.YearsOfExperience,
                    Bio = driver.Bio,
                    PassportImageUrl = driver.PassportImageUrl,
                    FirstName = driver.User.FirstName,
                    LastName = driver.User.LastName,
                    Email = driver.User.Email ?? string.Empty,
                    PhoneNumber = driver.User.PhoneNumber,
                    ProfileImageUrl = driver.ProfileImageUrl,
                    HasCar = driver.CarId != null,
                    CarBrand = driver.Car.Brand.Name ?? "N/A", // Handle nullable Car
                    CarYear = driver.Car.Year,
                    CarNumberOfSeats = driver.Car.NumberOfSeats,
                    CarModel = driver.Car.Model.Name,
                    CarColor = driver.Car.Color.Name,
                    CarBodyType = driver.Car.BodyType.Type,
                    CarFuelType = driver.Car.FuelType.Type,
                    MainCarImageUrl = driver.Car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl, // Main image
                    AddtionalCarImageUrls = driver.Car.CarImages
                        .Where(ci => !ci.IsMain)
                        .Select(ci => ci.ImageUrl)
                        .ToList() ?? new List<string>() // Additional images
                }).ToListAsync();

                // Return the result
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

        /// <summary>
        /// Retrieves detailed information about a specific driver, including car and nationality details, if applicable.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver.</param>
        /// <returns>
        /// A <see cref="DriverDto"/> object containing the driver's details or null if the driver does not exist.
        /// </returns>
        public async Task<DriverDto?> GetByIdAsync(int driverId)
        {
            if (driverId <= 0)
                return null;

            try
            {
                // Fetch all required details in a single database request
                var driverDetails = await (
                    from driver in _unitOfWork.Repository<Driver>().Table.AsNoTracking()
                    join user in _unitOfWork.Repository<ApplicationUser>().Table
                        on driver.UserId equals user.Id
                    join country in _unitOfWork.Repository<Country>().Table
                        on driver.NationalityId equals country.Id into countryGroup
                    from countryDetails in countryGroup.DefaultIfEmpty()
                    join car in _unitOfWork.Repository<Car>().Table
                        on driver.CarId equals car.Id into carGroup
                    from carDetails in carGroup.DefaultIfEmpty()
                    where driver.Id == driverId
                    select new DriverDto
                    {
                        Id = driver.Id,
                        WhatsAppNumber = driver.WhatsAppNumber,
                        Nationality = countryDetails.Name ?? "Unknown",
                        LicenseImageUrl = driver.LicenseImageUrl,
                        YearsOfExperience = driver.YearsOfExperience,
                        Bio = driver.Bio,
                        PassportImageUrl = driver.PassportImageUrl,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email ?? string.Empty,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImageUrl = driver.ProfileImageUrl,
                        HasCar = driver.CarId.HasValue,
                        CarBrand = carDetails.Brand.Name,
                        CarYear = carDetails.Year,
                        CarNumberOfSeats = carDetails.NumberOfSeats,
                        CarModel = carDetails.Model.Name,
                        CarColor = carDetails.Color.Name,
                        CarBodyType = carDetails.BodyType.Type,
                        CarFuelType = carDetails.FuelType.Type,
                        MainCarImageUrl = carDetails.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                        AddtionalCarImageUrls = carDetails.CarImages
                            .Where(ci => !ci.IsMain)
                            .Select(ci => ci.ImageUrl)
                            .ToList() ?? new List<string>()
                    }
                ).FirstOrDefaultAsync();

                return driverDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of drivers based on the provided parameters.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of records per page. Defaults to 10.</param>
        /// <param name="filter">An optional filter expression to filter drivers.</param>
        /// <returns>
        /// A <see cref="PaginatedResult{DriverDto}"/> object containing the paginated drivers and metadata.
        /// </returns>
        public async Task<PaginatedResult<DriverDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Driver, bool>>? filter = null)
        {
            // Validate input
            if (page <= 0) throw new ArgumentException("Page number must be greater than zero.", nameof(page));
            if (pageSize <= 0) throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

            try
            {


                var driverQuery = from driver in _unitOfWork.Repository<Driver>().Query()
                                  join user in _unitOfWork.Repository<ApplicationUser>().Query()
                                  on driver.UserId equals user.Id
                                  join country in _unitOfWork.Repository<Country>().Query()
                                  on driver.NationalityId equals country.Id into countryGroup
                                  from countryDetails in countryGroup.DefaultIfEmpty()
                                  join car in _unitOfWork.Repository<Car>().Query()
                                  on driver.CarId equals car.Id into carGroup
                                  from carDetails in carGroup.DefaultIfEmpty()
                                  select driver;

                // Apply filter if provided
                if (filter != null)
                {
                    driverQuery = driverQuery.Where(filter);
                }


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
                    .Select(x => new DriverDto
                    {
                        Id = x.Id,
                        WhatsAppNumber = x.WhatsAppNumber,
                        Nationality = x.Country.Name ?? "Unknown",
                        LicenseImageUrl = x.LicenseImageUrl,
                        YearsOfExperience = x.YearsOfExperience,
                        Bio = x.Bio,
                        PassportImageUrl = x.PassportImageUrl,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email ?? string.Empty,
                        PhoneNumber = x.User.PhoneNumber,
                        ProfileImageUrl = x.ProfileImageUrl,
                        HasCar = x.CarId != null,
                        CarBrand = x.Car.Brand.Name ?? string.Empty,
                        CarYear = x.Car.Year,
                        CarNumberOfSeats = x.Car.NumberOfSeats,
                        CarModel = x.Car.Model.Name,
                        CarColor = x.Car.Color.Name,
                        CarBodyType = x.Car.BodyType.Type,
                        CarFuelType = x.Car.FuelType.Type,
                        MainCarImageUrl = x.Car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                        AddtionalCarImageUrls = x.Car.CarImages
                            .Where(ci => !ci.IsMain)
                            .Select(ci => ci.ImageUrl)
                            .ToList() ?? new List<string>()
                    })
                    .ToListAsync();

                // Create paginated result object
                PaginatedResult<DriverDto> paginatedDriverDto = new()
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
        /// <summary>
        /// Retrieves a single driver based on the provided filter expression.
        /// </summary>
        /// <param name="filter">An expression used to filter the driver.</param>
        /// <returns>
        /// A <see cref="DriverDto"/> object containing driver details if a match is found; otherwise, null.
        /// </returns>
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
                var driver = await _unitOfWork.Repository<Driver>().Query()
                    .Where(filter)
                    .Select(x => new DriverDto
                    {
                        Id = x.Id,
                        WhatsAppNumber = x.WhatsAppNumber,
                        Nationality = x.Country.Name ?? "Unknown",
                        LicenseImageUrl = x.LicenseImageUrl,
                        YearsOfExperience = x.YearsOfExperience,
                        Bio = x.Bio,
                        PassportImageUrl = x.PassportImageUrl,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email ?? string.Empty,
                        PhoneNumber = x.User.PhoneNumber,
                        ProfileImageUrl = x.ProfileImageUrl,
                        HasCar = x.CarId != null,
                        CarBrand = x.Car.Brand.Name ?? string.Empty,
                        CarYear = x.Car.Year,
                        CarNumberOfSeats = x.Car.NumberOfSeats,
                        CarModel = x.Car.Model.Name,
                        CarColor = x.Car.Color.Name,
                        CarBodyType = x.Car.BodyType.Type,
                        CarFuelType = x.Car.FuelType.Type,
                        MainCarImageUrl = x.Car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                        AddtionalCarImageUrls = x.Car.CarImages
                            .Where(ci => !ci.IsMain)
                            .Select(ci => ci.ImageUrl)
                            .ToList() ?? new List<string>()
                    })
                    .FirstOrDefaultAsync();

                // Return the result
                return driver;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion

        #region Driver Addition Operations
        /// <summary>
        /// Adds a new driver to the system, including an associated car and its images if applicable.
        /// </summary>
        /// <param name="driverDto">The DTO containing driver, car, and related details.</param>
        /// <returns>A <see cref="Result"/> object indicating the success or failure of the operation.</returns>
        public async Task<Result> AddAsync(AddDriverDto driverDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();
            if (!transactionResult.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.");
            }

            try
            {
                // Process all driver-related images.
                await ProcessDriverImagesAsync(driverDto);

                // Create the user and assign the driver role.
                var user = _mapper.Map<ApplicationUser>(driverDto);
                var userResult = await _AddUserAsync(driverDto, user);
                if (!userResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return userResult;
                }

                var roleResult = await _AssignUserRoleAsync(user, AppUserRoles.RoleDriver);
                if (!roleResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return roleResult;
                }

                // Map the driver DTO to a driver entity and assign the user's id.
                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id;

                // If the driver includes car details, add the car (and associated images) through CarService.
                if (driverDto.HasCar)
                {
                    var carResult = await AddCarWithCarServiceAsync(driverDto, driverEntity);
                    if (!carResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return carResult;
                    }
                }

                // Add the driver entity to the repository.
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var saveDriverResult = await _unitOfWork.SaveChangesAsync();
                if (!saveDriverResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver details.");
                }

                // Commit the transaction.
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to commit the transaction.");
                }

                // Assign the generated driver id back to the DTO.
                driverDto.Id = driverEntity.Id;
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"An unexpected error occurred: {ex.Message}");
            }
        }

        #region User Addition Operations
        /// <summary>
        /// Creates a new user based on the details provided in the driver DTO.
        /// </summary>
        /// <param name="driverDto">The DTO containing the user's details, such as email, password, and name.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the user creation process.
        /// On success, returns <see cref="Result.Success()"/>.
        /// On failure, returns <see cref="Result.Failure(string)"/> containing error details.
        /// </returns>
        private async Task<Result> _AddUserAsync(AddDriverDto driverDto, ApplicationUser user)
        {
            // Map the username from the driver's email
            //user.UserName = driverDto.Email;
            //user.Id = 0;
            // Attempt to create the user
            var addUserResult = await _userManager.CreateAsync(user, user.PasswordHash ?? driverDto.Password);
            if (!addUserResult.Succeeded)
            {
                // Extract and combine error codes into a single error message
                var errors = addUserResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors));
            }

            return Result.Success();
        }

        /// <summary>
        /// Assigns a specified role to a given user.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance for user management.</param>
        /// <param name="user">The user to assign the role to.</param>
        /// <param name="role">The role to assign to the user.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// On success, returns <see cref="Result.Success()"/>.
        /// On failure, returns <see cref="Result.Failure(string)"/> containing error details.
        /// </returns>
        private  async Task<Result> _AssignUserRoleAsync(ApplicationUser user, string role)
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
        /// <summary>
        /// Adds a car to the system using the CarService, along with its images if provided.
        /// </summary>
        /// <param name="driverDto">The DTO containing car details.</param>
        /// <param name="driverEntity">The driver entity to link the car to.</param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        private async Task<Result> AddCarWithCarServiceAsync(AddDriverDto driverDto, Driver driverEntity)
        {
            // Map the driver DTO to a car DTO.
            var carDto = _mapper.Map<AddCarDto>(driverDto);
            carDto.Id = 0; // New car expected.

            // Add the car via the CarService.
            var carAddResult = await _carService.AddAsync(carDto);
            if (!carAddResult.IsSuccess)
            {
                return Result.Failure("Failed to add car details.");
            }

            // Update the driver's CarId with the new car's id.
            driverEntity.CarId = carDto.Id;

            // Process and add each car image if present.
            if (driverDto.CarImagesUrl != null && driverDto.CarImagesUrl.Any())
            {
                for (int i = 0; i < driverDto.CarImagesUrl.Count; i++)
                {
                    var carImageDto = new CarImageDto
                    {
                        CarId = carDto.Id,
                        ImageUrl = driverDto.CarImagesUrl[i],
                        IsMain = (i == driverDto.MainCarImageIndex)
                    };

                    var carImageResult = await _carService.AddCarImageAsync(carImageDto);
                    if (!carImageResult.IsSuccess)
                    {
                        return Result.Failure("Failed to add car images.");
                    }
                }
            }

            return Result.Success();
        }

        #endregion

        #endregion

        #region Driver Image Processing Operations
        /// <summary>
        /// Processes and saves all driver-related images, including profile, license, passport, and car images.
        /// </summary>
        /// <param name="driverDto">The DTO containing the image files.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ProcessDriverImagesAsync(AddDriverDto driverDto)
        {
            // Process profile image.
            if (driverDto.ProfileImage != null)
            {
                driverDto.ProfileImageUrl = await SaveImageAsync(driverDto.ProfileImage, "profiles");
            }

            // Process license image.
            if (driverDto.LicenseImage != null)
            {
                driverDto.LicenseImageUrl = await SaveImageAsync(driverDto.LicenseImage, "licenses");
            }

            // Process passport image.
            if (driverDto.PassportImage != null)
            {
                driverDto.PassportImageUrl = await SaveImageAsync(driverDto.PassportImage, "passports");
            }

            // Process car images.
            if (driverDto.HasCar && driverDto.CarImages != null && driverDto.CarImages.Any())
            {
                var processedCarImages = new List<string>();
                for (int i = 0; i < driverDto.CarImages.Count; i++)
                {
                    var carImage = driverDto.CarImages[i];
                    var savedImageUrl = await SaveImageAsync(carImage, "cars");
                    processedCarImages.Add(savedImageUrl);
                }

                driverDto.CarImagesUrl = processedCarImages;
            }
        }

        #endregion

        #region Driver Update Operations
        /// <summary>
        /// Updates an existing driver in the system, including associated user and car details if applicable.
        /// </summary>
        /// <param name="driverDto">The DTO containing updated driver and car details.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the update operation.
        /// </returns>
        public async Task<Result> UpdateAsync(UpdateDriverDto driverDto)
        {
            // Begin a transaction.
            var transactionResult = await _unitOfWork.StartTransactionAsync();
            if (!transactionResult.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.");
            }

            try
            {
                // Validate the existence of the driver.
                var existingDriver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverDto.Id);
                if (existingDriver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                // Step 1: Update driver details.
                _mapper.Map(driverDto, existingDriver);

                // Step 2: Update associated user details.
                var userUpdateResult = await UpdateUserDetailsAsync(driverDto, existingDriver.UserId);
                if (!userUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure($"Failed to update user details: {userUpdateResult.Errors.FirstOrDefault()}");
                }

                // Step 3: Update or add car details (if applicable).
                if (driverDto.HasCar)
                {
                    var carUpdateResult = await UpdateOrAddCarDetailsAsync(driverDto, existingDriver);
                    if (!carUpdateResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure($"Failed to update car details: {carUpdateResult.Errors.FirstOrDefault()}");
                    }
                }

                // Step 4: Save updated driver details.
                await _unitOfWork.Repository<Driver>().UpdateAsync(existingDriver);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save driver updates.");
                }

                // Commit the transaction.
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    return Result.Failure("Failed to commit the transaction.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any exception.
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Unexpected error occurred during driver update: {ex.Message}");
            }
        }

        #region Update Helper Functions
        /// <summary>
        /// Updates the associated user details of the driver.
        /// </summary>
        /// <param name="driverDto">The driver DTO with updated details.</param>
        /// <param name="userId">The ID of the associated user to update.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
        private async Task<Result> UpdateUserDetailsAsync(UpdateDriverDto driverDto, int userId)
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

        /// <summary>
        /// Updates or adds car details and images for the driver.
        /// </summary>
        /// <param name="driverDto">The driver DTO with updated car details.</param>
        /// <param name="existingDriver">The existing driver entity.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
        private async Task<Result> UpdateOrAddCarDetailsAsync(UpdateDriverDto driverDto, Driver existingDriver)
        {
            try
            {
                if (existingDriver.CarId.HasValue)
                {
                    // Update existing car.
                    var updateCarDto = _mapper.Map<UpdateCarDto>(driverDto);
                    updateCarDto.Id = existingDriver.CarId.Value;
                    var carUpdateResult = await _carService.UpdateAsync(updateCarDto);
                    if (!carUpdateResult.IsSuccess)
                    {
                        return Result.Failure("Failed to update car details.");
                    }
                }
                else
                {
                    var carDto = _mapper.Map<AddCarDto>(driverDto);
                    carDto.Id = 0;

                    var carAddResult = await _carService.AddAsync(carDto);
                    if (!carAddResult.IsSuccess)
                    {
                        return Result.Failure("Failed to add car details.");
                    }
                    existingDriver.CarId = carDto.Id;
                }
                // Process and update the car images.
                var imageResult = await ProcessAndUpdateCarImagesAsync(driverDto, existingDriver.CarId.Value);
                if (!imageResult.IsSuccess)
                {
                    return Result.Failure("Failed to update car images.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating or adding car details: {ex.Message}");
            }
        }

        /// <summary>
        /// Processes and updates car images, including adding or updating the main image.
        /// </summary>
        /// <param name="driverDto">The driver DTO containing car image details.</param>
        /// <param name="carId">The ID of the car associated with the driver.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the operation was successful or not.
        /// </returns>
        private async Task<Result> ProcessAndUpdateCarImagesAsync(UpdateDriverDto driverDto, int carId)
        {
            if (driverDto.HasCar && string.IsNullOrEmpty(driverDto.MainCarImageUrl) && (driverDto.CarImageUrl == null || !driverDto.CarImageUrl.Any()))
            {
                return Result.Success(); // No images to process
            }

            try
            {
                // Update or set the main image if specified
                if (!string.IsNullOrEmpty(driverDto.MainCarImageUrl))
                {
                    var setMainImageResult = await _carService.SetMainImageAsync(carId, driverDto.MainCarImageUrl);
                    if (!setMainImageResult.IsSuccess)
                    {
                        return Result.Failure($"Failed to set the main car image: {driverDto.MainCarImageUrl}");
                    }
                }

                // Add or update additional images
                if (driverDto.CarImageUrl != null && driverDto.CarImageUrl.Any())
                {
                    foreach (var imageUrl in driverDto.CarImageUrl)
                    {
                        // Check if the image already exists
                        var existingImage = await _carService.GetCarImagesAsync(carId);
                        if (existingImage != null && existingImage.Any(ci => ci.ImageUrl == imageUrl))
                        {
                            continue; // Skip if the image already exists
                        }

                        // Add new car image
                        var addImageResult = await _carService.AddCarImageAsync(new CarImageDto
                        {
                            CarId = carId,
                            ImageUrl = imageUrl,
                            IsMain = false // Additional images are not marked as main
                        });

                        if (!addImageResult.IsSuccess)
                        {
                            return Result.Failure($"Failed to add car image: {imageUrl}");
                        }
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while processing car images: {ex.Message}");
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
                // Start a transaction for atomicity.
                var transaction = await _unitOfWork.StartTransactionAsync();
                if (!transaction.IsSuccess)
                    return Result.Failure("Failed to start the transaction.");

                // Retrieve the driver.
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                // Delete associated car and its images.
                var carDeletionResult = await DeleteCarAndImagesAsync(driver);
                if (!carDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return carDeletionResult;
                }

                // Delete the driver record.
                var driverDeletionResult = await DeleteDriverRecordAsync(driver);
                if (!driverDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return driverDeletionResult;
                }

                // Delete the associated user.
                var userDeletionResult = await DeleteAssociatedUserAsync(driver);
                if (!userDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return userDeletionResult;
                }

                // Commit the transaction if all operations succeed.
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to commit the transaction.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback the transaction on error.
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error deleting driver: {ex.Message}");
            }
        }

        #region Deletion Helper Functions

        /// <summary>
        /// Deletes the car and its associated images, if they exist, for the specified driver.
        /// </summary>
        private async Task<Result> DeleteCarAndImagesAsync(Driver driver)
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
        private async Task<Result> DeleteDriverRecordAsync(Driver driver)
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
        private async Task<Result> DeleteAssociatedUserAsync(Driver driver)
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
        public async Task<Result> UpdateDriverStatusAsync(int driverId, DriverStatus newStatus)
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
                var saveResult = await _unitOfWork.CommitAsync();
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
        public async Task<DriverStatus?> GetDriverStatusAsync(int driverId)
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
        public async Task<bool> DoesDriverExistByIdAsync(int driverId, CancellationToken cancellationToken = default)
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
        public async Task<bool> DoesDriverExistByEmailAsync(string email, CancellationToken cancellationToken = default)
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

        #region Driver Cities Operations
        /// <summary>
        /// Adds a list of cities to a driver with the specified work status.
        /// </summary>
        /// <param name="dto">The DTO containing driver ID, city IDs, and work status.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> AddDriverCitiesAsync(AddDriverCitiesDto dto)
        {
            return null;
        }

        public async Task<bool> IsCityAlreadyAssignedAsync(int driverId, int cityId)
        {
            return await _unitOfWork.Repository<DriverCity>().Query()
                                .AnyAsync(dc => dc.DriverId == driverId && dc.CityId == cityId);
        }

        /// <summary>
        /// Removes a city association from a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="cityId">The ID of the city.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> RemoveDriverCityAsync(int driverId, int cityId)
        {
            try
            {
                var driverCity = await _unitOfWork.Repository<DriverCity>().GetAsync(dc => dc.DriverId == driverId && dc.CityId == cityId);

                if (driverCity == null)
                {
                    return Result.Failure("City not found for the specified driver.");
                }

                await _unitOfWork.Repository<DriverCity>().DeleteAsync(driverCity);

                var saveResult = await _unitOfWork.CommitAsync();
                if (!saveResult.IsSuccess)
                {
                    return Result.Failure("Failed to remove driver city association.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error occurred while removing driver city: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the work status for a specific driver-city association.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="cityId">The ID of the city.</param>
        /// <param name="newStatus">The new work status to set.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> UpdateDriverCityWorkStatusAsync(int driverId, int cityId, WorkStatus newStatus)
        {
            try
            {
                var driverCity = await _unitOfWork.Repository<DriverCity>().GetAsync(dc => dc.DriverId == driverId && dc.CityId == cityId);

                if (driverCity == null)
                {
                    return Result.Failure("City not found for the specified driver.");
                }

                driverCity.WorkStatus = newStatus;
                await _unitOfWork.Repository<DriverCity>().UpdateAsync(driverCity);

                var saveResult = await _unitOfWork.CommitAsync();
                if (!saveResult.IsSuccess)
                {
                    return Result.Failure("Failed to save driver city work status updates.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error occurred while updating work status: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all cities associated with a specific driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <returns>
        /// A <see cref="DataResult{DriverCity}"/> containing the cities associated with the driver.
        /// </returns>
        public async Task<GetDriverCitiesDto?> GetDriverCitiesAsync(int driverId)
        {
            try
            {
                if (driverId <= 0)
                {
                    throw new ArgumentException("Driver ID must be greater than zero.", nameof(driverId));
                }

                // Retrieve all DriverCity entities for the given driver ID
                var driverCities = await _unitOfWork.Repository<DriverCity>()

                    .GetAllAsync(dc => dc.DriverId == driverId);

                if (driverCities == null || !driverCities.Any())
                {
                    return null;
                }

                // Map DriverCity entities to GetDriverCitiesDto
                var driverCitiesDto = driverCities.Select(dc => new CityDto
                {
                    Id = dc.CityId,
                    Name = dc.City.Name

                });

                return new GetDriverCitiesDto
                {
                    DriverId = driverId,
                    Cities = driverCitiesDto.ToList()
                };
            }
            catch (ArgumentException ex)
            {
                // Log and rethrow for validation errors
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while retrieving driver cities.", ex);
            }
        }


        #endregion
    }
}
