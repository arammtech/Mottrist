
namespace Mottrist.Service.Features.General.DTOs
{
    public class PaginatedResult<T> (IEnumerable<T>? data,
                                     int pageNumber,
                                     int pageSize,
                                     int? dataRecordsCount,
                                     int? totalRecordsCount)
    {

        public PaginatedResult() : this(null,0,0,null,null)
        {
            
        }

        /// <summary>
        /// The data for the current page.
        /// </summary>
        public IEnumerable<T>? Data { get; set; } = data;

        /// <summary>
        /// The number of records across all pages.
        /// </summary>
        public int? DataRecordsCount { get; set; } = dataRecordsCount;

        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber { get; set; } = pageNumber;

        /// <summary>
        /// The number of records per page.
        /// </summary>
        public int PageSize { get; set; } = pageSize;

        public bool HasPerviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
        /// <summary>
        /// The total number of pages calculated from TotalRecordsCount and PageSize.
        /// </summary>
        public int? TotalPages => DataRecordsCount != null ? (int)Math.Ceiling((double)DataRecordsCount / PageSize) : null;

        /// <summary>
        /// The total number of records at Database all without filter
        /// </summary>
        public int? TotalRecordsCount { get; set; } = totalRecordsCount;
    }

}
