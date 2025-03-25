using Mottrist.Domain.Common;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class Country : LookupEntity
    {
        public string Name { get; set; } = null!;
        public Continent Continent { get; set; }


        #region Navigation Properties
        public virtual ICollection<DriverCountry> DriverCountryCoverages { get; set; } = new List<DriverCountry>();

        public virtual ISet<City> Cities { get; set; } = new HashSet<City>();
        #endregion
    }
}
