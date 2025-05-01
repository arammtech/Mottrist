using Mottrist.Domain.Common;
using Mottrist.Domain.Entities;

namespace Mottrist.Domain.LookupEntities
{
    public class DriverLanguage : BaseEntity
    {
        public int DriverId { get; set; }
        public int LanguageId { get; set; }

        #region Navigations
        public virtual Driver Driver { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
        #endregion
    }
}
