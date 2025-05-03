using Mottrist.Service.Features.General.DTOs;

namespace Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs
{
    public class CarFieldsDto
    {
        public DataResult<CarBrandDto> CarBrands { get; set; } = null!;
        public DataResult<CarColorDto> CarColors { get; set; } = null!;
        public DataResult<CarFuelTypeDto> CarFuelTypes { get; set; } = null!;
        public DataResult<CarBodyTypeDto> CarBodyTypes { get; set; } = null!;
    }
}
