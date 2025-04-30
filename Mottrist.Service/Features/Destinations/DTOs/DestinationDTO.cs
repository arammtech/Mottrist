using Microsoft.AspNetCore.Http;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Countries.DTOs;

namespace Mottrist.Service.Features.Destinations.DTOs
{
    /// <summary>
    /// Data Transfer Object for Destination operations, encapsulating essential information.
    /// </summary>
    public class DestinationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Description { get; set; }
        public CountryDto Country { get; set; } = null!;
        public CityDto City { get; set; } = null!;
    }
}
