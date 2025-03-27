using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Domain.Common.IUnitOfWork;
using AutoMapper;
using Mottrist.Domain.Global;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Mottrist.Service.Features.User.Inerfaces;
using Mottrist.Utilities.Identity;
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Domain.LookupEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Drivers.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<HashSet<DriverDto>> GetAllAsync()
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
                                      UserName = user.UserName,
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

 

                return await driverQuery.ToHashSetAsync();
            }
            catch (Exception ex)
            {
                // Log exception (replace with actual logging)
                Console.WriteLine($"Error retrieving drivers: {ex.Message}");
                return new();
            }
        }

        public async Task<DriverDto?> GetByIdAsync(int driverId)
        {
            if (driverId <= 0)
                return null;

            try
            {
                Driver? driverEntity = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverId);
                if (driverEntity == null)
                    return null;
                // Query driver, user, and nationality (country) first
                var driverQuery = await (
                    from driver in _unitOfWork.Repository<Driver>().Table.AsQueryable()
                    join user in _unitOfWork.Repository<ApplicationUser>().Table
                    on driver.UserId equals user.Id
                    join country in _unitOfWork.Repository<Country>().Table
                    on driver.NationailtyId equals country.Id into countryGroup
                    from countryDetails in countryGroup.DefaultIfEmpty()
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
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImageUrl = driver.ProfileImageUrl,
                        HasCar = driver.CarId != null
                    }
                ).FirstOrDefaultAsync();

                if (driverQuery == null)
                    return null;

                // Query car details only if the driver has a car
                if (driverQuery.HasCar)
                {
                    if(driverEntity.CarId <= 0)
                        return null;

   
                    var carDetails = _unitOfWork.Repository<Car>().Table
                        .Include(c => c.Brand)
                        .Include(c => c.Model)
                        .Include(c => c.Color)
                        .Include(c => c.BodyType)
                        .Include(c => c.FuelType)
                        .Include(c => c.CarImages)
                        .Where(c => c.Id == driverEntity.CarId)
                        .Select(c => new
                        {
                            Brand = c.Brand.Name,
                            Year = c.Year,
                            NumberOfSeats = c.NumberOfSeats,
                            Model = c.Model.Name,
                            Color = c.Color.Name,
                            BodyType = c.BodyType.Type,
                            FuelType = c.FuelType.Type,
                            CarImageUrl = c.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl
                        }).FirstOrDefault();

                    if (carDetails != null)
                    {
                        driverQuery.CarBrand = carDetails.Brand;
                        driverQuery.CarYear = carDetails.Year;
                        driverQuery.CarNumberOfSeats = carDetails.NumberOfSeats;
                        driverQuery.CarModel = carDetails.Model;
                        driverQuery.CarColor = carDetails.Color;
                        driverQuery.CarBodyType = carDetails.BodyType;
                        driverQuery.CarFuelType = carDetails.FuelType;
                        driverQuery.CarImageUrl = carDetails.CarImageUrl;
                    }
                }

                return driverQuery;
            }
            catch (Exception ex)
            {
                // Log exception (use your logging framework of choice)
                Console.WriteLine($"Error retrieving driver data: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        /// Adds a new driver, along with an associated car and image if applicable.
        /// </summary>
        public async Task<Result> AddAsync(AddUpdateDriverDto driverDto)
        {
            var transaction = await _unitOfWork.StartTransactionAsync();
            if (!transaction.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction");
            }
            try
            { 

                ApplicationUser user = _mapper.Map<ApplicationUser>(driverDto);
                var addUserResult = await _userManager.CreateAsync(user);

                if (!addUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    Result.Failure("Failed to save the driver to the database.");
                }

                // Assign role to user
                var roleResult = await _userManager.AddToRoleAsync(user, AppUserRoles.RoleDriver);
                if (!roleResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to add the role to the user");
                }
                // Step 2: Create a Driver entity and link it to the ApplicationUser
                var driverEntity = _mapper.Map<Driver>(driverDto);
                driverEntity.UserId = user.Id; // Link the User to the Driver

                // Check if the driver has a car
                if (driverDto.HasCar)
                {
                    // Create a Car entity
                     var carEntity = _mapper.Map<Car>(driverDto);


                    // Add car to the database
                    await _unitOfWork.Repository<Car>().AddAsync(carEntity);
                    var carSaveResult = await _unitOfWork.SaveChangesAsync();
                    if (!carSaveResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure("Failed to save the car.");
                    }

                    // Handle car image creation
                    if (string.IsNullOrEmpty(driverDto.CarImageUrl))
                    {
                        var carImageEntity = new CarImage
                        {
                            CarId = carEntity.Id,
                            ImageUrl = driverDto.CarImageUrl,
                            IsMain = true
                        };

                        // Add car image to the database
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

                // Add Driver to the database
                await _unitOfWork.Repository<Driver>().AddAsync(driverEntity);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save the driver.");
                }

                // Commit the transaction

                await _unitOfWork.CommitAsync();

                if(driverEntity.Id <= 0)
                {
                    return Result.Failure("Failed to save the driver.");
                }
                
                driverDto.Id = driverEntity.Id;

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error adding driver: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing driver, along with associated car details if applicable.
        /// </summary>
        public async Task<Result> UpdateAsync(AddUpdateDriverDto driverDto)
        {
            var transaction = await _unitOfWork.StartTransactionAsync();
            if (!transaction.IsSuccess)
            {
                return Result.Failure("Failed to start the transaction");
            }
            try
            {
                // Step 1: Map the Driver entity
                var driverEntity = _mapper.Map<Driver>(driverDto);

                // Fetch the existing driver
                var existingDriver = await _unitOfWork.Repository<Driver>().GetAsync(d => d.Id == driverDto.Id);
                if (existingDriver == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Driver not found.");
                }

                // Update driver details using mapped data
                _mapper.Map(driverDto, existingDriver);

                // Step 2: Update ApplicationUser using UserManager
                var user = _mapper.Map<ApplicationUser>(driverDto);
                var existingUser = await _userManager.FindByIdAsync(existingDriver.UserId.ToString());
                if (existingUser == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Associated user not found.");
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.UserName = user.UserName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.PasswordHash = user.PasswordHash;

                var userUpdateResult = await _userManager.UpdateAsync(existingUser);
                if (!userUpdateResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update user data.");
                }

                // Step 3: Check if the driver has a car
                if (driverDto.HasCar || existingDriver.CarId != null)
                {
                    var carEntity = _mapper.Map<Car>(driverDto);

                    if (existingDriver.CarId > 0)
                    {
                        // Fetch the existing car
                        var existingCar = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == existingDriver.CarId);
                        if (existingCar != null)
                        {
                            _mapper.Map(driverDto, existingCar);
                            existingCar.Id = existingDriver.CarId.Value;
                            _unitOfWork.Repository<Car>().Update(existingCar);
                        }
                    }
                    else
                    {
                        // Create a new car if no car is associated with the driver
                        await _unitOfWork.Repository<Car>().AddAsync(carEntity);
                        existingDriver.CarId = carEntity.Id; // Link the car to the driver
                    }

                    var carSaveResult = await _unitOfWork.SaveChangesAsync();
                    if (!carSaveResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result.Failure("Failed to update or create the car.");
                    }

                    // Step 4: Handle car image creation
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
                            return Result.Failure("Failed to update or create the car image.");
                        }
                    }
                }

                // Step 5: Save updated driver data
                _unitOfWork.Repository<Driver>().Update(existingDriver);
                var driverSaveResult = await _unitOfWork.SaveChangesAsync();
                if (!driverSaveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update driver.");
                }
                
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error updating driver: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(int driverId)
        {
            try
            {
                // Retrieve the driver based on the filter
                var driver = await _unitOfWork.Repository<Driver>().GetAsync(x=> x.Id == driverId);
                if (driver == null)
                    return Result.Failure("Driver not found.");

                // Delete associated car if it exists
                if (driver.CarId != null)
                {
                    var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == driver.CarId);
                    if (car != null)
                    {
                        if (car.CarImages != null && car.CarImages.Any())
                        {
                            _unitOfWork.Repository<CarImage>().DeleteRange(car.CarImages);
                        }

                        _unitOfWork.Repository<Car>().Delete(car);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                int UserId = driver.UserId;
                // Delete the driver
                _unitOfWork.Repository<Driver>().Delete(driver);
              var result =  await _userManager.DeleteAsync(await _userManager.FindByIdAsync(UserId.ToString()));

                return  result.Succeeded ? Result.Success() : Result.Failure("Didnt Delete the driver");
            }
            catch (Exception ex)
            {
                // Log exception (replace with actual logging)
                Console.WriteLine($"Error deleting driver: {ex.Message}");
                return Result.Failure($"Error deleting driver: {ex.Message}");
            }
        }


    }
}
