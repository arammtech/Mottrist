using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new traveler.
    /// </summary>
    public class AddTravelerDto
    {
        // Personal Information
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;


        // Location & Nationality
        [Range(1, 5, ErrorMessage = "Nationality ID must be between 1 and 5.")]
        public int NationalityId { get; set; }

        public int? CityId { get; set; }

        // Contact Information
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format. Expected format: example@domain.com")]
        [UniqueUser]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. Expected format: +12345678901 (10-15 digits).")]
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }

        // Authentication
        [Required]
        public string Password { get; set; } = null!;

        // Preferences & Profile
        public IFormFile? ProfileImage { get; set; }

        public int? PreferredLanguageId { get; set; }
    }
}
