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

namespace Mottrist.Service.Features.Drivers.Services
{
    public class DriverService :BaseService ,IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
        public async Task<ISet<DriverDto>?> GetAllAsync(Expression<Func<DriverDto, bool>>? filter = null)
        {
            try
            {
                // Build the base query
                var driverQuery = from driver in _unitOfWork.Repository<Driver>().Table.AsQueryable()
                                  join user in _unitOfWork.Repository<ApplicationUser>().Table
                                  on driver.UserId equals user.Id
                                  join country in _unitOfWork.Repository<Country>().Table
                                  on driver.NationailtyId equals country.Id into countryGroup
                                  from countryDetails in countryGroup.DefaultIfEmpty()
                                  join car in _unitOfWork.Repository<Car>().Table
                                  on driver.CarId equals car.Id into carGroup
                                  from carDetails in carGroup.DefaultIfEmpty()
                                  select new DriverDto
                                  {
                                      Id = driver.Id,
                                      WhatsAppNumber = driver.WhatsAppNumber,
                                      Nationailty = countryDetails.Name ?? "Unknown",
                                      LicenseImageUrl = driver.LicenseImageUrl,
                                      YearsOfExperience = driver.YearsOfExperience,
                                      Bio = driver.Bio,
                                      PassportImageUrl = driver.PassportImageUrl,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
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

                // Execute the query and return the result as a set
                return await driverQuery.ToHashSetAsync();
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
                        on driver.NationailtyId equals country.Id into countryGroup
                    from countryDetails in countryGroup.DefaultIfEmpty()
                    join car in _unitOfWork.Repository<Car>().Table
                        on driver.CarId equals car.Id into carGroup
                    from carDetails in carGroup.DefaultIfEmpty()
                    where driver.Id == driverId
                    select new DriverDto
                    {
                        Id = driver.Id,
                        WhatsAppNumber = driver.WhatsAppNumber,
                        Nationailty = countryDetails.Name ?? "Unknown",
                        LicenseImageUrl = driver.LicenseImageUrl,
                        YearsOfExperience = driver.YearsOfExperience,
                        Bio = driver.Bio,
                        PassportImageUrl = driver.PassportImageUrl,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
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
                        CarImageUrl = carDetails.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl
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
        public async Task<Result> AddAsync(AddUpdateDriverDto driverDto)
        {
            Result result = new Result();

            // Start a transaction
            var transaction = await _unitOfWork.StartTransactionAsync();
            if (!transaction.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.");
            }

            try
            {
                // Step 1: Map and create the user
                var user = _mapper.Map<ApplicationUser>(driverDto);
                var addUserResult = await _userManager.CreateAsync(user);

                if (!addUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();

                    if(addUserResult?.Errors != null)
                    {
                    
                        foreach (var error in addUserResult.Errors)
                        {
                            result.AddError(error.Description);
                        }

                        return result;
                    }

                    return Result.Failure("Failed to with database when add user.");
                }

                // Step 2: Assign the 'Driver' role to the user
                var roleResult = await _userManager.AddToRoleAsync(user, AppUserRoles.RoleDriver);
                if (!roleResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();

                    if (roleResult?.Errors != null)
                    {

                        foreach (var error in addUserResult.Errors)
                        {
                            result.AddError(error.Description);
                        }

                        return result;
                    }
                    
                    return Result.Failure("Failed to assign the driver role to the user.");
                } 

                // Step 3: Map and create the driver entity
                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id; // Link the user to the driver

                // Check if the driver has a car
                if (driverDto.HasCar)
                {
                    // Map and create the car entity
                    var carEntity = _mapper.Map<Car>(driverDto);

                    await _unitOfWork.Repository<Car>().AddAsync(carEntity);
                    var carSaveResult = await _unitOfWork.SaveChangesAsync();
                    if (!carSaveResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure("Failed to save the car.");
                    }

                    // Handle the car image creation if applicable
                    if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
                    {
                        var carImageEntity = new CarImage
                        {
                            CarId = carEntity.Id,
                            ImageUrl = driverDto.CarImageUrl,
                            IsMain = true
                        };

                        await _unitOfWork.Repository<CarImage>().AddAsync(carImageEntity);
                        var carImageSaveResult = await _unitOfWork.SaveChangesAsync();
                        if (!carImageSaveResult.IsSuccess)
                        {
                            await _unitOfWork.RollbackAsync();
                            return Result.Failure("Failed to save the car image.");
                        }
                    }

                    // Link the car to the driver
                    driverEntity.CarId = carEntity.Id;
                }

                // Step 4: Add the driver entity to the database
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver.");
                }

                // Commit the transaction
                await _unitOfWork.CommitAsync();

                // Confirm the driver was saved and update the DTO with the generated ID
                if (driverEntity.Id <= 0)
                {
                    return Result.Failure("Driver ID was not generated successfully.");
                }
                driverDto.Id = driverEntity.Id;

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback transaction on error
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error adding driver: {ex.Message}",true);
            }
        }

        /// <summary>
        /// Updates an existing driver in the system, including associated car details if applicable.
        /// </summary>
        /// <param name="driverDto">The DTO containing updated driver and car details.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the update operation.
        /// </returns>
        public async Task<Result> UpdateAsync(AddUpdateDriverDto driverDto)
        {
            // Start a transaction
            var transaction = await _unitOfWork.StartTransactionAsync();
            if (!transaction.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction.");
            }

            try
            {
                // Fetch the existing driver
                var existingDriver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverDto.Id);
                if (existingDriver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                // Update driver entity with new details
                _mapper.Map(driverDto, existingDriver);

                // Step 1: Update associated user details
                var existingUser = await _userManager.FindByIdAsync(existingDriver.UserId.ToString());
                if (existingUser == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Associated user not found.");
                }

                _mapper.Map(driverDto, existingUser); // Map updated user details
                var userUpdateResult = await _userManager.UpdateAsync(existingUser);
                if (!userUpdateResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update user details.");
                }

                // Step 2: Check and update car details if applicable
                if (driverDto.HasCar)
                {
                    var carEntity = _mapper.Map<Car>(driverDto);

                    if (existingDriver.CarId.HasValue)
                    {
                        // Update existing car
                        var existingCar = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == existingDriver.CarId);
                        if (existingCar != null)
                        {
                            _mapper.Map(driverDto, existingCar);
                            _unitOfWork.Repository<Car>().Update(existingCar);
                        }
                    }
                    else
                    {
                        // Create a new car and link it to the driver
                        await _unitOfWork.Repository<Car>().AddAsync(carEntity);
                        existingDriver.CarId = carEntity.Id;
                    }

                    var carSaveResult = await _unitOfWork.SaveChangesAsync();
                    if (!carSaveResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure("Failed to update or create car details.");
                    }

                    // Step 3: Handle car image updates
                    if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
                    {
                        var existingCarImage = await _unitOfWork.Repository<CarImage>().GetAsync(ci => ci.CarId == carEntity.Id && ci.IsMain);
                        if (existingCarImage != null)
                        {
                            existingCarImage.ImageUrl = driverDto.CarImageUrl;
                            _unitOfWork.Repository<CarImage>().Update(existingCarImage);
                        }
                        else
                        {
                            var carImageEntity = new CarImage
                            {
                                CarId = carEntity.Id,
                                ImageUrl = driverDto.CarImageUrl,
                                IsMain = true
                            };
                            await _unitOfWork.Repository<CarImage>().AddAsync(carImageEntity);
                        }

                        var carImageSaveResult = await _unitOfWork.SaveChangesAsync();
                        if (!carImageSaveResult.IsSuccess)
                        {
                            await _unitOfWork.RollbackAsync();
                            return Result.Failure("Failed to update or create car image.");
                        }
                    }
                }

                // Step 4: Save updated driver data
                _unitOfWork.Repository<Driver>().Update(existingDriver);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update driver details.");
                }

                // Commit the transaction
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction on error
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error updating driver: {ex.Message}");
            }
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

                // Step 2: Delete the driver and the associated user
                var userDeletionResult = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(driver.UserId.ToString()));
                if (!userDeletionResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete associated user.");
                }

                // Delete the driver
                _unitOfWork.Repository<Driver>().Delete(driver);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete the driver.");
                }

                return await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction on error
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error deleting driver: {ex.Message}");
            }
        }



    }
}
