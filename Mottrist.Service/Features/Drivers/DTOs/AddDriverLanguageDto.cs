namespace Mottrist.Service.Features.Drivers.Dtos
{
    /// <summary>
    /// DTO for adding a language to a driver.
    /// </summary>
    public class AddDriverLanguageDto
    {
        /// <summary>
        /// Gets or sets the driver's ID.
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Gets or sets the language ID.
        /// </summary>
        public List<int> LanguageId { get; set; } = new List<int>();
    }
}
