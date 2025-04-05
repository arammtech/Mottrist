using Mottrist.Domain.Enums;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    /// <summary>
    /// DTO for adding multiple cities to a driver.
    /// </summary>
    public class AddDriverCitiesDto
    {
        /// <summary>
        /// Gets or sets the driver's ID.
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Gets or sets the list of city IDs and their associated work statuses.
        /// </summary>
        public List<(int CityId, WorkStatus WorkStatus)> Cities { get; set; } = new List<(int, WorkStatus)>();
    }
}
