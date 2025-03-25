﻿using Mottrist.Domain.Common;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string? WhatsAppNumber { get; set; }
        public int NationailtyId { get; set; }
        public string LicenseImageUrl { get; set; } = null!;
        public byte YearsOfExperience { get; set; }
        public string? Bio { get; set; }
        public string PassportImageUrl { get; set; } = null!;
        public int? CarId { get; set; }
        public int UserId { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Car? Car { get; set; }
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<DriverCityCoverage> DriverCityCoverages { get; set; } = new List<DriverCityCoverage>();
        public virtual ICollection<DriverCountryCoverage> DriverCountryCoverages { get; set; } = new List<DriverCountryCoverage>();
        public virtual ICollection<DriverLanguage> DriverLanguages { get; set; } = new List<DriverLanguage>();

        #endregion
    }
}
