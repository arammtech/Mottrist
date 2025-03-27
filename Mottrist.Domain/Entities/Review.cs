
using Mottrist.Domain.Common;
using Mottrist.Domain.Identity;

namespace Mottrist.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
