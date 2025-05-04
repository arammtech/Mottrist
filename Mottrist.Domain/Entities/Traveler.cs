using Mottrist.Domain.Common;
using Mottrist.Domain.Common.Interfaces;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Domain.Entities
{
    public class Traveler : BaseEntity, ISoftDeleteable
    {
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public int? CityId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int? PreferredLanguageId { get; set; }
        public int UserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Language PreferredLanguage { get; set; } = null!;
        public virtual City City { get; set; } = null!;

        #endregion
    }
}
