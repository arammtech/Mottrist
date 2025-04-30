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
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        // Contact Details
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }

        // Profile & Preferences
        public string? ProfileImageUrl { get; set; }
        public LanguageDto? PreferredLanguage { get; set; }

        // Location Information
        public CountryDto Country { get; set; } = null!;
        public CityDto? City { get; set; }
    }
}
