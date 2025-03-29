
namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new driver, including personal information and optional car details.
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

        // Images
        public string LicenseImageUrl { get; set; } = null!;
        public string PassportImageUrl { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }

        // Professional Information
        public byte YearsOfExperience { get; set; }

        // Car Details
        public bool HasCar { get; set; } = false;
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? Year { get; set; }
        public byte? NumberOfSeats { get; set; }
        public int? ColorId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public string? CarImageUrl { get; set; }
    }
}
