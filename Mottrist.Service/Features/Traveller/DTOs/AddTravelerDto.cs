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

        /// <summary>
        /// Gets or sets the first name of the traveler.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the traveler.
        /// </summary>
        public string LastName { get; set; } = null!;

        // Location & Nationality

        /// <summary>
        /// Gets or sets the unique identifier of the traveler's nationality.
        /// </summary>
        public int NationalityId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the city where the traveler resides. Optional.
        /// </summary>
        public int? CityId { get; set; }

        // Contact Information

        /// <summary>
        /// Gets or sets the email address of the traveler. 
        /// This property is validated to be unique for each traveler.
        /// </summary>
        [UniqueUser]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phone number of the traveler. Optional.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the WhatsApp number of the traveler. Optional.
        /// </summary>
        public string? WhatsAppNumber { get; set; }

        // Authentication

        /// <summary>
        /// Gets or sets the password for the traveler. This property is required for authentication.
        /// </summary>
        public string Password { get; set; } = null!;

        // Preferences & Profile

        /// <summary>
        /// Gets or sets the profile image file for the traveler. Optional.
        /// </summary>
        public IFormFile? ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the traveler's preferred language. Optional.
        /// </summary>
        public int? PreferredLanguageId { get; set; }
    }
}
