using Mottrist.Domain.Common;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class DriverCountry : BaseEntity
    {
        public int DriverId { get; set; }
        public int CountryId { get; set; }
        public  WorkStatus WorkStatus { get; set; }

        #region Navigation Properties
        public virtual Driver Driver { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        #endregion
    }

}
