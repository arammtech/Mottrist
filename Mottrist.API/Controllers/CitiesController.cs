using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Cities.Interfaces;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;
using Microsoft.AspNetCore.Authorization;

namespace Mottrist.WebAPI.Controllers
{
    /// <summary>
    /// API Controller for managing city-related operations.
    /// </summary>
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        /// <summary>
        /// City service instance for handling city-related operations.
        /// </summary>
        private readonly ICityService _cityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesController"/> class.
        /// </summary>
        /// <param name="cityService">The city service to manage city-related operations.</param>
        /// <remarks>
        /// The city service is injected using dependency injection.
        /// </remarks>
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        /// <summary>
        /// Retrieves details of a city based on the specified ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the city to retrieve.
        /// </param>
        /// <returns>
        /// An API response containing city details.
        /// </returns>
        /// <response code="200">Successfully retrieved city details.</response>
        /// <response code="400">Bad request due to invalid city ID.</response>
        /// <response code="404">City not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetCityByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidId", "The provided city ID is invalid.");
            }

            try
            {
                var city = await _cityService.GetByIdAsync(id);

                if (city == null)
                {
                    return NotFoundResponse("CityNotFound", "No city found with the provided ID.");
                }

                return SuccessResponse(city, "City retrieved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all cities.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of cities if successful.
        /// - HTTP 204 No Content if no cities are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("all", Name = "GetAllCitiesAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _cityService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Cities retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");

            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of cities associated with a specific country.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the country for which cities are requested.
        /// </param>
        /// <returns>
        /// An API response containing the list of cities within the specified country.
        /// </returns>
        /// <response code="200">Successfully retrieved cities associated with the country.</response>
        /// <response code="400">Bad request due to invalid country ID.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("country/{id:int}", Name = "GetAllByCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityWithCountryDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByCountryAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidCountryId", "The provided country ID is invalid.");
            }

            try
            {
                var dataResult = await _cityService.GetAllWithCountryAsync(x => x.Country.Id == id);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Cities retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of all cities along with their associated country details.
        /// </summary>
        /// <returns>
        /// An API response containing city details along with country information.
        /// </returns>
        /// <response code="200">Successfully retrieved cities with country details.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("with-country", Name = "GetAllCitiesWithCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityWithCountryDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithCountryAsync()
        {
            try
            {
                var dataResult = await _cityService.GetAllWithCountryAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Cities retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
