using Mottrist.Domain.Common;
using Mottrist.Domain.Entities;

namespace Mottrist.Domain.LookupEntities
{
    public class City : LookupEntity
    {
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        #region Navigation Properties
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<DriverCity> DriverCities { get; set; } = new List<DriverCity>();
        public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();
        #endregion
    }

}
