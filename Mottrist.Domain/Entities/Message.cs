using Mottrist.Domain.Common;
using Mottrist.Domain.Common.Interfaces;
using Mottrist.Domain.Identity;

namespace Mottrist.Domain.Entities
{
    public class Message : BaseEntity, ICreateAt
    {
        public int UserId { get; set; }
        public string MessageBody { get; set; } = null!;
        public DateTime CreatedAt { get ; set ; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        #endregion
    }
}
