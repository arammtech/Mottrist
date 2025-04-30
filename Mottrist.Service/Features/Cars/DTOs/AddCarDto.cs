using Microsoft.AspNetCore.Http;

namespace Feature.Car.DTOs
{
    public class AddCarDto
    {
        public int BrandId { get; set; }
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public List<IFormFile>? CarImages { get; set; }
    }
}
