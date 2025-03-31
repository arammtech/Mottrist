using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    [Route("api/Traveler")]
    [ApiController]
    public class TravelerController : ControllerBase
    {
        private readonly ITravelerService _travelerService;

        public TravelerController(ITravelerService travelerService)
        {
            _travelerService = travelerService;
        }

        /// <summary>
        /// Retrieves a traveler by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the traveler.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the traveler data if found,
        /// or an error message if not found or in case of an exception.
        /// </returns>
        [HttpGet("{id:int}", Name = "GetByIdAsync")]
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
        /// An <see cref="IActionResult"/> containing a list of all travelers
        /// or an error message if no data is found or in case of an exception.
        /// </returns>
        [HttpGet(Name = "GetAll")]
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
        /// An <see cref="IActionResult"/> containing a paginated list of travelers,
        /// or an error message if the page parameters are invalid, no data is found,
        /// or in case of an exception.
        /// </returns>

        [HttpGet("TravelersPerPage", Name = "TravelersPerPage")]
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
        /// An <see cref="IActionResult"/> with the result of the creation operation,
        /// including a location header on success or an error message on failure.
        /// </returns>
        [HttpPost(Name = "Create")]
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
        /// An <see cref="IActionResult"/> indicating the result of the update operation,
        /// such as NoContent on success or an error message on failure.
        /// </returns>
        [HttpPut("{id:int}", Name = "Update")]
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
        /// An <see cref="IActionResult"/> indicating the result of the deletion,
        /// such as NoContent on success or an error message if the deletion fails.
        /// </returns>
        [HttpDelete("{id:int}", Name = "Delete")]
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
