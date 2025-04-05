using Mottrist.Domain.Enums;

namespace Mottrist.Service.Features.Drivers.Dtos
{
    /// <summary>
    /// DTO for adding a country to a driver.
    /// </summary>
    public class AddDriverCountryDto
    {
        /// <summary>
        /// Gets or sets the driver's ID.
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        public List<int> CountryId { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the work status for the country association.
        /// </summary>
        public WorkStatus WorkStatus { get; set; }
    }
}
