using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new traveler.
    /// </summary>
    public class AddTravelerDto
    {
        /// <summary>
        /// Auto-generated traveler ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// WhatsApp number of the traveler. Optional.
        /// </summary>
        public string? WhatsAppNumber { get; set; }

        /// <summary>
        /// Nationality ID.
        /// </summary>
        [Range(1, 5, ErrorMessage = "Nationality ID must be between 1 and 5.")]
        public int NationalityId { get; set; }

        /// <summary>
        /// City ID of the traveler.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// First name of the traveler. Required. (2-50 characters)
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last name of the traveler. Required. (2-50 characters)
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Email of the traveler. Required. Example: example@domain.com
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format. Expected format: example@domain.com")]
        [UniqueUser]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Phone number in international format. Example: +12345678901
        /// </summary>
        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. Expected format: +12345678901 (10-15 digits).")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Hashed Password of the traveler. Optional but should follow security rules.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Profile image URL
        /// </summary>
        public string? ProfileImageUrl { get; set; }

        /// <summary>
        /// Profile image as file
        /// </summary>
        public IFormFile? ProfileImage { get; set; }

        /// <summary>
        /// Preferred LanguageId to speck with
        /// </summary>
        public int? PreferredLanguageId { get; set; }
    }
}
