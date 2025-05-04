using Mottrist.Domain.Common;
using Mottrist.Domain.Common.Interfaces;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

public class Driver : BaseEntity, ICreateAt , ISoftDeleteable
{
    // Professional Information
    public int UserId { get; set; }
    public string? WhatsAppNumber { get; set; }
    public int NationalityId { get; set; }
    public string? Bio { get; set; }
    public byte YearsOfExperience { get; set; }
    public int? CarId { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
    public bool IsAvailableAllTime { get; set; }
    public decimal PricePerHour { get; set; }
    public DriverStatus Status { get; set; } = DriverStatus.Pending;

    // Images
    public string LicenseImageUrl { get; set; } = null!;
    public string PassportImageUrl { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DateDeleted { get; set; }

    #region Navigation Properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Car? Car { get; set; }
    public virtual Country Country { get; set; } = null!;
    public virtual ICollection<DriverCity> DriverCities { get; set; } = [];
    public virtual ICollection<DriverCountry> DriverCountries { get; set; } = [];
    public virtual ISet<DriverLanguage> DriverLanguages { get; set; } = new HashSet<DriverLanguage>();
    public virtual ICollection<DriverInteraction> DriverInteractions { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    #endregion
}
