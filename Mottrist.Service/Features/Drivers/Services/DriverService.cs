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

namespace Mottrist.Service.Features.Drivers.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Adds a new driver, along with an associated car and image if applicable.
        /// </summary>
        public async Task<Result> AddDriverAsync(AddUpdateDriverDto driverDto)
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
        public async Task<Result> UpdateDriverAsync(AddUpdateDriverDto driverDto)
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

                // Commit the transaction
                await _unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error updating driver: {ex.Message}");
            }
        }

    }
}
