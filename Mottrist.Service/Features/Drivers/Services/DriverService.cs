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

namespace Mottrist.Service.Features.Drivers.Services
{
    /// <summary>
    /// Provides services for managing driver-related operations, including user management,
    /// car details handling, and database transactions.
    /// </summary>
    public class DriverService : BaseService, IDriverService
    {
        #region Folder Paths
        private string _GetProfilesFolder(int driverId) => $"drivers/{driverId}/profiles";
        private string _GetPassportsFolder(int driverId) => $"drivers/{driverId}/passports";
        private string _GetLicensesFolder(int driverId) => $"drivers/{driverId}/licenses";

        private string _GetCarsFolder(int driverId) => $"drivers/{driverId}/cars";
        #endregion
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

        /// <summary>
        /// Service for managing language-related operations.
        /// </summary>
        private readonly ILanguageService _languageService;

        /// <summary>
        /// Service for managing country-related operations.
        /// </summary>
        private readonly ICountryService _countryService;

        /// <summary>
        /// Service for managing city-related operations.
        /// </summary>
        private readonly ICityService _cityService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for managing database interactions.</param>
        /// <param name="mapper">The mapper instance for object mapping.</param>
        /// <param name="carService">The service for managing car operations.</param>
        /// <param name="userManager">The user manager for handling user-related operations.</param>
        /// <param name="languageService">The service for managing language-related operations.</param>
        /// <param name="countryService">The service for managing country-related operations.</param>
        /// <param name="cityService">The service for managing city-related operations.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the injected dependencies are null.
        /// </exception>
        public DriverService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICarService carService,
            UserManager<ApplicationUser> userManager,
            ILanguageService languageService,
            ICountryService countryService,
            ICityService cityService)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
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
                    LikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == true).Count(),
                    DislikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == false).Count(),
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
                        .ToList() ?? new List<string>(), // Additional images
                    Status = driver.Status.ToString(),
                    IsAvailableAllTime = driver.IsAvailableAllTime,
                    AvailableFrom = driver.AvailableFrom,
                    AvailableTo = driver.AvailableTo,
                    PricePerHour = driver.PricePerHour,
                    CitiesCoverNow = driver.DriverCities.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.City.Name).ToList(),
                    CitiesWorkedOn = driver.DriverCities.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.City.Name).ToList(),
                    CountriesCoverNow = driver.DriverCountries.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.Country.Name).ToList(),
                    CountriesWorkedOn = driver.DriverCountries.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.Country.Name).ToList(),
                    LanguagesSpoken = driver.DriverLanguages.Select(x => x.Language.Name).ToList()
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
                        LikesCount = driver.DriverInteractions
                            .Where(x => x.DriverId == driver.Id && x.IsLiked == true)
                            .Count(),
                        DislikesCount = driver.DriverInteractions
                            .Where(x => x.DriverId == driver.Id && x.IsLiked == false)
                            .Count(),
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
                            .ToList() ?? new List<string>(),
                        Status = driver.Status.ToString(),
                        IsAvailableAllTime = driver.IsAvailableAllTime,
                        AvailableFrom = driver.AvailableFrom,
                        AvailableTo = driver.AvailableTo,
                        PricePerHour = driver.PricePerHour,
                        CitiesCoverNow = driver.DriverCities
                            .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                            .Select(x => x.City.Name)
                            .ToList(),
                        CitiesWorkedOn =driver.DriverCities
                            .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                            .Select(x => x.City.Name)
                            .ToList(),
                        CountriesCoverNow = driver.DriverCountries
                            .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                            .Select(x => x.Country.Name)
                            .ToList(),
                        CountriesWorkedOn = driver.DriverCountries
                            .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                            .Select(x => x.Country.Name)
                            .ToList(),
                        LanguagesSpoken = driver.DriverLanguages
                            .Select(x => x.Language.Name)
                            .ToList()
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
                            .ToList() ?? new List<string>(),
                        Status = x.Status.ToString(),
                        IsAvailableAllTime = x.IsAvailableAllTime,
                        AvailableFrom = x.AvailableFrom,
                        AvailableTo = x.AvailableTo,
                        PricePerHour = x.PricePerHour,
                        CitiesCoverNow = x.DriverCities.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.City.Name).ToList(),
                        CitiesWorkedOn = x.DriverCities.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.City.Name).ToList(),
                        CountriesCoverNow = x.DriverCountries.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.Country.Name).ToList(),
                        CountriesWorkedOn = x.DriverCountries.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.Country.Name).ToList(),
                        LanguagesSpoken = x.DriverLanguages.Select(x => x.Language.Name).ToList()
                    })
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
                        LikesCount = x.DriverInteractions.Where(x => x.DriverId == x.Id && x.IsLiked == true).Count(),
                        DislikesCount = x.DriverInteractions.Where(x => x.DriverId == x.Id && x.IsLiked == false).Count(),
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
                            .ToList() ?? new List<string>(),
                        Status = x.Status.ToString(),
                        IsAvailableAllTime = x.IsAvailableAllTime,
                        AvailableFrom = x.AvailableFrom,
                        AvailableTo = x.AvailableTo,
                        PricePerHour = x.PricePerHour,
                        CitiesCoverNow = x.DriverCities.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.City.Name).ToList(),
                        CitiesWorkedOn =  x.DriverCities.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.City.Name).ToList(),
                        CountriesCoverNow =  x.DriverCountries.Where(x => x.WorkStatus == WorkStatus.CoverNow).Select(x => x.Country.Name).ToList(),
                        CountriesWorkedOn = x.DriverCountries.Where(x => x.WorkStatus == WorkStatus.WorkedOn).Select(x => x.Country.Name).ToList(),
                        LanguagesSpoken = x.DriverLanguages.Select(x => x.Language.Name).ToList()
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

        /// <summary>
        /// Retrieves all necessary form fields for driver registration, including car-related fields, languages, countries, and cities.
        /// </summary>
        /// <returns>
        /// A <see cref="DriverFormFieldsDto"/> object containing car fields, languages, countries, and cities.
        /// Returns null if an exception occurs.
        /// </returns>
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
     

        /// <summary>
        /// Retrieves a list of drivers available in the specified country and, optionally, in the specified city and/or on the specified date.
        /// </summary>
        /// <param name="countryId">
        /// The ID of the country where the driver operates. Only drivers whose associated country matches this ID (with a "CoverNow" work status) are included.
        /// </param>
        /// <param name="cityId">
        /// (Optional) The ID of the city where the driver operates. Only drivers whose associated city matches this ID (with a "CoverNow" work status) are included.
        /// </param>
        /// <param name="date">
        /// (Optional) The specific date on which the driver should be available. 
        /// Drivers are included if they are marked as available all the time or if the provided date falls between their AvailableFrom and AvailableTo dates.
        /// </param>
        /// <returns>
        /// A <see cref="DataResult{DriverDto}"/> containing the list of matching drivers, or null if the provided parameters are invalid or an error occurs.
        /// </returns>
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
                    .Include(d => d.User)
                    .Include(d => d.Country)
                    .Include(d => d.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(d => d.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(d => d.DriverCountries)
                        .ThenInclude(dc => dc.Country)
                    .Include(d => d.DriverLanguages)
                        .ThenInclude(dl => dl.Language)
                    .AsNoTracking();

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
                // Project the data into a DriverDto.
                var drivers = await query.Select(driver => new DriverDto
                {
                    Id = driver.Id,
                    WhatsAppNumber = driver.WhatsAppNumber,
                    Nationality = driver.Country.Name ?? "Unknown",
                    LicenseImageUrl = driver.LicenseImageUrl,
                    YearsOfExperience = driver.YearsOfExperience,
                    Bio = driver.Bio,
                    PassportImageUrl = driver.PassportImageUrl,
                    FirstName = driver.User.FirstName,
                    LastName = driver.User.LastName,
                    Email = driver.User.Email ?? string.Empty,
                    PhoneNumber = driver.User.PhoneNumber,
                    ProfileImageUrl = driver.ProfileImageUrl,
                    LikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == true).Count(),
                    DislikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == false).Count(),
                    HasCar = driver.CarId.HasValue,
                    CarBrand = driver.Car.Brand.Name ?? "N/A",
                    CarYear = driver.Car.Year,
                    CarNumberOfSeats = driver.Car.NumberOfSeats,
                    CarModel = driver.Car.Model.Name,
                    CarColor = driver.Car.Color.Name,
                    CarBodyType = driver.Car.BodyType.Type,
                    CarFuelType = driver.Car.FuelType.Type,
                    MainCarImageUrl = driver.Car.CarImages
                                         .FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                    AddtionalCarImageUrls = driver.Car != null ?
                        driver.Car.CarImages.Where(ci => !ci.IsMain).Select(ci => ci.ImageUrl).ToList() : new List<string>(),
                    Status = driver.Status.ToString(),
                    IsAvailableAllTime = driver.IsAvailableAllTime,
                    AvailableFrom = driver.AvailableFrom,
                    AvailableTo = driver.AvailableTo,
                    PricePerHour = driver.PricePerHour,
                    CitiesCoverNow = driver.DriverCities
                                        .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                                        .Select(x => x.City.Name)
                                        .ToList(),
                    CitiesWorkedOn = driver.DriverCities
                                        .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                                        .Select(x => x.City.Name)
                                        .ToList(),
                    CountriesCoverNow = driver.DriverCountries
                                        .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                                        .Select(x => x.Country.Name)
                                        .ToList(),
                    CountriesWorkedOn = driver.DriverCountries
                                        .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                                        .Select(x => x.Country.Name)
                                        .ToList(),
                    LanguagesSpoken = driver.DriverLanguages
                                        .Select(x => x.Language.Name)
                                        .ToList()
                })
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

        /// <summary>
        /// Retrieves a paginated list of drivers filtered by country, city, and optional availability date.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier of the country to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="cityId">
        /// (Optional) The unique identifier of the city to filter drivers.
        /// If provided, must be greater than 0.
        /// </param>
        /// <param name="date">
        /// (Optional) The specific date when the driver should be available.
        /// If provided, only drivers available on this date or marked as available all the time will be included.
        /// </param>
        /// <param name="page">
        /// The page number for paginated results.
        /// Must be greater than 0.
        /// </param>
        /// <param name="pageSize">
        /// The number of records per page.
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// A <see cref="PaginatedResult{DriverDto}"/> containing paginated drivers that match the filtering criteria.
        /// Returns an empty paginated list if no drivers match.
        /// </returns>
        private async Task<PaginatedResult<DriverDto>?> _GetByLocationAndDateWithPaginationAsync(
            int countryId,
            int? cityId = null,
            DateTime? date = null,
            int page = 1,
            int pageSize = 10)
        {
            // Validate parameters: countryId is required and must be > 0;
            // cityId, if provided, must also be > 0;
            // page and pageSize must be greater than 0.
            if (countryId < 1 || (cityId.HasValue && cityId < 1) || page < 1 || pageSize < 1)
            {
                return null;
            }

            try
            {
                // Build the base query with required includes and no-tracking for performance.
                var query = _unitOfWork.Repository<Driver>().Query()
                    .Include(d => d.User)
                    .Include(d => d.Country)
                    .Include(d => d.Car)
                        .ThenInclude(c => c.CarImages)
                    .Include(d => d.DriverCities)
                        .ThenInclude(dc => dc.City)
                    .Include(d => d.DriverCountries)
                        .ThenInclude(dc => dc.Country)
                    .Include(d => d.DriverLanguages)
                        .ThenInclude(dl => dl.Language)
                    .AsNoTracking();

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
                    .Select(driver => new DriverDto
                    {
                        Id = driver.Id,
                        WhatsAppNumber = driver.WhatsAppNumber,
                        Nationality = driver.Country.Name ?? "Unknown",
                        LicenseImageUrl = driver.LicenseImageUrl,
                        YearsOfExperience = driver.YearsOfExperience,
                        Bio = driver.Bio,
                        PassportImageUrl = driver.PassportImageUrl,
                        FirstName = driver.User.FirstName,
                        LastName = driver.User.LastName,
                        Email = driver.User.Email ?? string.Empty,
                        PhoneNumber = driver.User.PhoneNumber,
                        ProfileImageUrl = driver.ProfileImageUrl,
                        LikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == true).Count(),
                        DislikesCount = driver.DriverInteractions.Where(x => x.DriverId == driver.Id && x.IsLiked == false).Count(),
                        HasCar = driver.CarId.HasValue,
                        CarBrand = driver.Car.Brand.Name ?? "N/A",
                        CarYear = driver.Car.Year,
                        CarNumberOfSeats = driver.Car.NumberOfSeats,
                        CarModel = driver.Car.Model.Name,
                        CarColor = driver.Car.Color.Name,
                        CarBodyType = driver.Car.BodyType.Type,
                        CarFuelType = driver.Car.FuelType.Type,
                        MainCarImageUrl = driver.Car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                        AddtionalCarImageUrls = driver.Car != null ?
                            driver.Car.CarImages
                                            .Where(ci => !ci.IsMain)
                                            .Select(ci => ci.ImageUrl)
                                            .ToList() : new List<string>(),
                        Status = driver.Status.ToString(),
                        IsAvailableAllTime = driver.IsAvailableAllTime,
                        AvailableFrom = driver.AvailableFrom,
                        AvailableTo = driver.AvailableTo,
                        PricePerHour = driver.PricePerHour,
                        CitiesCoverNow = driver.DriverCities
                                            .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                                            .Select(x => x.City.Name)
                                            .ToList(),
                        CitiesWorkedOn = driver.DriverCities
                                            .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                                            .Select(x => x.City.Name)
                                            .ToList(),
                        CountriesCoverNow = driver.DriverCountries
                                            .Where(x => x.WorkStatus == WorkStatus.CoverNow)
                                            .Select(x => x.Country.Name)
                                            .ToList(),
                        CountriesWorkedOn = driver.DriverCountries
                                            .Where(x => x.WorkStatus == WorkStatus.WorkedOn)
                                            .Select(x => x.Country.Name)
                                            .ToList(),
                        LanguagesSpoken = driver.DriverLanguages
                                            .Select(x => x.Language.Name)
                                            .ToList()
                    }).ToListAsync();

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

                // Create user and assign driver role
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

                // Map DTO to driver entity
                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id;


                if (driverEntity.IsAvailableAllTime)
                {
                    driverEntity.AvailableFrom = null;
                    driverEntity.AvailableTo = null;
                }

                // Add driver entity to the repository
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var saveDriverResult = await _unitOfWork.SaveChangesAsync();
                if (!saveDriverResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver details.");
                }

                // Add all associations in one batch
                var associationsResult = await _AddDriverAssociationsAsync(
                    driverEntity.Id,
                    driverDto.LanguagesSpoken,
                    driverDto.CitiesWorkedOn,
                    driverDto.CitiesCoverNow,
                    driverDto.CountriesWorkedOn,
                    driverDto.CountriesCoverNow
                );

                if (!associationsResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return associationsResult;
                }

                if(driverEntity.Id < 1)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver details.");
                }

                driverDto.Id = driverEntity.Id;

                // Process all driver-related images
                await _ProcessDriverImagesAsync(driverDto);

                if(string.IsNullOrEmpty(driverDto.LicenseImageUrl) || string.IsNullOrEmpty(driverDto.PassportImageUrl))
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save images.");

                }

                driverEntity.LicenseImageUrl = driverDto.LicenseImageUrl;
                driverEntity.PassportImageUrl = driverDto.PassportImageUrl;
                driverEntity.ProfileImageUrl = driverDto.ProfileImageUrl;

               await _unitOfWork.Repository<Driver>().UpdateAsync(driverEntity);
                var saveUpdateDriverResult = await _unitOfWork.SaveChangesAsync();
                if (!saveUpdateDriverResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver details.");
                }

                // Add car details if HasCar is true
                if (driverDto.HasCar)
                {
                    var carResult = await _AddCarWithCarServiceAsync(driverDto, driverEntity);
                    if (!carResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return carResult;
                    }
                }

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to commit the transaction.");
                }
                // Assign generated driver ID back to DTO
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"An unexpected error occurred: {ex.Message}");
            }
        }

        #region Additional Methods
        /// <summary>
        /// Adds languages, cities, and countries associated with a driver to their respective tables in a single batch.
        /// </summary>
        /// <param name="driverId">The unique ID of the driver.</param>
        /// <param name="languages">The list of language IDs to add.</param>
        /// <param name="citiesWorkedOn">The list of city IDs marked as worked-on to add.</param>
        /// <param name="citiesCoverNow">The list of city IDs currently covered to add.</param>
        /// <param name="countriesWorkedOn">The list of country IDs marked as worked-on to add.</param>
        /// <param name="countriesCoverNow">The list of country IDs currently covered to add.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the batch operation.
        /// </returns>
        private async Task<Result> _AddDriverAssociationsAsync(
            int driverId,
            List<int> languages,
            List<int> citiesWorkedOn,
            List<int> citiesCoverNow,
            List<int> countriesWorkedOn,
            List<int> countriesCoverNow)
        {
            try
            {
                // Languages
                if (languages != null && languages.Any())
                {
                    var driverLanguages = languages.Select(languageId => new DriverLanguage
                    {
                        DriverId = driverId,
                        LanguageId = languageId
                    }).ToList();

                    await _unitOfWork.Repository<DriverLanguage>().AddRangeAsync(driverLanguages);
                }

                // Cities Worked On
                if (citiesWorkedOn != null && citiesWorkedOn.Any())
                {
                    var driverCitiesWorkedOn = citiesWorkedOn.Select(cityId => new DriverCity
                    {
                        DriverId = driverId,
                        CityId = cityId,
                        WorkStatus = WorkStatus.WorkedOn
                    }).ToList();

                    await _unitOfWork.Repository<DriverCity>().AddRangeAsync(driverCitiesWorkedOn);
                }

                // Cities Cover Now
                if (citiesCoverNow != null && citiesCoverNow.Any())
                {
                    var driverCitiesCoverNow = citiesCoverNow.Select(cityId => new DriverCity
                    {
                        DriverId = driverId,
                        CityId = cityId,
                        WorkStatus = WorkStatus.CoverNow
                    }).ToList();

                    await _unitOfWork.Repository<DriverCity>().AddRangeAsync(driverCitiesCoverNow);
                }

                // Countries Worked On
                if (countriesWorkedOn != null && countriesWorkedOn.Any())
                {
                    var driverCountriesWorkedOn = countriesWorkedOn.Select(countryId => new DriverCountry
                    {
                        DriverId = driverId,
                        CountryId = countryId,
                        WorkStatus = WorkStatus.WorkedOn
                    }).ToList();

                    await _unitOfWork.Repository<DriverCountry>().AddRangeAsync(driverCountriesWorkedOn);
                }

                // Countries Cover Now
                if (countriesCoverNow != null && countriesCoverNow.Any())
                {
                    var driverCountriesCoverNow = countriesCoverNow.Select(countryId => new DriverCountry
                    {
                        DriverId = driverId,
                        CountryId = countryId,
                        WorkStatus = WorkStatus.CoverNow
                    }).ToList();

                    await _unitOfWork.Repository<DriverCountry>().AddRangeAsync(driverCountriesCoverNow);
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An unexpected error occurred while adding associations: {ex.Message}");
            }
        }


        #endregion

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
        /// <summary>
        /// Adds a car to the system using the CarService, along with its images if provided.
        /// </summary>
        /// <param name="driverDto">The DTO containing car details.</param>
        /// <param name="driverEntity">The driver entity to link the car to.</param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        private async Task<Result> _AddCarWithCarServiceAsync(AddDriverDto driverDto, Driver driverEntity)
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
                        IsMain = (i == 0)
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
        private async Task _ProcessDriverImagesAsync(AddDriverDto driverDto)
        {
            // Process profile image.
            if (driverDto.ProfileImage != null)
            {
                driverDto.ProfileImageUrl = await SaveImageAsync(driverDto.ProfileImage, _GetProfilesFolder(driverDto.Id));
            }

            // Process license image.
            if (driverDto.LicenseImage != null)
            {
                driverDto.LicenseImageUrl = await SaveImageAsync(driverDto.LicenseImage,_GetLicensesFolder(driverDto.Id));
            }

            // Process passport image.
            if (driverDto.PassportImage != null)
            {
                driverDto.PassportImageUrl = await SaveImageAsync(driverDto.PassportImage, _GetPassportsFolder(driverDto.Id));
            }

            // Process car images.
            if (driverDto.HasCar && driverDto.CarImages != null && driverDto.CarImages.Any())
            {
                var processedCarImages = new List<string>();
                for (int i = 0; i < driverDto.CarImages.Count; i++)
                {
                    var carImage = driverDto.CarImages[i];
                    var savedImageUrl = await SaveImageAsync(carImage, _GetCarsFolder(driverDto.Id));
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


                var imageUpdateResult = await _UpdateImagesAsync(driverDto, existingDriver);
                if (!imageUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return imageUpdateResult;
                }
                // Step 1: Update driver details.
                _mapper.Map(driverDto, existingDriver);

                // Step 2: Update associated user details.
                var userUpdateResult = await _UpdateUserDetailsAsync(driverDto, existingDriver.UserId);
                if (!userUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure($"Failed to update user details: {userUpdateResult.Errors.FirstOrDefault()}");
                }


                // Step 4: Update languages, cities, and countries.
                var associationsResult = await _UpdateDriverAssociationsAsync(
                    driverDto.Id,
                    driverDto.LanguagesSpoken,
                    driverDto.CitiesWorkedOn,
                    driverDto.CitiesCoverNow,
                    driverDto.CountriesWorkedOn,
                    driverDto.CountriesCoverNow
                );


                if (!associationsResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return associationsResult;
                }

                if (existingDriver.IsAvailableAllTime)
                {
                    existingDriver.AvailableFrom = null;
                    existingDriver.AvailableTo = null;
                }

                if (existingDriver.PricePerHour <= 0)
                {
                    existingDriver.PricePerHour = null;
                }

                // Step 5: Save updated driver details.
                await _unitOfWork.Repository<Driver>().UpdateAsync(existingDriver);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save driver updates.");
                }

                if (driverDto.HasCar)
                {
                    var carUpdateResult = await _UpdateOrAddCarDetailsAsync(driverDto, existingDriver);
                    if (!carUpdateResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure($"Failed to update car details: {carUpdateResult.Errors.FirstOrDefault()}");
                    }
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


        /// <summary>
        /// Handles the updating of a driver's images, including profile, license, and passport.
        /// </summary>
        /// <param name="driverDto">The DTO containing updated image data.</param>
        /// <param name="existingDriver">The existing driver entity.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        private async Task<Result> _UpdateImagesAsync(UpdateDriverDto driverDto, Driver existingDriver)
        {
            try
            {
                var profileUpdateResult = (IsSuccess: false, NewImageUrl: (string?)null);

                if (!string.IsNullOrEmpty(existingDriver.ProfileImageUrl) && (string.IsNullOrEmpty(driverDto.ProfileImageUrl) && driverDto.ProfileImage is null))
                {
                    profileUpdateResult = await UpdateImageAsync(null, _GetProfilesFolder(driverDto.Id), existingDriver.ProfileImageUrl);
                }
                else if(driverDto.ProfileImage != null)
                {
                    profileUpdateResult = await UpdateImageAsync(driverDto.ProfileImage,
                                                                   _GetProfilesFolder(driverDto.Id),
                                                                   existingDriver.ProfileImageUrl); ;
                }
                else
                {
                    profileUpdateResult.NewImageUrl = existingDriver.ProfileImageUrl;
                }

                if (!profileUpdateResult.IsSuccess)
                    return Result.Failure("Error updating profile image.");
                driverDto.ProfileImageUrl = profileUpdateResult.NewImageUrl;


                var licenseUpdateResult = (IsSuccess: false, NewImageUrl: (string?)null);

                if (!string.IsNullOrEmpty(existingDriver.LicenseImageUrl) && (string.IsNullOrEmpty(driverDto.LicenseImageUrl) && driverDto.LicenseImage is null))
                {
                    licenseUpdateResult = await UpdateImageAsync(null, _GetLicensesFolder(driverDto.Id), existingDriver.LicenseImageUrl);
                }
                else if(driverDto.LicenseImage != null)
                {
                    licenseUpdateResult = await UpdateImageAsync(driverDto.LicenseImage,
                                                                   _GetLicensesFolder(driverDto.Id),
                                                                   existingDriver.LicenseImageUrl);
                }
                else
                {
                    licenseUpdateResult.NewImageUrl = existingDriver.LicenseImageUrl;
                }

                // Ensure license update success before assigning
                if (!licenseUpdateResult.IsSuccess || string.IsNullOrWhiteSpace(licenseUpdateResult.NewImageUrl))
                    return Result.Failure("Error updating license image.");
                driverDto.LicenseImageUrl = licenseUpdateResult.NewImageUrl;



                var passportUpdateResult = (IsSuccess: false, NewImageUrl: (string?)null);

                if (!string.IsNullOrEmpty(existingDriver.PassportImageUrl) && (string.IsNullOrEmpty(driverDto.PassportImageUrl) && driverDto.PassportImage is null))
                {
                    passportUpdateResult = await UpdateImageAsync(null, _GetPassportsFolder(driverDto.Id), existingDriver.PassportImageUrl);
                }
                else if(driverDto.PassportImage != null)
                {
                    passportUpdateResult = await UpdateImageAsync(driverDto.PassportImage,
                                                                   _GetPassportsFolder(driverDto.Id),
                                                                   existingDriver.PassportImageUrl);
                }
                else
                {
                    passportUpdateResult.NewImageUrl = existingDriver.PassportImageUrl;
                }

                // Ensure passport image update was successful
                if (!passportUpdateResult.IsSuccess || string.IsNullOrEmpty(passportUpdateResult.NewImageUrl))
                    return Result.Failure("Error updating driver passport image.");

                driverDto.PassportImageUrl = passportUpdateResult.NewImageUrl;


                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error updating driver images: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the availability details of a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose availability is being updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="availableFrom">
        /// The date when the driver becomes available.
        /// If null, the availability start date remains unchanged.
        /// </param>
        /// <param name="availableTo">
        /// The date when the driver is no longer available.
        /// If null, the availability end date remains unchanged.
        /// </param>
        /// <param name="availableAllTime">
        /// Indicates whether the driver is available all the time.
        /// If true, the availability dates may be ignored.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> object indicating whether the update was successful or if an error occurred.
        /// </returns>
        public async Task<Result> UpdateAvailabilityAsync(
            int driverId,
            DateTime? availableFrom,
            DateTime? availableTo,
            bool availableAllTime)
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
                driver.IsAvailableAllTime = availableAllTime;

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

        /// <summary>
        /// Updates the price per hour for a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose pricing is being updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="newPricePerHour">
        /// The new price per hour to set for the driver.
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the update was successful or if an error occurred.
        /// </returns>
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
        /// <summary>
        /// Updates the languages, cities, and countries associated with the driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="languages">The updated list of language IDs.</param>
        /// <param name="citiesWorkedOn">The updated list of worked-on city IDs.</param>
        /// <param name="citiesCoverNow">The updated list of currently covered city IDs.</param>
        /// <param name="countriesWorkedOn">The updated list of worked-on country IDs.</param>
        /// <param name="countriesCoverNow">The updated list of currently covered country IDs.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the update operation.
        /// </returns>
        private async Task<Result> _UpdateDriverAssociationsAsync(
            int driverId,
            List<int> languages,
            List<int> citiesWorkedOn,
            List<int> citiesCoverNow,
            List<int> countriesWorkedOn,
            List<int> countriesCoverNow)
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
                        return Result.Failure("Failed to delete existing driver languages.");
                    }
                }
                if (languages != null && languages.Any())
                {
                    var newLanguages = languages.Select(languageId => new DriverLanguage
                    {
                        DriverId = driverId,
                        LanguageId = languageId
                    }).ToList();
                    await _unitOfWork.Repository<DriverLanguage>().AddRangeAsync(newLanguages);
                    var saveLangResult = await _unitOfWork.SaveChangesAsync();
                    if (!saveLangResult.IsSuccess)
                    {
                        return Result.Failure("Failed to save driver languages.");
                    }
                }

                // --- Cities Worked On ---
                var existingWorkedOnCities = await _unitOfWork.Repository<DriverCity>()
                    .Query().AsNoTracking()
                    .Where(dc => dc.DriverId == driverId && dc.WorkStatus == WorkStatus.WorkedOn)
                    .ToListAsync();
                if (existingWorkedOnCities != null && existingWorkedOnCities.Any())
                {
                    await _unitOfWork.Repository<DriverCity>().DeleteRangeAsync(existingWorkedOnCities);
                    var deleteWorkedOnCitiesResult = await _unitOfWork.SaveChangesAsync();
                    if (!deleteWorkedOnCitiesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to delete existing worked-on cities for the driver.");
                    }
                }
                if (citiesWorkedOn != null && citiesWorkedOn.Any())
                {
                    var newWorkedOnCities = citiesWorkedOn.Select(cityId => new DriverCity
                    {
                        DriverId = driverId,
                        CityId = cityId,
                        WorkStatus = WorkStatus.WorkedOn
                    }).ToList();
                    await _unitOfWork.Repository<DriverCity>().AddRangeAsync(newWorkedOnCities);
                    var saveWorkedOnCitiesResult = await _unitOfWork.SaveChangesAsync();
                    if (!saveWorkedOnCitiesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to save driver worked-on cities.");
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
                        return Result.Failure("Failed to delete existing cover now cities for the driver.");
                    }
                }
                if (citiesCoverNow != null && citiesCoverNow.Any())
                {
                    var newCoverNowCities = citiesCoverNow.Select(cityId => new DriverCity
                    {
                        DriverId = driverId,
                        CityId = cityId,
                        WorkStatus = WorkStatus.CoverNow
                    }).ToList();
                    await _unitOfWork.Repository<DriverCity>().AddRangeAsync(newCoverNowCities);
                    var saveCoverNowCitiesResult = await _unitOfWork.SaveChangesAsync();
                    if (!saveCoverNowCitiesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to save driver cover now cities.");
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
                        return Result.Failure("Failed to delete existing worked-on countries for the driver.");
                    }
                }
                if (countriesWorkedOn != null && countriesWorkedOn.Any())
                {
                    var newWorkedOnCountries = countriesWorkedOn.Select(countryId => new DriverCountry
                    {
                        DriverId = driverId,
                        CountryId = countryId,
                        WorkStatus = WorkStatus.WorkedOn
                    }).ToList();
                    await _unitOfWork.Repository<DriverCountry>().AddRangeAsync(newWorkedOnCountries);
                    var saveWorkedOnCountriesResult = await _unitOfWork.SaveChangesAsync();
                    if (!saveWorkedOnCountriesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to save driver worked-on countries.");
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
                        return Result.Failure("Failed to delete existing cover now countries for the driver.");
                    }
                }
                if (countriesCoverNow != null && countriesCoverNow.Any())
                {
                    var newCoverNowCountries = countriesCoverNow.Select(countryId => new DriverCountry
                    {
                        DriverId = driverId,
                        CountryId = countryId,
                        WorkStatus = WorkStatus.CoverNow
                    }).ToList();
                    await _unitOfWork.Repository<DriverCountry>().AddRangeAsync(newCoverNowCountries);
                    var saveCoverNowCountriesResult = await _unitOfWork.SaveChangesAsync();
                    if (!saveCoverNowCountriesResult.IsSuccess)
                    {
                        return Result.Failure("Failed to save driver cover now countries.");
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to update driver associations: {ex.Message}");
            }
        }


        /// <summary>
        /// Updates the associated user details of the driver.
        /// </summary>
        /// <param name="driverDto">The driver DTO with updated details.</param>
        /// <param name="userId">The ID of the associated user to update.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
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

        /// <summary>
        /// Updates or adds car details and images for the driver.
        /// </summary>
        /// <param name="driverDto">The driver DTO with updated car details.</param>
        /// <param name="existingDriver">The existing driver entity.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
        private async Task<Result> _UpdateOrAddCarDetailsAsync(UpdateDriverDto driverDto, Driver existingDriver)
        {
            try
            {
                if (existingDriver.CarId.HasValue)
                {
                    if (!driverDto.HasCar)
                    {
                        // If the driver no longer has a car, delete the car and its images.
                        var deleteCarResult = await _carService.DeleteAsync(existingDriver.CarId.Value);
                        if (!deleteCarResult.IsSuccess)
                        {
                            return Result.Failure("Failed to delete car details.");
                        }
                        existingDriver.CarId = null; // Clear the CarId from the driver entity.
                        return Result.Success();
                    }

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
                var imageResult = await _ProcessAndUpdateCarImagesAsync(driverDto, existingDriver.CarId.Value);
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
        private async Task<Result> _ProcessAndUpdateCarImagesAsync(UpdateDriverDto driverDto, int carId)
        {
            if (driverDto.HasCar && (driverDto.CarImageUrls == null || !driverDto.CarImageUrls.Any()))
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
                if (driverDto.CarImageUrls != null && driverDto.CarImageUrls.Any())
                {
                    foreach (var imageUrl in driverDto.CarImageUrls)
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
                var carDeletionResult = await _DeleteCarAndImagesAsync(driver);
                if (!carDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return carDeletionResult;
                }


                // Delete the driver record.
                var associatetionDeletionResult = await _DeleteDriverAssociationsAsync(driverId);
                if (!associatetionDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return associatetionDeletionResult;
                }


                // Delete the driver record.
                var driverDeletionResult = await _DeleteDriverRecordAsync(driver);
                if (!driverDeletionResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return driverDeletionResult;
                }

                // Delete the associated user.
                var userDeletionResult = await _DeleteAssociatedUserAsync(driver);
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
