using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// DTO for updating a traveler's information.
    /// </summary>
    public class UpdateTravelerDto
    {
        // Identity Information
        [Required(ErrorMessage = "Traveler ID is required.")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        // Location Information
        [Range(1, 5, ErrorMessage = "Nationality ID must be between 1 and 5.")]
        [Required]
        public int NationalityId { get; set; }

        public int? CityId { get; set; }

        // Contact Details

        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. Expected format: +12345678901 (10-15 digits).")]
        public string? PhoneNumber { get; set; }

        public string? WhatsAppNumber { get; set; }

        // Preferences & Profile
        public IFormFile? ProfileImage { get; set; }

        public int? PreferredLanguageId { get; set; }

    }
}
