namespace Feature.Car.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }
    }
}
