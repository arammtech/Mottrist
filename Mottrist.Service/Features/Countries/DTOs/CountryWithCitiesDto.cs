using Mottrist.Service.Features.Cities.Dtos;

namespace Mottrist.Service.Features.Countries.DTOs
{
    public class CountryWithCitiesDto : CountryDto
    {
        public List<CityDto> Cities { get; set; } = new();
    }
}
