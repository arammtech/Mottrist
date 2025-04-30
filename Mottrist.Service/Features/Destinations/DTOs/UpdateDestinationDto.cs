using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Destinations.DTOs
{
    /// <summary>
    /// Data Transfer Object for Destination operations, encapsulating essential information.
    /// </summary>
    public class UpdateDestinationDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CityId { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        public IFormFile? Image { get; set; } 
        public string? Description { get; set; }
    }
}
