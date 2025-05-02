using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Feature.Car.DTOs
{
    public class UpdateCarDto
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public byte NumberOfSeats { get; set; }

        [Required]
        public bool HasWiFi { get; set; }

        [Required]
        public bool HasAirCondiations { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int BodyTypeId { get; set; }

        [Required]
        public int FuelTypeId { get; set; }

        public List<IFormFile>? CarImages { get; set; }
    }
}
