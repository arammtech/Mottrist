using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object for adding a new driver, including personal information, car details, and image uploads.
    /// </summary>
    public class AddDriverDto
    {
        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string? Bio { get; set; }
        public byte YearsOfExperience { get; set; }
        public decimal? PricePerHour { get; set; } 
        public DateTime? AvailableFrom { get; set; } 
        public DateTime? AvailableTo { get; set; } 
        public bool IsAvailableAllTime { get; set; } = false; 
        public bool HasCar { get; set; } = false;

        // Image Uploads
        public IFormFile? ProfileImage { get; set; }
        public IFormFile LicenseImage { get; set; } = null!;
        public IFormFile PassportImage { get; set; } = null!;

        #region Navigation Properties
        public AddCarDto? Car { get; set; } 
        public List<int> CitiesWorkedOn { get; set; } = [];
        public List<int> CitiesCoverNow { get; set; } = [];
        public List<int> CountriesWorkedOn { get; set; } = [];
        public List<int> CountriesCoverNow { get; set; } = [];
        public List<int> LanguagesSpoken { get; set; } = [];
        #endregion
    }
}
