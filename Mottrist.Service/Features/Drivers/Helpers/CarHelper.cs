using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Drivers.DTOs;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Drivers.Helpers
{
    /// <summary>
    /// Provides helper methods for managing car and car-related details for drivers.
    /// </summary>
    public static class CarHelper
    {
        ///// <summary>
        ///// Adds a car to the system using CarService, along with its image if provided.
        ///// </summary>
        ///// <param name="carService">The car service for managing car operations.</param>
        ///// <param name="driverDto">The driver DTO containing car details.</param>
        ///// <param name="driverEntity">The driver entity to link the car to.</param>
        ///// <returns>A <see cref="Result"/> indicating success or failure of the operation.</returns>
        //public static async Task<Result> AddCarWithCarServiceAsync(
        //    ICarService carService,
        //    AddDriverDto driverDto,
        //    Driver driverEntity)
        //{
        //    // Map car details to CarDto using DriverMapper
        //    var carDto = DriverMapper.MapToCarDto(driverDto);

        //    // Add the car using the CarService
        //    var carAddResult = await carService.AddAsync(carDto);
        //    if (!carAddResult.IsSuccess)
        //    {
        //        return Result.Failure("Failed to add the car details.", true);
        //    }

        //    // Update the driver's CarId with the newly created car's ID
        //    driverEntity.CarId = carDto.Id;

        //    // Handle car image creation using CarService
        //    if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
        //    {
        //        var carImageDto = new CarImageDto
        //        {
        //            CarId = carDto.Id,
        //            ImageUrl = driverDto.CarImageUrl,
        //            IsMain = true
        //        };

        //        var carImageResult = await carService.AddCarImageAsync(carImageDto);
        //        if (!carImageResult.IsSuccess)
        //        {
        //            return Result.Failure("Failed to add the car image.", true);
        //        }
        //    }

        //    return Result.Success();
        //}

        ///// <summary>
        ///// Updates or adds car details for the driver.
        ///// </summary>
        ///// <param name="carService">The car service for managing car operations.</param>
        ///// <param name="driverDto">The driver DTO with updated car details.</param>
        ///// <param name="existingDriver">The existing driver entity.</param>
        ///// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        //public static async Task<Result> UpdateOrAddCarDetailsAsync(
        //    ICarService carService,
        //    UpdateDriverDto driverDto,
        //    Driver existingDriver)
        //{
        //    // Map updated car details to CarDto
        //    var carDto = m (driverDto);

        //    if (existingDriver.CarId.HasValue)
        //    {
        //        // Update existing car
        //        carDto.Id = existingDriver.CarId.Value;
        //        var carUpdateResult = await carService.UpdateAsync(carDto);
        //        if (!carUpdateResult.IsSuccess)
        //        {
        //            return Result.Failure("Failed to update car details.");
        //        }
        //    }
        //    else
        //    {
        //        // Add new car
        //        var carAddResult = await carService.AddAsync(carDto);
        //        if (!carAddResult.IsSuccess)
        //        {
        //            return Result.Failure("Failed to add car details.");
        //        }
        //        existingDriver.CarId = carDto.Id;
        //    }

        //    // Handle car image updates
        //    if (!string.IsNullOrEmpty(driverDto.CarImageUrl))
        //    {
        //        var carImageResult = await carService.AddCarImageAsync(new CarImageDto
        //        {
        //            CarId = existingDriver.CarId.Value,
        //            ImageUrl = driverDto.CarImageUrl,
        //            IsMain = true
        //        });

        //        if (!carImageResult.IsSuccess)
        //        {
        //            return Result.Failure("Failed to update car image.");
        //        }
        //    }

        //    return Result.Success();
        //}
    }
}
