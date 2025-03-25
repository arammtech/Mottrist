using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class DriverCity
    {
        public int DriverId { get; set; }
        public int CityId { get; set; }
        public  WorkStatus WorkStatus { get; set; }



        #region Navigation Properties
        public virtual Driver Driver { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        #endregion
    }
}
