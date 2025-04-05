namespace Mottrist.Service.Features.Cities.Dtos
{
    public class CityWithCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
