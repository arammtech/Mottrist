using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cities.Dtos;

namespace Mottrist.Service.Features.Countries.DTOs
{
    public class GetCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CityDto> Cities { get; set; } = null!;
    }
}
