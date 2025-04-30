using Mottrist.Domain.Common;
using Mottrist.Domain.Identity;

namespace Mottrist.Domain.Entities
{
    public class DriverInteraction : BaseEntity
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public bool? IsLiked { get; set; } // NULL = no reaction, true = liked, false = disliked
        public int ViewsCount { get; set; } = 0;
       // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Driver Driver { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
