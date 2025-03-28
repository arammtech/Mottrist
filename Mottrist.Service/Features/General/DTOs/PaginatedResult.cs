
namespace Mottrist.Service.Features.General.DTOs
{
    public class PaginatedResult<T>
    {
        /// <summary>
        /// The data for the current page.
        /// </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// The number of records in the current page.
        /// </summary>
        public int DataRecordsCount => Data.Count();

        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of records per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of records across all pages.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// The total number of pages calculated from TotalRecordsCount and PageSize.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalRecordsCount / PageSize);
    }

}
