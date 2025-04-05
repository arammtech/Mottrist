namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a driver's personal information and optional car details.
    /// </summary>
    public class DriverDto
    {
        // Identification
        public int Id { get; set; }

        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string Nationality { get; set; } = null!;
        public string? Bio { get; set; }

        // Images
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string LicenseImageUrl { get; set; } = null!;
        public string PassportImageUrl { get; set; } = null!;

        // Professional Information
        public byte YearsOfExperience { get; set; }
        public string Status { get; set; } = null!;

        // Car Details
        public bool HasCar { get; set; } = false;
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public int? CarYear { get; set; }
        public byte? CarNumberOfSeats { get; set; }
        public string? CarColor { get; set; }
        public string? CarBodyType { get; set; }
        public string? CarFuelType { get; set; }
        public string? MainCarImageUrl { get; set; }
        public List<string>? AddtionalCarImageUrls { get; set; }



        // Cities and Countries worked on and covered
        public string? CitiesWorkedOn { get; set; } = string.Empty;
        public string? CitiesCoverNow { get; set; } = string.Empty;
        public string? CountriesWorkedOn { get; set; } = string.Empty;
        public string? CountriesCoverNow { get; set; } = string.Empty;
        public string? LanguagesSpoken { get; set; } = string.Empty;
    }
}
