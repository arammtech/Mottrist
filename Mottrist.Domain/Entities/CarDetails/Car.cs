using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class Car : BaseEntity
    {
        public int BrandId { get; set; }
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }

        #region Navigation Properties
        public Model Model { get; set; } = null!;
        public Color Color { get; set; } = null!;
        public Brand Brand { get; set; } = null!;
        public BodyType BodyType { get; set; } = null!;
        public FuelType FuelType { get; set; } = null!;
        public ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();
        #endregion
    }
}
