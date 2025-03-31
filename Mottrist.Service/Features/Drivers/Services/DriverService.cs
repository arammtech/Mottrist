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
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Cars.Interfaces;
using Feature.Car.DTOs;
using static Mottrist.Service.Features.Drivers.Helpers.UserHelper;
namespace Mottrist.Service.Features.Drivers.Services
{
    /// <summary>
    /// Provides services for managing driver-related operations, including user management,
    /// car details handling, and database transactions.
    /// </summary>
    public class DriverService : BaseService, IDriverService
    {
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

        /// <summary>
        /// Retrieves a set of drivers with their detailed information, optionally filtered by the specified criteria.
        /// </summary>
        /// <param name="filter">Optional: A filter expression to apply to the query. Defaults to null.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains:
        /// - A set of <see cref="DriverDto"/> objects representing the drivers with their details, or
        /// - Null if an exception occurs or if no matching drivers are found.
        /// </returns>
        public async Task<DataResult<DriverDto>?> GetAllAsync(Expression<Func<DriverDto, bool>>? filter = null)
        {
            try
            {
                // Build the base query
                var driverQuery = from driver in _unitOfWork.Repository<Driver>().Table.AsQueryable()
                                  join user in _unitOfWork.Repository<ApplicationUser>().Table
                                  on driver.UserId equals user.Id
                                  join country in _unitOfWork.Repository<Country>().Table
                                  on driver.NationalityId equals country.Id into countryGroup
                                  from countryDetails in countryGroup.DefaultIfEmpty()
                                  join car in _unitOfWork.Repository<Car>().Table
                                  on driver.CarId equals car.Id into carGroup
                                  from carDetails in carGroup.DefaultIfEmpty()
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
                                      HasCar = driver.CarId != null,
                                      CarBrand = carDetails.Brand.Name,
                                      CarYear = carDetails.Year,
                                      CarNumberOfSeats = carDetails.NumberOfSeats,
                                      CarModel = carDetails.Model.Name,
                                      CarColor = carDetails.Color.Name,
                                      CarBodyType = carDetails.BodyType.Type,
                                      CarFuelType = carDetails.FuelType.Type,
                                      CarImageUrl = carDetails.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl
                                  };

                // Apply filter if provided
                if (filter != null)
                {
                    driverQuery = driverQuery.Where(filter);
                }
                var drivers = await driverQuery.ToListAsync();

                DataResult<DriverDto> driversResult = new()
                {
                    Data = _mapper.Map<IEnumerable<DriverDto>>(drivers)
                };

                if (!drivers.Any())
                    driversResult.Data = Enumerable.Empty<DriverDto>();


                // Execute the query and return the result as a set
                return driversResult;
            }
            catch (Exception)
            {
                // Log the exception if required
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
                        Email = user.Email ?? string.Empty ,
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
                        CarImageUrl = carDetails.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty
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
        /// Adds a new driver to the system, including an associated car and its image if applicable.
        /// </summary>
        /// <param name="driverDto">The DTO containing driver, car, and related details.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> AddAsync(AddDriverDto driverDto)
        {
            // Start a transaction
            var transaction = await _unitOfWork.StartTransactionAsync();
            if (!transaction.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.", true);
            }

            try
            {
                // Check if user with the same email already exists
                var existingUser = await _userManager.FindByEmailAsync(driverDto.Email);
                if (existingUser != null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Exist("User with the same email already exists.");
                }

                // Step 1: Create user
                var user = _mapper.Map<ApplicationUser>(driverDto);
                var userResult = await AddUserAsync(_userManager,driverDto,user);
                if (!userResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return userResult;
                }

                // Step 2: Assign role
                var roleResult = await AssignUserRoleAsync(_userManager,user, AppUserRoles.RoleDriver);
                if (!roleResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return roleResult;
                }

                // Step 3: Create driver entity
                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id;

                // Step 4: Add car using CarService if applicable
                if (driverDto.HasCar)
                {
                    var carResult = await AddCarWithCarServiceAsync(driverDto, driverEntity);
                    if (!carResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return carResult;
                    }
                }

                // Step 5: Save driver entity
                driverEntity.Id = 0;
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver.", true);
                }

                // Commit transaction and update DTO with generated ID
                await _unitOfWork.CommitAsync();
                driverDto.Id = driverEntity.Id;
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error adding driver: {ex.Message}", true);
            }
        }




        /// <summary>
        /// Adds a car to the system using CarService, along with its image if provided.
        /// </summary>
        /// <param name="driverDto">The driver DTO containing car details.</param>
        /// <param name="driverEntity">The driver entity to link the car to.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        private async Task<Result> AddCarWithCarServiceAsync(AddDriverDto driverDto, Driver driverEntity)
        {
            // Map car details to a CarDto
            var carDto = _mapper.Map<CarDto>(driverDto);

            // Add the car using the CarService
            var carAddResult = await _carService.AddAsync(carDto);
            if (!carAddResult.IsSuccess)
            {
                return Result.Failure("Failed to add the car details.", true);
            }

            // Update the driver's CarId with the newly created car's ID
            driverEntity.CarId = carDto.Id;

            // Handle car image creation using CarService
            if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
            {
                var carImageDto = new CarImageDto
                {
                    CarId = carDto.Id,
                    ImageUrl = driverDto.CarImageUrl,
                    IsMain = true
                };

                var carImageResult = await _carService.AddCarImageAsync(carImageDto);
                if (!carImageResult.IsSuccess)
                {
                    return Result.Failure("Failed to add the car image.", true);
                }
            }

            return Result.Success();
        }


        /// <summary>
        /// Updates an existing driver in the system, including associated user and car details if applicable.
        /// </summary>
        /// <param name="driverDto">The DTO containing updated driver and car details.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the update operation.
        /// </returns>
        public async Task<Result> UpdateAsync(UpdateDriverDto driverDto)
        {
            // Begin transaction
            var transactionResult = await _unitOfWork.StartTransactionAsync();
            if (!transactionResult.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.", true);
            }

            try
            {

                // Validate driver existence in a single call
                var existingDriver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverDto.Id);
                
                if (existingDriver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.NotFound("Driver not found.");
                }

               

                // Step 1: Update driver details
                _mapper.Map(driverDto, existingDriver);

                // Step 2: Update associated user details
                var userUpdateResult = await UpdateUserDetailsAsync(driverDto, existingDriver.UserId);
                if (!userUpdateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure($"Failed to update user details: {userUpdateResult.Errors.FirstOrDefault()}", userUpdateResult.IsException);
                }

                // Step 3: Update or add car details if applicable
                if (driverDto.HasCar)
                {
                    var carUpdateResult = await UpdateOrAddCarDetailsAsync(driverDto, existingDriver);
                    if (!carUpdateResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure($"Failed to update car details: {carUpdateResult.Errors.FirstOrDefault()}", carUpdateResult.IsException);
                    }
                }

                // Step 4: Save all changes to the repository
                _unitOfWork.Repository<Driver>().Update(existingDriver);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save driver updates.");
                }

                // Commit the transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    return Result.Failure("Failed to commit the transaction.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an exception
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Unexpected error occurred during driver update: {ex.Message}", true);
            }
        }

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
            
            var userUpdateResult = await _userManager.UpdateAsync(existingUser);
            if (!userUpdateResult.Succeeded)
            {
                return Result.Failure("Failed to update user details.", true);
            }

            return Result.Success();
        }

        /// <summary>
        /// Updates or adds car details for the driver.
        /// </summary>
        /// <param name="driverDto">The driver DTO with updated car details.</param>
        /// <param name="existingDriver">The existing driver entity.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
        private async Task<Result> UpdateOrAddCarDetailsAsync(UpdateDriverDto driverDto, Driver existingDriver)
        {
            var carDto = _mapper.Map<CarDto>(driverDto);

            if (existingDriver.CarId.HasValue)
            {
                // Update existing car
                carDto.Id = existingDriver.CarId.Value;
                var carUpdateResult = await _carService.UpdateAsync(carDto);
                if (!carUpdateResult.IsSuccess)
                {
                    return Result.Failure("Failed to update car details.");
                }
            }
            else
            {
                // Create a new car and link it to the driver
                var carAddResult = await _carService.AddAsync(carDto);
                if (!carAddResult.IsSuccess)
                {
                    return Result.Failure("Failed to add car details.");
                }
                existingDriver.CarId = carDto.Id;
            }

            // Handle car image updates
            if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
            {
                var carImageResult = await _carService.AddCarImageAsync(new CarImageDto
                {
                    CarId = existingDriver.CarId.Value,
                    ImageUrl = driverDto.CarImageUrl,
                    IsMain = true
                });

                if (!carImageResult.IsSuccess)
                {
                    return Result.Failure("Failed to update car image.");
                }
            }

            return Result.Success();
        }


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
                // Start transaction to ensure atomicity
                var transaction = await _unitOfWork.StartTransactionAsync();
                if (!transaction.IsSuccess)
                {
                    return Result.Failure("Failed to start the transaction.");
                }

                // Retrieve the driver
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                // Step 1: Delete associated car and images if present
                if (driver.CarId.HasValue)
                {
                    var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == driver.CarId);
                    if (car != null)
                    {
                        // Delete car images if any exist
                        var carImages = await _unitOfWork.Repository<CarImage>().GetAllAsync(ci => ci.CarId == car.Id);
                        if (carImages != null && carImages.Any())
                        {
                            _unitOfWork.Repository<CarImage>().DeleteRange(carImages);
                        }

                        // Delete the car
                        _unitOfWork.Repository<Car>().Delete(car);
                        var carSaveResult = await _unitOfWork.SaveChangesAsync();
                        if (!carSaveResult.IsSuccess)
                        {
                            await _unitOfWork.RollbackAsync();
                            return Result.Failure("Failed to delete associated car details.");
                        }
                    }
                }

                // Delete the driver
                _unitOfWork.Repository<Driver>().Delete(driver);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete the driver.");
                }

                // Step 2: Delete the driver and the associated user
                var userDeletionResult = await _userManager.DeleteAsync(await _userManager?.FindByIdAsync(driver?.UserId.ToString()));
                if (!userDeletionResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete associated user.");
                }

                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback transaction on error
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error deleting driver: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks whether a driver exists in the database by their unique identifier.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver.
        /// Must be greater than 0.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// Returns <c>true</c> if the driver exists; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="driverId"/> is less than or equal to 0.
        /// </exception>
        /// <remarks>
        /// This method queries the database asynchronously to determine the existence of the driver.
        /// </remarks>
        public async Task<bool> DoesDriverExistByIdAsync(int driverId, CancellationToken cancellationToken = default)
        {
            if (driverId <= 0)
            {
                return false;
            }

            try
            {
                return await _unitOfWork.Repository<Driver>()
                                        .Table
                                        .AnyAsync(x => x.Id == driverId, cancellationToken);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
