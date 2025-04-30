using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class Car : BaseEntity
    {
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasAirCondiations { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }

        #region Navigation Properties
        public virtual Model Model { get; set; } = null!;
        public virtual Color Color { get; set; } = null!;
        public virtual Brand Brand { get; set; } = null!;
        public virtual BodyType BodyType { get; set; } = null!;
        public virtual FuelType FuelType { get; set; } = null!;
       public virtual ICollection<CarImage> CarImages { get; set; } = [];
        #endregion
    }
}
