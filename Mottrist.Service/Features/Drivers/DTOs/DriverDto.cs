using Mottrist.Service.Features.Cars.DTOs;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.Languages.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a driver's personal information, car details, and user interactions.
    /// </summary>
    public class DriverDto
    {
        // Personal Information
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? Bio { get; set; }
        public byte YearsOfExperience { get; set; }
        public string Status { get; set; } = null!;
        public decimal PricePerHour { get; set; } // Driver's hourly rate
        
        public byte Rating { get; set; } // Driver's rating out of 5
        public bool IsAvailable
        {
            get
            {
                // If the driver is available all the time, return true immediately
                if (IsAvailableAllTime)
                    return true;

                // Ensure both availability dates exist before checking the range
                if(AvailableFrom == null || AvailableTo == null)
                    return false;   

                bool isWithinDateRange = 
                   AvailableFrom.Value.Date <= DateTime.Today &&
                   AvailableTo.Value.Date >= DateTime.Today;

                return isWithinDateRange;
            }
        }
        public bool HasCar => Car != null;
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }  
        public bool IsAvailableAllTime { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int NumberOfViews { get; set; }

        // Images
        public string? ProfileImageUrl { get; set; }
        public string LicenseImageUrl { get; set; } = null!;
        public string PassportImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        #region Navigation Properties
        public CarDto? Car { get; set; }
        public CountryDto Natioanlity { get; set; } = null!;
        public IEnumerable<CityDto>? CitiesWorkedOn { get; set; } = [];
        public IEnumerable<CityDto>? CitiesCoverNow { get; set; } = [];
        public IEnumerable<CountryDto>? CountriesWorkedOn { get; set; } = [];
        public IEnumerable<CountryDto>? CountriesCoverNow { get; set; } = [];
        public IEnumerable<LanguageDto>? SpokenLanguages { get; set; } = [];
        #endregion
    }
}
