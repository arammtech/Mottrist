using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class CarImage : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public int CarId { get; set; }
        public bool IsMain { get; set; }

        #region Navigations
        public virtual Car Car { get; set; } = null!;
        #endregion
    }
}
