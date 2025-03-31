namespace Feature.Car.DTOs
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }

        //public string MainCarImage { get; set; } = null!;
        //public List<string>? AddtionalCarImages { get; set; } = new List<string>();
    }
}
