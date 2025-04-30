using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Cars.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public byte NumberOfSeats { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasAirCondiations { get; set; }

        #region Navigations Properties
        public CarBrandDto Brand { get; set; } = null!;
        public CarModelDto Model { get; set; } = null!;
        public CarColorDto Color { get; set; } = null!;
        public CarBodyTypeDto BodyType { get; set; } = null!;
        public CarFuelTypeDto FuelType { get; set; } = null!;
        public IEnumerable<CarImageDto>? CarImageUrls { get; set; } = [];
        #endregion
    }
}
