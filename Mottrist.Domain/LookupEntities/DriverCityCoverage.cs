using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class DriverCityCoverage
    {
        public int DriverId { get; set; }
        public int CityId { get; set; }
        public int CoverageTypeId { get; set; }



        #region Navigation Properties
        public virtual Driver Driver { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        public virtual CoverageType CoverageType { get; set; } = null!;
        #endregion
    }
}
