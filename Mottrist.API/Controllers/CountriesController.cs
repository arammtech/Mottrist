using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mottrist.API.Response;
using Mottrist.Service.Features.Countries.Interfaces;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// The <see cref="CountriesController"/> class handles HTTP requests related to countries.
    /// It provides endpoints for retrieving country details by ID and fetching all countries.
    /// </summary>
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        /// <summary>
        /// Country service instance for handling country-related operations.
        /// </summary>
        private readonly ICountryService _countryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesController"/> class.
        /// </summary>
        /// <param name="countryService">The service used to interact with country data.</param>
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Retrieves a country by its ID.
        /// </summary>
        /// <param name="id">The unique identifier for the country.</param>
        /// <returns>A <see cref="IActionResult"/> representing the HTTP response, which may include the country or an error message.</returns>
        /// <response code="200">Returns the country data if found.</response>
        /// <response code="400">The provided country ID is invalid.</response>
        /// <response code="404">No country found with the provided ID.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetCountryByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]  // Success response
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]  // Invalid ID
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]  // Country not found
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]  // Internal server error
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidId", "The provided country ID is invalid.");
            }

            try
            {
                var country = await _countryService.GetByIdAsync(id);

                if (country == null)
                {
                    return NotFoundResponse("CountryNotFound", "No country found with the provided ID.");
                }

                return SuccessResponse(country, "Country retrieved successfully.");
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
        /// Retrieves all countries.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> representing the HTTP response, which may include a list of countries or an error message.</returns>
        /// <response code="200">Returns a list of countries.</response>
        /// <response code="204">No countries found in the database.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [AllowAnonymous]
        [HttpGet("All", Name = "GetAllCountriesAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]  // Success response
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]  // No countries found
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]  // Internal server error
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _countryService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No countries found.");
                }

                return SuccessResponse(dataResult, "Countries retrieved successfully.");
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
