using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Destinations.DTOs
{
    /// <summary>
    /// Data Transfer Object for Destination operations, encapsulating essential information.
    /// </summary>
    public class AddDestinationDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CityId { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public IFormFile Image { get; set; } = null!;

        public string? Description { get; set; }
    }
}
