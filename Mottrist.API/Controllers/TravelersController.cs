using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing traveler-related operations.
    /// </summary>
    [Route("api/Travelers")]
    [ApiController]
    public class TravelersController : ControllerBase
    {
        private readonly ITravelerService _travelerService;

        public TravelersController(ITravelerService travelerService)
        {
            _travelerService = travelerService ?? throw new ArgumentNullException(nameof(travelerService)); 
        }

        /// <summary>
        /// Retrieves a traveler by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the traveler.</param>
        /// <returns>
        /// Traveler data if found; otherwise, an error message.
        /// </returns>
        /// <response code="200">Traveler retrieved successfully.</response>
        /// <response code="400">Invalid traveler id.</response>
        /// <response code="404">Traveler not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("{id:int}", Name = "GetTravelerByIdAsync")]
        [ProducesResponseType(typeof(GetTravelerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequestResponse("INVALID_TRAVELER_ID", "Traveler id not valid.", "Traveler Id should be positive number");

            try
            {
                GetTravelerDto? travelerDto = await _travelerService.GetByIdAsync(id);

                return travelerDto != null ? SuccessResponse<GetTravelerDto>(travelerDto, "Traveler retrieved successfully.")
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
        /// <returns>
        /// A list of all travelers, or an appropriate error message.
        /// </returns>
        /// <response code="200">Travelers retrieved successfully.</response>
        /// <response code="204">No travelers found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet(Name = "GetAllTravelers")]
        [ProducesResponseType(typeof(DataResult<GetTravelerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var travelerDtos = await _travelerService.GetAllAsync();

                if (travelerDtos?.DataRecordsCount == 0 && travelerDtos?.Data != null)
                    return NoContentResponse("There is no travelers");


                return travelerDtos != null ? SuccessResponse<DataResult<GetTravelerDto>>(travelerDtos, "Travelers retrieved successfully.")
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
        /// Retrieves travelers with pagination.
        /// </summary>
        /// <param name="page">The page number (must be greater than 0).</param>
        /// <param name="pageSize">The number of travelers per page (must be greater than 0).</param>
        /// <returns>
        /// A paginated list of travelers, or an appropriate error message.
        /// </returns>
        /// <response code="200">Travelers retrieved successfully.</response>
        /// <response code="400">Invalid page or pageSize parameter.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("TravelersPerPage", Name = "TravelersPerPage")]
        [ProducesResponseType(typeof(PaginatedResult<GetTravelerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequestResponse("PAGE_OR_PAGE_SIZE_INVALID", "Invalid Page or pageSize", "Page and pageSize must be greater than zero.");

            try
            {
                var travelerDtos = await _travelerService.GetAllWithPaginationAsync(page, pageSize);

                return travelerDtos != null ? SuccessResponse<PaginatedResult<GetTravelerDto>>(travelerDtos, "Travelers retrieved successfully.")
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
        /// Creates a new traveler.
        /// </summary>
        /// <param name="travelerDto">The traveler data transfer object.</param>
        /// <returns>
        /// The created traveler data, along with a location header on success.
        /// </returns>
        /// <response code="201">Traveler created successfully.</response>
        /// <response code="400">Validation error or invalid traveler data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost(Name = "CreateTraveler")]
        [ProducesResponseType(typeof(AddTravelerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(AddTravelerDto travelerDto)
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
                if (travelerDto == null)
                    return BadRequestResponse("DATA_INVALID", "Traveler data is invalid.", "AddTravelerDto is null.");

                var result = await _travelerService.AddAsync(travelerDto);

                if (result.IsSuccess)
                    return CreatedResponse<AddTravelerDto>("GetByIdAsync", new { id = travelerDto.Id }, travelerDto);
                else
                {
                    return StatusCodeResponse(StatusCodes.Status500InternalServerError, "FAILD_CREATED_TRAVELER", "Error creating traveler.", result.Errors.ToArray());
                }
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
        /// Updates an existing traveler.
        /// </summary>
        /// <param name="id">The unique identifier of the traveler to update.</param>
        /// <param name="travelerDto">The traveler data transfer object with updated information.</param>
        /// <returns>
        /// The updated traveler data on success; otherwise, an error message.
        /// </returns>
        /// <response code="200">Traveler updated successfully.</response>
        /// <response code="400">Validation error or mismatched traveler id.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("{id:int}", Name = "UpdateTraveler")]
        [ProducesResponseType(typeof(UpdateTravelerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateTravelerDto travelerDto)
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

                if (travelerDto == null)
                    return BadRequestResponse("DATA_INVALID", "Traveler data is invalid.", "UpdateTravelerDto is null.");

                var result = await _travelerService.UpdateAsync(travelerDto);

                if (result.IsSuccess)
                    return SuccessResponse<UpdateTravelerDto>(travelerDto, "Traveler updated successfully.");
                else
                {
                    return StatusCodeResponse(StatusCodes.Status500InternalServerError, "FAILD_UPDATE_TRAVELER", "Error updating traveler.", result.Errors.ToArray());
                }

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
        /// <param name="id">The unique identifier of the traveler to delete.</param>
        /// <returns>
        /// No content on successful deletion; otherwise, an error message.
        /// </returns>
        /// <response code="204">Traveler record deleted successfully.</response>
        /// <response code="400">Invalid traveler id.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpDelete("{id:int}", Name = "DeleteTraveler")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequestResponse("INVALID_TRAVELER_ID", "Traveler id not valid.", "Traveler Id should be positive number");


            try
            {
                var result = await _travelerService.DeleteAsync(id);

                if (result.IsSuccess)
                    return NoContentResponse("Traveler record deleted successfully.");
                else
                {
                    return StatusCodeResponse(StatusCodes.Status500InternalServerError, "FAILD_DELETE_TRAVELER", "Error deleting traveler.", result.Errors.ToArray());
                }


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
