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
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        // Location & Nationality
        public int NationalityId { get; set; }
        public int? CityId { get; set; }

        // Contact Information
        [UniqueUser]
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }

        // Authentication
        public string Password { get; set; } = null!;

        // Preferences & Profile
        public IFormFile? ProfileImage { get; set; }
        public int? PreferredLanguageId { get; set; }
    }
}
