using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.Languages.DTOs;

namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for retrieving traveler information.
    /// </summary>
    public class TravelerDto
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

        // Contact Details

        /// <summary>
        /// Gets or sets the email address of the traveler.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phone number of the traveler. Optional.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the WhatsApp number of the traveler. Optional.
        /// </summary>
        public string? WhatsAppNumber { get; set; }

        // Profile & Preferences

        /// <summary>
        /// Gets or sets the URL of the traveler's profile image. Optional.
        /// </summary>
        public string? ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the preferred language of the traveler. Optional.
        /// </summary>
        public LanguageDto? PreferredLanguage { get; set; }

        // Location Information

        /// <summary>
        /// Gets or sets the country associated with the traveler.
        /// </summary>
        public CountryDto Country { get; set; } = null!;

        /// <summary>
        /// Gets or sets the city associated with the traveler. Optional.
        /// </summary>
        public CityDto? City { get; set; }
    }
}
