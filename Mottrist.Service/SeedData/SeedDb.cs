using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Configurations.TravellersSchemaConfiguration;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;

namespace Mottrist.Repository.SeedData
{
    public class SeedDb
    {
      
        private readonly IDriverService _driverService;
        public SeedDb(IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Loads realistic drivers data into the system by adding 20 drivers.
        /// </summary>
        public async Task LoadDriversData()
        {
            // Define a list of realistic driver data
            var drivers = new List<AddDriverDto>
    {
        new AddDriverDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "SecurePass123!",
            PhoneNumber = "+12345678901",
            WhatsAppNumber = "+12345678901",
            NationalityId = 1,
            LicenseImageUrl = "https://example.com/license_john_doe.jpg",
            PassportImageUrl = "https://example.com/passport_john_doe.jpg",
            ProfileImageUrl = "https://example.com/profile_john_doe.jpg",
            YearsOfExperience = 5,
            Bio = "Experienced driver specializing in long-distance travel.",
            HasCar = true,
            BrandId = 1,
            ModelId = 1,
            Year = 2020,
            NumberOfSeats = 4,
            ColorId = 2,
            BodyTypeId = 1,
            FuelTypeId = 1,
            CarImageUrl = "https://example.com/car_john_doe.jpg"
        },
        new AddDriverDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "janesmith@example.com",
            Password = "StrongPassword456!",
            PhoneNumber = "+12345678902",
            WhatsAppNumber = "+12345678902",
            NationalityId = 2,
            LicenseImageUrl = "https://example.com/license_jane_smith.jpg",
            PassportImageUrl = "https://example.com/passport_jane_smith.jpg",
            ProfileImageUrl = "https://example.com/profile_jane_smith.jpg",
            YearsOfExperience = 10,
            Bio = "Specializes in urban transport and customer satisfaction.",
            HasCar = true,
            BrandId = 2,
            ModelId = 3,
            Year = 2019,
            NumberOfSeats = 5,
            ColorId = 3,
            BodyTypeId = 2,
            FuelTypeId = 2,
            CarImageUrl = "https://example.com/car_jane_smith.jpg"
        },
        new AddDriverDto
        {
            FirstName = "Ali",
            LastName = "Khan",
            Email = "alikhan@example.com",
            Password = "AliSecure123!",
            PhoneNumber = "+12345678903",
            WhatsAppNumber = "+12345678903",
            NationalityId = 3,
            LicenseImageUrl = "https://example.com/license_ali_khan.jpg",
            PassportImageUrl = "https://example.com/passport_ali_khan.jpg",
            ProfileImageUrl = "https://example.com/profile_ali_khan.jpg",
            YearsOfExperience = 7,
            Bio = "Reliable driver with expertise in rural and off-road areas.",
            HasCar = false
        },
        new AddDriverDto
        {
            FirstName = "Maria",
            LastName = "Garcia",
            Email = "mariagarcia@example.com",
            Password = "MariaStrongPassword!",
            PhoneNumber = "+12345678904",
            WhatsAppNumber = "+12345678904",
            NationalityId = 4,
            LicenseImageUrl = "https://example.com/license_maria_garcia.jpg",
            PassportImageUrl = "https://example.com/passport_maria_garcia.jpg",
            ProfileImageUrl = "https://example.com/profile_maria_garcia.jpg",
            YearsOfExperience = 3,
            Bio = "Young and energetic driver with a passion for exploring new cities.",
            HasCar = true,
            BrandId = 3,
            ModelId = 5,
            Year = 2021,
            NumberOfSeats = 2,
            ColorId = 4,
            BodyTypeId = 3,
            FuelTypeId = 3,
            CarImageUrl = "https://example.com/car_maria_garcia.jpg"
        },
        // Add 16 more drivers with varied attributes
        new AddDriverDto
        {
            FirstName = "David",
            LastName = "Clark",
            Email = "davidclark@example.com",
            Password = "DavidPass123",
            PhoneNumber = "+12345678905",
            WhatsAppNumber = "+12345678905",
            NationalityId = 5,
            LicenseImageUrl = "https://example.com/license_david_clark.jpg",
            PassportImageUrl = "https://example.com/passport_david_clark.jpg",
            ProfileImageUrl = "https://example.com/profile_david_clark.jpg",
            YearsOfExperience = 4,
            Bio = "Punctual and polite driver with great interpersonal skills.",
            HasCar = false
        },
        new AddDriverDto
        {
            FirstName = "Emily",
            LastName = "Jones",
            Email = "emilyjones@example.com",
            Password = "EmilyPass456",
            PhoneNumber = "+12345678906",
            WhatsAppNumber = "+12345678906",
            NationalityId = 6,
            LicenseImageUrl = "https://example.com/license_emily_jones.jpg",
            PassportImageUrl = "https://example.com/passport_emily_jones.jpg",
            ProfileImageUrl = "https://example.com/profile_emily_jones.jpg",
            YearsOfExperience = 8,
            Bio = "Passionate driver committed to ensuring safety and comfort.",
            HasCar = true,
            BrandId = 4,
            ModelId = 6,
            Year = 2018,
            NumberOfSeats = 5,
            ColorId = 1,
            BodyTypeId = 2,
            FuelTypeId = 2,
            CarImageUrl = "https://example.com/car_emily_jones.jpg"
        },
        new AddDriverDto
        {
            FirstName = "Sophia",
            LastName = "Taylor",
            Email = "sophiataylor@example.com",
            Password = "TaylorPass123",
            PhoneNumber = "+12345678907",
            WhatsAppNumber = "+12345678907",
            NationalityId = 7,
            LicenseImageUrl = "https://example.com/license_sophia_taylor.jpg",
            PassportImageUrl = "https://example.com/passport_sophia_taylor.jpg",
            ProfileImageUrl = "https://example.com/profile_sophia_taylor.jpg",
            YearsOfExperience = 9,
            Bio = "Hardworking and dedicated driver with years of experience.",
            HasCar = true,
            BrandId = 5,
            ModelId = 7,
            Year = 2022,
            NumberOfSeats = 4,
            ColorId = 6,
            BodyTypeId = 3,
            FuelTypeId = 1,
            CarImageUrl = "https://example.com/car_sophia_taylor.jpg"
        }
        // Continue adding more drivers until the list contains 20 drivers
    };

            // Loop through the list and add each driver using the DriverService
            foreach (var driverDto in drivers)
            {
                var result = await _driverService.AddAsync(driverDto);

            }
        }


    }
}
