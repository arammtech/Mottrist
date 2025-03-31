using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Domain.Entities.CarDetails;
using Feature.Car.DTOs;

namespace Mottrist.Service.Features.Drivers.Mappers
{
    /// <summary>
    /// Provides mapping functionality for Driver-related objects.
    /// </summary>
    public static class DriverMapper
    {
        /// <summary>
        /// Maps an <see cref="AddDriverDto"/> to a <see cref="Driver"/> entity.
        /// </summary>
        /// <param name="dto">The DTO containing driver details.</param>
        /// <returns>A <see cref="Driver"/> entity.</returns>
        public static Driver MapToDriver(AddDriverDto dto)
        {
            return new Driver
            {
                WhatsAppNumber = dto.WhatsAppNumber,
                NationalityId = dto.NationalityId,
                LicenseImageUrl = dto.LicenseImageUrl,
                YearsOfExperience = dto.YearsOfExperience,
                Bio = dto.Bio,
                ProfileImageUrl = dto.ProfileImageUrl,
                PassportImageUrl = dto.PassportImageUrl,
                CarId = null // Car will be linked later if applicable
            };
        }

        /// <summary>
        /// Maps a <see cref="Driver"/> entity to a <see cref="DriverDto"/>.
        /// </summary>
        /// <param name="entity">The Driver entity from the database.</param>
        /// <returns>A <see cref="DriverDto"/> object.</returns>
        public static DriverDto MapToDriverDto(Driver entity)
        {
            return new DriverDto
            {
                Id = entity.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Email = entity.User.Email ?? string.Empty,
                PhoneNumber = entity.User.PhoneNumber,
                WhatsAppNumber = entity.WhatsAppNumber,
                Nationality = entity.Country.Name, // Assuming Country has a Name property
                LicenseImageUrl = entity.LicenseImageUrl,
                YearsOfExperience = entity.YearsOfExperience,
                Bio = entity.Bio,
                PassportImageUrl = entity.PassportImageUrl,
                ProfileImageUrl = entity.ProfileImageUrl,
                HasCar = entity.CarId.HasValue,
                CarBrand = entity.Car?.Brand?.Name,
                CarModel = entity.Car?.Model?.Name,
                CarYear = entity.Car?.Year,
                CarNumberOfSeats = entity.Car?.NumberOfSeats,
                CarColor = entity.Car?.Color?.Name,
                CarBodyType = entity.Car?.BodyType?.Type,
                CarFuelType = entity.Car?.FuelType?.Type,
                MainCarImageUrl = entity.Car?.CarImages.FirstOrDefault(x => x.IsMain)?.ImageUrl
            };
        }

        /// <summary>
        /// Updates an existing <see cref="Driver"/> entity using a <see cref="UpdateDriverDto"/>.
        /// </summary>
        /// <param name="dto">The DTO containing updated driver details.</param>
        /// <param name="entity">The existing Driver entity to update.</param>
        public static void UpdateDriverFromDto(UpdateDriverDto dto, Driver entity)
        {
            entity.WhatsAppNumber = dto.WhatsAppNumber;
            entity.NationalityId = dto.NationalityId;
            entity.LicenseImageUrl = dto.LicenseImageUrl;
            entity.YearsOfExperience = dto.YearsOfExperience;
            entity.Bio = dto.Bio;
            entity.ProfileImageUrl = dto.ProfileImageUrl;
            entity.PassportImageUrl = dto.PassportImageUrl;
        }

        /// <summary>
        /// Maps an <see cref="UpdateDriverDto"/> to a <see cref="Driver"/> entity for updates.
        /// </summary>
        /// <param name="dto">The DTO containing updated driver details.</param>
        /// <returns>A mapped <see cref="Driver"/> entity.</returns>
        public static Driver MapToDriver(UpdateDriverDto dto)
        {
            return new Driver
            {
                WhatsAppNumber = dto.WhatsAppNumber,
                NationalityId = dto.NationalityId,
                LicenseImageUrl = dto.LicenseImageUrl,
                YearsOfExperience = dto.YearsOfExperience,
                Bio = dto.Bio,
                ProfileImageUrl = dto.ProfileImageUrl,
                PassportImageUrl = dto.PassportImageUrl
            };
        }

        /// <summary>
        /// Maps a <see cref="Driver"/> entity to a lightweight <see cref="DriverDto"/> for display.
        /// </summary>
        /// <param name="entity">The Driver entity.</param>
        /// <returns>A simplified <see cref="DriverDto"/> object.</returns>
        public static DriverDto MapToSimpleDriverDto(Driver entity)
        {
            return new DriverDto
            {
                Id = entity.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Email = entity.User.Email ?? string.Empty,
                PhoneNumber = entity.User.PhoneNumber
            };
        }

        /// <summary>
        /// Maps a <see cref="AddDriverDto"/> to an <see cref="ApplicationUser"/> entity.
        /// </summary>
        /// <param name="driverDto">The DTO containing user details.</param>
        /// <returns>An <see cref="ApplicationUser"/> entity.</returns>
        public static ApplicationUser MapToApplicationUser(AddDriverDto driverDto)
        {
            return new ApplicationUser
            {
                Email = driverDto.Email,
                UserName = driverDto.Email,
                FirstName = driverDto.FirstName,
                LastName = driverDto.LastName,
                PhoneNumber = driverDto.PhoneNumber
            };
        }

        /// <summary>
        /// Maps an <see cref="UpdateDriverDto"/> to an <see cref="ApplicationUser"/> entity.
        /// </summary>
        /// <param name="driverDto">The DTO containing updated user details.</param>
        /// <param name="user">The existing ApplicationUser entity to update.</param>
        public static void UpdateApplicationUserFromDto(UpdateDriverDto driverDto, ApplicationUser user)
        {
            user.FirstName = driverDto.FirstName;
            user.LastName = driverDto.LastName;
            user.PhoneNumber = driverDto.PhoneNumber;
        }

        #region CarMapping
        /// <summary>
        /// Maps a <see cref="AddDriverDto"/> to a <see cref="Driver"/> entity, linking car details.
        /// </summary>
        /// <param name="dto">The DTO containing driver and car details.</param>
        /// <returns>A <see cref="Driver"/> entity with associated car details if available.</returns>
        public static Driver MapToDriverWithCar(AddDriverDto dto)
        {
            // Map the driver entity
            var driver = new Driver
            {
                WhatsAppNumber = dto.WhatsAppNumber,
                NationalityId = dto.NationalityId,
                LicenseImageUrl = dto.LicenseImageUrl,
                YearsOfExperience = dto.YearsOfExperience,
                Bio = dto.Bio,
                ProfileImageUrl = dto.ProfileImageUrl,
                PassportImageUrl = dto.PassportImageUrl,
                CarId = null // This will be set when a car entity is linked
            };

            // Map car details if the driver has a car
            if (dto.HasCar)
            {
                var car = new Car
                {
                    BrandId = dto.BrandId ?? 0,
                    ModelId = dto.ModelId ?? 0,
                    Year = dto.Year ?? 0,
                    NumberOfSeats = dto.NumberOfSeats ?? 0,
                    ColorId = dto.ColorId ?? 0,
                    BodyTypeId = dto.BodyTypeId ?? 0,
                    FuelTypeId = dto.FuelTypeId ?? 0
                };
                // Attach the car to the driver
                driver.Car = car;
            }

            return driver;
        }

        /// <summary>
        /// Updates the <see cref="Driver"/> entity with car details using a <see cref="UpdateDriverDto"/>.
        /// </summary>
        /// <param name="dto">The DTO containing updated driver and car details.</param>
        /// <param name="driver">The existing Driver entity.</param>
        public static void UpdateDriverWithCar(UpdateDriverDto dto, Driver driver)
        {
            driver.WhatsAppNumber = dto.WhatsAppNumber;
            driver.NationalityId = dto.NationalityId;
            driver.LicenseImageUrl = dto.LicenseImageUrl;
            driver.YearsOfExperience = dto.YearsOfExperience;
            driver.Bio = dto.Bio;
            driver.ProfileImageUrl = dto.ProfileImageUrl;
            driver.PassportImageUrl = dto.PassportImageUrl;

            // Update car details if applicable
            if (dto.HasCar)
            {
                if (driver.Car == null)
                {
                    driver.Car = new Car(); // Create a new car if one doesn't exist
                }

                driver.Car.BrandId = dto.BrandId ?? 0;
                driver.Car.ModelId = dto.ModelId ?? 0;
                driver.Car.Year = dto.Year ?? 0;
                driver.Car.NumberOfSeats = dto.NumberOfSeats ?? 0;
                driver.Car.ColorId = dto.ColorId ?? 0;
                driver.Car.BodyTypeId = dto.BodyTypeId ?? 0;
                driver.Car.FuelTypeId = dto.FuelTypeId ?? 0;
            }
        }

        #endregion
    }
}
