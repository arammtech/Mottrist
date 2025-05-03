using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Feature.Car.DTOs
{
    public class UpdateCarDto
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

        public List<IFormFile>? CarImages { get; set; }
    }
}
