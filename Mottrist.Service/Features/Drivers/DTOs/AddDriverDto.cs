using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new driver, including personal information, car details, and image uploads.
    /// </summary>
    public class AddDriverDto
    {
        // Identification
        public int Id { get; set; }

        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string? Bio { get; set; }

        // Driver Info Images URLs
        public string? ProfileImageUrl { get; set; }
        public string? LicenseImageUrl { get; set; }
        public string? PassportImageUrl { get; set; }

        // Image Uploads
        public IFormFile? ProfileImage { get; set; }
        public IFormFile LicenseImage { get; set; } = null!;
        public IFormFile PassportImage { get; set; } = null!;

        // Professional Information
        public byte YearsOfExperience { get; set; }

        // Pricing
        public decimal? PricePerHour { get; set; } // Driver's hourly rate

        // Availability (Storing Only Date)
        public DateTime? AvailableFrom { get; set; } // When the driver starts being available
        public DateTime? AvailableTo { get; set; }   // When the driver's availability ends
        public bool IsAvailableAllTime { get; set; } = false; // If true, driver ignores date restrictions

        // Car Details
        public bool HasCar { get; set; } = false;
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? Year { get; set; }
        public byte? NumberOfSeats { get; set; }
        public int? ColorId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public List<string>? CarImagesUrl { get; set; }
        public List<IFormFile>? CarImages { get; set; }

        // Cities and Countries worked on and covered
        public List<int> CitiesWorkedOn { get; set; } = new List<int>();
        public List<int> CitiesCoverNow { get; set; } = new List<int>();
        public List<int> CountriesWorkedOn { get; set; } = new List<int>();
        public List<int> CountriesCoverNow { get; set; } = new List<int>();
        public List<int> LanguagesSpoken { get; set; } = new List<int>();
    }
}
