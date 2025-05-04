using Mottrist.Domain.LookupEntities;

namespace Mottrist.Service.Features.Countries.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
