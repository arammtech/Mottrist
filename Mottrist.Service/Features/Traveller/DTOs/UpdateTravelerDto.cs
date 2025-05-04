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

        /// <summary>
        /// Gets or sets the unique identifier of the traveler.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the traveler.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the traveler.
        /// </summary>
        public string LastName { get; set; } = null!;

        // Location Information

        /// <summary>
        /// Gets or sets the unique identifier of the traveler's nationality.
        /// </summary>
        public int NationalityId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the city where the traveler resides. Optional.
        /// </summary>
        public int? CityId { get; set; }

        // Contact Details

        /// <summary>
        /// Gets or sets the phone number of the traveler. Optional.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the WhatsApp number of the traveler. Optional.
        /// </summary>
        public string? WhatsAppNumber { get; set; }

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
