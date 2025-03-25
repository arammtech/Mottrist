using Mottrist.Domain.Common;

namespace Mottrist.Domain.LookupEntities
{
    public class CoverageType : BaseEntity
    {
        public string Type { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<DriverCountryCoverage> DriverCountryCoverages { get; set; } = new List<DriverCountryCoverage>();

        public virtual ICollection<DriverCityCoverage> DriverCityCoverages { get; set; } = new List<DriverCityCoverage>();

        #endregion
    }
}
