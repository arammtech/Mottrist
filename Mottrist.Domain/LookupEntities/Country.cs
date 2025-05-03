using Mottrist.Domain.Common;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class Country : LookupEntity
    {
        public string Name { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<DriverCountry> DriverCountries { get; set; } = new List<DriverCountry>();
        public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
        public virtual ISet<City> Cities { get; set; } = new HashSet<City>();
        #endregion
    }
}
