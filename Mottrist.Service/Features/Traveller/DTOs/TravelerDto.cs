namespace Mottrist.Service.Features.Traveller.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for retrieving traveler information.
    /// </summary>
    public class TravelerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the traveler.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the traveler's WhatsApp number.
        /// </summary>
        public string? WhatsAppNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the traveler's country.
        /// </summary>
        public string CountryName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the traveler's city.
        /// </summary>
        public string? CityName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the traveler.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the traveler.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the traveler.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the username of the traveler (optional).
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the traveler (optional).
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL of the traveler's profile image (optional).
        /// </summary>
        public string? ProfileImageUrl { get; set; }

        /// <summary>
        /// Preferred LanguageId to speck with
        /// </summary>
        public string? PreferredLanguageName { get; set; } = null!;
    }
}