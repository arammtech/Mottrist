using Mottrist.Service.Features.Cities.Dtos;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// DTO for retrieving cities associated with a driver.
    /// </summary>
    public class GetDriverCitiesDto
    {
        /// <summary>
        /// Gets or sets a dictionary containing city IDs and their names.
        /// </summary>
        public List<CityDto> Cities { get; set; } = new List<CityDto>();

        /// <summary>
        /// Gets or sets the driver's ID to whom these cities belong.
        /// </summary>
        public int DriverId { get; set; }
    }
}
