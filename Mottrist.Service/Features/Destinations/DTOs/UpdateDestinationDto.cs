using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Destinations.DTOs
{
    /// <summary>
    /// Data Transfer Object for Destination operations, encapsulating essential information.
    /// </summary>
    public class UpdateDestinationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
        public string Type { get; set; } = null!;
        public IFormFile? Image { get; set; } 
        public string? Description { get; set; }
    }
}
