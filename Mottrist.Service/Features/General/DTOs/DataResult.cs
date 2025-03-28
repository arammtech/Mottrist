
namespace Mottrist.Service.Features.General.DTOs
{
    public class DataResult<T>
    {
        /// <summary>
        /// All the data records.
        /// </summary>
        public IEnumerable<T>? Data { get; set; } = null;

        /// <summary>
        /// The number of data records.
        /// </summary>
        public int? DataRecordsCount => Data?.Count() ?? null;
    }
}
