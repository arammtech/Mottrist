using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a driver's personal information, car details, and user interactions.
    /// </summary>
    public class DriverDto
    {
        // Identification
        public int Id { get; set; }

        // Personal Information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string Nationality { get; set; } = null!;
        public string? Bio { get; set; }

        // Images
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string LicenseImageUrl { get; set; } = null!;
        public string PassportImageUrl { get; set; } = null!;

        // Professional Information
        public byte YearsOfExperience { get; set; }
        public string Status { get; set; } = null!;

        // Car Details
        public bool HasCar { get; set; } = false;
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public int? CarYear { get; set; }
        public byte? CarNumberOfSeats { get; set; }
        public string? CarColor { get; set; }
        public string? CarBodyType { get; set; }
        public string? CarFuelType { get; set; }
        public string? MainCarImageUrl { get; set; }
        public List<string>? AddtionalCarImageUrls { get; set; }

        // Pricing
        public decimal? PricePerHour { get; set; } // Driver's hourly rate

        // Availability (Storing Only Date)
        public DateTime? AvailableFrom { get; set; } // When the driver starts being available
        public DateTime? AvailableTo { get; set; }   // When the driver's availability ends
        public bool IsAvailableAllTime { get; set; } // If true, driver ignores date restrictions

        public bool IsAvailable
        {
            get
            {
                // If the driver is available all the time, return true immediately
                if (IsAvailableAllTime)
                    return true;

                // Ensure both availability dates exist before checking the range
                bool hasValidDates = AvailableFrom.HasValue && AvailableTo.HasValue;

                if (!hasValidDates)
                {
                    return false;
                }

                bool isWithinDateRange = hasValidDates && 
                                         DateTime.Now >= AvailableFrom.Value && 
                                         DateTime.Now <= AvailableTo.Value;

                return isWithinDateRange;
            }
        }

        // Cities and Countries worked on and covered
        public List<string>? CitiesWorkedOn { get; set; } = new List<string>();
        public List<string>? CitiesCoverNow { get; set; } = new List<string>();
        public List<string>? CountriesWorkedOn { get; set; } = new List<string>();
        public List<string>? CountriesCoverNow { get; set; } = new List<string>();
        public List<string>? LanguagesSpoken { get; set; } = new List<string>();

        // User Interaction
        /// <summary>
        /// The total count of likes received by the driver.
        /// </summary>
        public int LikesCount { get; set; } = 0;

        /// <summary>
        /// The total count of dislikes received by the driver.
        /// </summary>
        public int DislikesCount { get; set; } = 0;
    }
}
