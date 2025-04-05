using Mottrist.Domain.Common;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Domain.Entities
{
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

        // Status
        [Required]
        public DriverStatus Status { get; set; } = DriverStatus.Binding;

        #region Navigation Properties

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Car? Car { get; set; }
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<DriverCity> DriverCities { get; set; } = new List<DriverCity>();
        public virtual ICollection<DriverCountry> DriverCountrites { get; set; } = new List<DriverCountry>();
        public virtual ICollection<DriverLanguage> DriverLanguages { get; set; } = new List<DriverLanguage>();

        #endregion
    }

}
