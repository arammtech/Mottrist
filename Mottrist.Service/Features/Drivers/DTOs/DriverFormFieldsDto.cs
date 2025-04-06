using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Languages.DTOs;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    public class DriverFormFieldsDto
    {
        public CarFieldsDto CarFieldsDto { get; set; } = null!;
        public DataResult<LanguageDto> Languages { get; set; } = null!;
        public DataResult<CountryDto> Countries { get; set; } = null!;
        public DataResult<CityDto> Cities { get; set; } = null!;
    }
}
