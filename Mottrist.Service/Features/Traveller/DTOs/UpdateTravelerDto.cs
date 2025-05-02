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
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        // Location Information
        public int NationalityId { get; set; }
        public int? CityId { get; set; }

        // Contact Details
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }

        // Preferences & Profile
        public IFormFile? ProfileImage { get; set; }
        public int? PreferredLanguageId { get; set; }

    }
}
