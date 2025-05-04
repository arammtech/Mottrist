using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Languages.Interfaces;
using Mottrist.Service.Features.Languages.DTOs;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;
using Microsoft.AspNetCore.Authorization;

namespace Mottrist.WebAPI.Controllers
{
    /// <summary>
    /// API Controller for managing language-related operations.
    /// </summary>
    [Route("api/languages")]
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
        /// Retrieves details of a language based on the specified ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the language to retrieve.
        /// </param>
        /// <returns>
        /// An API response containing language details.
        /// </returns>
        /// <response code="200">Successfully retrieved language details.</response>
        /// <response code="400">Bad request due to invalid language ID.</response>
        /// <response code="404">Language not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
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
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of all available languages.
        /// </summary>
        /// <returns>
        /// An API response containing language details.
        /// </returns>
        /// <response code="200">Successfully retrieved language data.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all", Name = "GetAllLanguagesAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<LanguageDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _languageService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Languages retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
