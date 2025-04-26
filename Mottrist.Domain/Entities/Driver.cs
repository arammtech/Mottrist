using Mottrist.Domain.Common;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using System.ComponentModel.DataAnnotations;

public class Driver : BaseEntity
{
    // Identification
    public int UserId { get; set; }

    // Personal Information
    public string? WhatsAppNumber { get; set; }
    public int NationalityId { get; set; }
    public string? Bio { get; set; }

    // Images
    public string LicenseImageUrl { get; set; } = null!;
    public string PassportImageUrl { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }

    // Professional Information
    public byte YearsOfExperience { get; set; }

    // Car Information
    public int? CarId { get; set; }

    // Availability
    public DateTime? AvailableFrom { get; set; } // When the driver starts being available
    public DateTime? AvailableTo { get; set; }   // When the driver's availability ends
    public bool IsAvailableAllTime { get; set; } = false; // If true, driver ignores date restrictions

    // Pricing
    public decimal? PricePerHour { get; set; } // Driver's price per hour (nullable to allow flexibility)

    // Status
    [Required]
    public DriverStatus Status { get; set; } = DriverStatus.Pending;

    #region Navigation Properties

    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Car? Car { get; set; }
    public virtual Country Country { get; set; } = null!;
    public virtual ICollection<DriverCity> DriverCities { get; set; } = new List<DriverCity>();
    public virtual ICollection<DriverCountry> DriverCountries { get; set; } = new List<DriverCountry>();
    public virtual ICollection<DriverLanguage> DriverLanguages { get; set; } = new List<DriverLanguage>();

    #endregion
}
