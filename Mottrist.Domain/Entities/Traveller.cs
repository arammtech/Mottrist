using Mottrist.Domain.Common;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Domain.Entities
{
    public class Traveller : BaseEntity
    {
        public string? WhatsAppNumber { get; set; }
        public int NationailtyId { get; set; }
        public int UserId { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        #endregion
    }
}
