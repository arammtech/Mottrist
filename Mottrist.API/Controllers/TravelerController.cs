using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mottrist.Domain.Entities;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("{id:int}",Name = "GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new ApiResponse<GetTravelerDto>
                {
                    Success = false,
                    Message = "Invalid Id",
                    Errors = new List<string> { $"Traveler Id should be positive number" }
                });

            try
            {
                GetTravelerDto? travelerDto = await _travelerService.GetByIdAsync(id);

                return travelerDto != null ?
                       Ok(new {
                            Success = true,
                            Message = "Traveler retrieved successfully.",
                            Data = travelerDto
                           }) :
                       NotFound(new ApiResponse<GetTravelerDto>
                       {
                             Success = false,
                             Message = "Traveler not found.",
                             Errors = new List<string> { $"Traveler with Id {id} was not found." }
                         });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GetTravelerDto>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Service error: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GetTravelerDto>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
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
           try {
                var travelerDtos = await _travelerService.GetAllAsync();

                if(travelerDtos?.DataRecordsCount == 0 && travelerDtos?.Data != null)
                       return StatusCode(StatusCodes.Status204NoContent, new ApiResponse<DataResult<GetTravelerDto>>
                       {
                           Success = true,
                           Message = "There is no travelers",
                           Data = travelerDtos
                       });

                return travelerDtos != null ?
                        Ok(new ApiResponse<DataResult<GetTravelerDto>>
                        {
                            Success = true,
                            Message = "Travelers retrieved successfully.",
                            Data = travelerDtos
                        })
                       : StatusCode(StatusCodes.Status500InternalServerError,
                       new ApiResponse<DataResult<GetTravelerDto>>
                       {
                           Success = false,
                           Message = "No data found.",
                           Errors = new List<string> { "There is no data found for travelers." }
                       });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<DataResult<GetTravelerDto>>
                    {
                        Success = false,
                        Message = "An error accrued",
                        Errors = new List<string> { $"Service error: {ex.Message}" 
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<DataResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
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
                return BadRequest(
                    new ApiResponse<PaginatedResult<GetTravelerDto>>
                    {
                        Success = false,
                        Message = "Invalid Page or pageSize",
                        Errors = new List<string> { "Page and pageSize must be greater than zero." }
                    });

            try
            {
                var travelerDtos = await _travelerService.GetAllWithPaginationAsync(page, pageSize);

                return travelerDtos != null ?
                        Ok(new ApiResponse<PaginatedResult<GetTravelerDto>>
                        {
                            Success = true,
                            Message = "Travelers retrieved successfully.",
                            Data = travelerDtos
                        })
                       : StatusCode(StatusCodes.Status500InternalServerError,
                       new ApiResponse<PaginatedResult<GetTravelerDto>>
                        {
                            Success = false,
                            Message = "No data found.",
                            Errors = new List<string> { "There is no data found for travelers." }
                       });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Service error: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
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
                    .ToList();

                return BadRequest(
                    new ApiResponse<AddTravelerDto>
                    {
                        Success = false,
                        Message = "Validation error in AddTravelerDto.",
                        Errors = errors
                    });
            }

            try
            {
                if (travelerDto == null)
                    return BadRequest(new ApiResponse<AddTravelerDto>
                    {
                        Success = false,
                        Message = "Traveler data is invalid.",
                        Errors = new List<string> { $"AddTravelerDto is null." }
                    });


                var result = await _travelerService.AddAsync(travelerDto);

                if (result.IsSuccess)
                    return CreatedAtRoute("GetByIdAsync", new { id = travelerDto.Id }, new ApiResponse<AddTravelerDto>
                        {
                            Success = true,
                            Message = "Traveler added successfully",
                            Data = travelerDto
                        }
                    );
                else
                {
                    var errors = result.Errors?.ToList() ?? new List<string>();

                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Message = "Error creating traveler.",
                        Errors = errors
                    });
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<AddTravelerDto>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Service error: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<AddTravelerDto>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
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
                    .ToList();

                return BadRequest(
                   new ApiResponse<UpdateTravelerDto>
                   {
                       Success = false,
                       Message = "Validation error in UpdateTravelerDto.",
                       Errors = errors
                   });
            }

            try
            {
                if (travelerDto.Id != id)
                    return BadRequest(new ApiResponse<UpdateTravelerDto>
                    {
                        Success = false,
                        Message = "Invalid Id",
                        Errors = new List<string> { $"Not allowed to change Traveler's Id value which is {id}" }
                    });

                if (travelerDto == null)
                    return BadRequest(new ApiResponse<UpdateTravelerDto>
                    {
                        Success = false,
                        Message = "UpdateTravelerDto data is invalid.",
                        Errors = new List<string> { $"Traveler is null." }
                    });

                var result = await _travelerService.UpdateAsync(travelerDto);

                if (result.IsSuccess)
                    return Ok(new ApiResponse<UpdateTravelerDto>
                         { 
                            Success = true,
                            Message = "Traveler updated successfully",
                            Data = travelerDto
                        });
                else
                {
                    var errors = result.Errors?.ToList() ?? new List<string>();

                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Message = "Error updating traveler.",
                        Errors = errors
                    });
                }

            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Service error: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
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
                return BadRequest(new ApiResponse<UpdateTravelerDto>
                {
                    Success = false,
                    Message = "Invalid Id",
                    Errors = new List<string> { $"Traveler Id should be positive number" }
                });

            try
            {
                var result = await _travelerService.DeleteAsync(id);

                if (result.IsSuccess)
                    return StatusCode(StatusCodes.Status204NoContent, new ApiResponse<Traveler>
                    {
                        Success = true,
                        Message = "Traveler record deleted successfully",
                    });
                else
                {
                    var errors = result.Errors?.ToList() ?? new List<string>();

                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Traveler>
                    {
                        Success = false,
                        Message = "Error deleting traveler.",
                        Errors = errors
                    });
                }
             

            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Service error: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PaginatedResult<GetTravelerDto>>
                {
                    Success = false,
                    Message = "An error accrued",
                    Errors = new List<string> { $"Unexpected error: {ex.Message}" }
                });
            }
        }
    }
}
