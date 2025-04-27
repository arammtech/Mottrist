using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating driver information, including optional car details.
    /// </summary>
    public class UpdateDriverDto
    {
        // Identification
        public int Id { get; set; }

        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? WhatsAppNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int NationalityId { get; set; }
        public string? Bio { get; set; }

        // Images
        public string? LicenseImageUrl { get; set; } = string.Empty;
        public string? PassportImageUrl { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = string.Empty;

        // Professional Information
        public byte YearsOfExperience { get; set; }

        // Pricing
        public decimal? PricePerHour { get; set; } // Driver's hourly rate

        // Availability (Storing Only Date)
        public DateTime? AvailableFrom { get; set; } // When the driver starts being available
        public DateTime? AvailableTo { get; set; }   // When the driver's availability ends
        public bool IsAvailableAllTime { get; set; } // If true, driver ignores date restrictions

        // Car Details
        public bool HasCar { get; set; } = false;
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? Year { get; set; }
        public byte? NumberOfSeats { get; set; }
        public int? ColorId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public string? MainCarImageUrl { get; set; } // Main car image URL
        public List<string>? CarImageUrl { get; set; } // Additional car image URLs

        // Cities and Countries worked on and covered
        public List<int> CitiesWorkedOn { get; set; } = new List<int>();
        public List<int> CitiesCoverNow { get; set; } = new List<int>();
        public List<int> CountriesWorkedOn { get; set; } = new List<int>();
        public List<int> CountriesCoverNow { get; set; } = new List<int>();
        public List<int> LanguagesSpoken { get; set; } = new List<int>();
    }
}
