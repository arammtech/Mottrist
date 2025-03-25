using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class DriverCountryCoverage
    {
        public int DriverId { get; set; }
        public int CountryId { get; set; }
        public int CoverageTypeId { get; set; }


        #region Navigation Properties
        public virtual CoverageType CoverageType { get; set; } = null!;

        public virtual Driver Driver { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        #endregion
    }

}
