using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating driver information, including optional car details.
    /// </summary>
    public class UpdateDriverDto
    {
        // Personal Information
        public int Id { get; set; }      
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string? Bio { get; set; }
        public byte YearsOfExperience { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public bool IsAvailableAllTime { get; set; }
        public IFormFile? ProfileImage { get; set; }

        #region Navigation Properties
        public UpdateCarDto? Car { get; set; }
        public HashSet<int> CitiesWorkedOn { get; set; } = [];
        public HashSet<int> CitiesCoverNow { get; set; } = [];
        public HashSet<int> CountriesWorkedOn { get; set; } = [];
        public HashSet<int> CountriesCoverNow { get; set; } = [];
        public HashSet<int> LanguagesSpoken { get; set; } = [];
        #endregion
    }
}
