namespace Mottrist.Service.Features.General.DTOs
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the request was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message that can be used to provide additional information.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The payload or data returned by the API.
        /// </summary>
        public T Data { get; set; } 

        /// <summary>
        /// A list of errors, if any occurred.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }
    }
}