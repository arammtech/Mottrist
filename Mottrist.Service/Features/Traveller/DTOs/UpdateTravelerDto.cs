using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// DTO for updating a traveler's information.
    /// </summary>
    public class UpdateTravelerDto
    {
        /// <summary>
        /// Unique identifier for the traveler (required).
        /// </summary>
        [Required(ErrorMessage = "Traveler ID is required.")]
        public int Id { get; set; }

        /// <summary>
        /// WhatsApp contact number (optional).
        /// </summary>
        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. Example: +12345678901")]
        public string? WhatsAppNumber { get; set; }

        /// <summary>
        /// Nationality ID of the traveler (required).
        /// </summary>
        [Required(ErrorMessage = "Nationality is required.")]
        [Range(1, 5, ErrorMessage = "NationalityId must be between 1 and 5.")]
        public int NationalityId { get; set; }

        /// <summary>
        /// First name of the traveler (required, min 2 chars, max 50 chars).
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last name of the traveler (required, min 2 chars, max 50 chars).
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Phone number (optional, must be in international format).
        /// </summary>
        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid phone number format. Example: +12345678901")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Profile image URL (optional).
        /// </summary>
        public string? ProfileImageUrl { get; set; }

        /// <summary>
        /// Profile image as file
        /// </summary>
        public IFormFile? ProfileImage { get; set; }
    }
}
