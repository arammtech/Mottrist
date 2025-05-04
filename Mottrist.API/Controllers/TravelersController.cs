using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Utilities.Identity;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing traveler-related operations.
    /// </summary>
    [Route("api/travelers")]
    [ApiController]
    public class TravelersController : ControllerBase
    {
        private readonly ITravelerService _travelerService;

        public TravelersController(ITravelerService travelerService)
        {
            _travelerService = travelerService; 
        }

        /// <summary>
        /// Retrieves a traveler by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the traveler.</param>
        /// <returns>Traveler data if found; otherwise, an error message.</returns>
        /// <response code="200">Traveler retrieved successfully.</response>
        /// <response code="400">Invalid traveler ID.</response>
        /// <response code="404">Traveler not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleTraveler}")]
        [HttpGet("{id:int}", Name = "GetTravelerByIdAsync")]
        [ProducesResponseType(typeof(TravelerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequestResponse("INVALID_TRAVELER_ID", "Traveler id not valid.", "Traveler Id should be positive number");

            try
            {
                TravelerDto? travelerDto = await _travelerService.GetByIdAsync(id);

                return travelerDto != null ? SuccessResponse<TravelerDto>(travelerDto, "Traveler retrieved successfully.")
                       : NotFoundResponse("TRAVELER_NOT_FOUND", "Traveler not found.", $"Traveler with Id {id} was not found.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all travelers.
        /// </summary>
        /// <returns>A list of all travelers.</returns>
        /// <response code="200">Travelers retrieved successfully.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpGet("all",Name = "GetAllTravelersAsync")]
        [ProducesResponseType(typeof(DataResult<TravelerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var travelerDtos = await _travelerService.GetAllAsync();

                return travelerDtos != null ? SuccessResponse<DataResult<TravelerDto>>(travelerDtos, "Travelers retrieved successfully.")
                       : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NO_DATA_FOUND", "No data found.", "There is no data found for travelers.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a paginated list of travelers.
        /// </summary>
        /// <param name="page">The page number (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A paginated list of travelers.</returns>
        /// <response code="200">Travelers retrieved successfully.</response>
        /// <response code="400">Invalid page or page size.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpGet("all/paged", Name = "GetAllTravelersWithPaginationAsync")]
        [ProducesResponseType(typeof(PaginatedResult<TravelerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequestResponse("PAGE_OR_PAGE_SIZE_INVALID", "Invalid Page or pageSize", "Page and pageSize must be greater than zero.");

            try
            {
                var travelerDtos = await _travelerService.GetAllWithPaginationAsync(page, pageSize);

                return travelerDtos != null ? SuccessResponse<PaginatedResult<TravelerDto>>(travelerDtos, "Travelers retrieved successfully.")
                       : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NO_DATA_FOUND", "No data found.", "There is no data found for travelers.");

            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new traveler to the system.
        /// </summary>
        /// <param name="travelerDto">The data of the traveler to add.</param>
        /// <returns>The created traveler with status and route reference.</returns>
        /// <response code="201">Traveler created successfully.</response>
        /// <response code="400">Validation error occurred.</response>
        /// <response code="500">An internal server error occurred.</response>
        [AllowAnonymous]
        [HttpPost(Name = "AddNewTravelerAsync")]
        [ProducesResponseType(typeof(TravelerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm]AddTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                return BadRequestResponse("DATA_INVALID", "Validation error in AddTravelerDto.", errors);
            }

            try
            {
                var result = await _travelerService.AddAsync(travelerDto);

                return result.IsSuccess
                ? CreatedResponse("GetTravelerByIdAsync", new { id = result.Data?.Id }, result.Data, "traveler created successfully.")
                : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create traveler.");

  
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing traveler's information.
        /// </summary>
        /// <param name="id">The ID of the traveler to update.</param>
        /// <param name="travelerDto">The updated traveler data.</param>
        /// <returns>The updated traveler if successful.</returns>
        /// <response code="200">Traveler updated successfully.</response>
        /// <response code="400">Invalid request or mismatched ID.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleTraveler}")]
        [HttpPut("{id:int}", Name = "UpdateTravelerAsync")]
        [ProducesResponseType(typeof(TravelerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                return BadRequestResponse("DATA_INVALID", "Validation error in AddTravelerDto.", errors);

            }

            try
            {
                if (travelerDto.Id != id)
                    return BadRequestResponse("ACCESS_DEINED", "Invalid Id to change data.", $"Not allowed to change Traveler's Id value which is {id}");

                var result = await _travelerService.UpdateAsync(travelerDto);

                return result.IsSuccess
                ? SuccessResponse(result.Data, "Traveler details updated successfully.")
                : StatusCodeResponse(StatusCodes.Status500InternalServerError, "FAILD_UPDATE_TRAVELER", "Error updating traveler.");



            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a traveler by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the traveler.</param>
        /// <returns>Status message indicating result of deletion.</returns>
        /// <response code="200">Traveler deleted successfully.</response>
        /// <response code="400">Invalid traveler ID.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleTraveler}")]
        [HttpDelete("{id:int}", Name = "DeleteTraveler")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequestResponse("INVALID_TRAVELER_ID", "Traveler id not valid.", "Traveler Id should be positive number");

            try
            {
                var result = await _travelerService.DeleteAsync(id);

                return result.IsSuccess
                ? SuccessResponse("Traveler deleted successfully.")
                : StatusCodeResponse(StatusCodes.Status500InternalServerError, "FAILD_DELETE_TRAVELER", "Error deleting traveler.", result.Errors.ToArray());

            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
