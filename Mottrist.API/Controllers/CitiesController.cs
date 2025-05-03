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
    [Route("api/Cities")]
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
        /// Retrieves a city by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the city to retrieve.</param>
        /// <returns>
        /// - HTTP 200 OK with the city details if found.
        /// - HTTP 404 Not Found if no city exists with the given ID.
        /// - HTTP 400 Bad Request if the provided ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
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
            catch (HttpRequestException httpEx)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", httpEx.Message);
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
        [HttpGet("All", Name = "GetAllCitiesAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _cityService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No cities found.");
                }

                return SuccessResponse(dataResult, "Cities retrieved successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all cities belonging to the specified country.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>
        /// - HTTP 200 OK with the list of cities if successful.
        /// - HTTP 204 No Content if no cities are found for the given country.
        /// - HTTP 400 Bad Request if the provided country ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("country/{id:int}", Name = "GetAllByCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityWithCountryDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
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

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No cities found for the specified country.");
                }

                return SuccessResponse(dataResult, "Cities retrieved successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all cities with country information.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of cities with country data.
        /// - HTTP 204 No Content if no cities are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("WithCountry", Name = "GetAllCitiesWithCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<CityWithCountryDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithCountryAsync()
        {
            try
            {
                var dataResult = await _cityService.GetAllWithCountryAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No cities found with country information.");
                }

                return SuccessResponse(dataResult, "Cities with country information retrieved successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
