using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new driver, including personal information, car details, and image uploads.
    /// </summary>
    public class AddDriverDto
    {
        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        // Driver Info
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string? Bio { get; set; }
        public byte YearsOfExperience { get; set; }


        // Image Uploads
        public IFormFile? ProfileImage { get; set; }
        public IFormFile LicenseImage { get; set; } = null!;
        public IFormFile PassportImage { get; set; } = null!;


        // Pricing
        public decimal? PricePerHour { get; set; } // Driver's hourly rate

        // Availability (Storing Only Date)
        public DateTime? AvailableFrom { get; set; } // When the driver starts being available
        public DateTime? AvailableTo { get; set; }   // When the driver's availability ends
        public bool IsAvailableAllTime { get; set; } = false; // If true, driver ignores date restrictions

        // Car Details


        public bool HasCar { get; set; } = false;
        public AddCarDto? Car { get; set; } 




        // Cities and Countries worked on and covered
        public List<int> CitiesWorkedOn { get; set; } = new List<int>();
        public List<int> CitiesCoverNow { get; set; } = new List<int>();
        public List<int> CountriesWorkedOn { get; set; } = new List<int>();
        public List<int> CountriesCoverNow { get; set; } = new List<int>();
        public List<int> LanguagesSpoken { get; set; } = new List<int>();
    }
}
