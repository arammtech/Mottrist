using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Destinations.DTOs
{
    /// <summary>
    /// Data Transfer Object for Destination operations, encapsulating essential information.
    /// </summary>
    public class DestinationDTO
    {
        /// <summary>
        /// The unique identifier of the destination.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the destination.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The Name of the city where the destination is located.
        /// </summary>
        public string? CityName { get; set; }

        /// <summary>
        /// The type/category of the destination (e.g., Tourist Spot, Historical Site).
        /// </summary>
        public string? Type { get; set; }

        public IFormFile? Image { get; set; }

        /// <summary>
        /// URL or path of the destination's image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// A brief description of the destination.
        /// </summary>
        public string? Description { get; set; }
    }
}
