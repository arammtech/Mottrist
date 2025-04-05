using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Languages.Interfaces;
using Mottrist.Service.Features.Languages.DTOs;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.WebAPI.Controllers
{
    /// <summary>
    /// API Controller for managing language-related operations.
    /// </summary>
    [Route("api/Languages")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        /// <summary>
        /// Language service instance for handling language-related operations.
        /// </summary>
        private readonly ILanguageService _languageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesController"/> class.
        /// </summary>
        /// <param name="languageService">The language service to manage language-related operations.</param>
        /// <remarks>
        /// The language service is injected using dependency injection.
        /// </remarks>
        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
        }

        /// <summary>
        /// Retrieves a language by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the language to retrieve.</param>
        /// <returns>
        /// - HTTP 200 OK with the language details if found.
        /// - HTTP 404 Not Found if no language exists with the given ID.
        /// - HTTP 400 Bad Request if the provided ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("{id:int}", Name = "GetLanguageByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<LanguageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidId", "The provided language ID is invalid.");
            }

            try
            {
                var language = await _languageService.GetByIdAsync(id);

                if (language == null)
                {
                    return NotFoundResponse("LanguageNotFound", "No language found with the provided ID.");
                }

                return SuccessResponse(language, "Language retrieved successfully.");
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
        /// Retrieves all languages.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of languages if successful.
        /// - HTTP 204 No Content if no languages are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("All", Name = "GetAllLanguagesAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<LanguageDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _languageService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No languages found.");
                }

                return SuccessResponse(dataResult, "Languages retrieved successfully.");
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
    }
}
