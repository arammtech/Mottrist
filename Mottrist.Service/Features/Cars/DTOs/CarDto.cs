namespace Mottrist.Service.Features.Cars.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string BodyType { get; set; } = null!;
        public string FuelType { get; set; } = null!;
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public string? MainCarImageUrl { get; set; } 
        public List<string>? AdditionalCarImageUrls { get; set; } = new List<string>();
    }
}
