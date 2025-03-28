using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;

namespace Mottrist.API.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Traveler Id.");

            try
            {
                GetTravelerDto? travelerDto = await _travelerService.GetByIdAsync(id);

                return travelerDto != null ?
                       Ok(travelerDto)
                     : NotFound(new { Error = $"Traveler with Id {id} was not found." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Retrieves all travelers.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a list of all travelers
        /// or an error message if no data is found or in case of an exception.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           try {
                var travelersDto = await _travelerService.GetAllAsync();

                return  travelersDto != null ?
                        Ok(travelersDto)
                       : StatusCode(500, new { Error = "No data found." } );
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
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
        [HttpGet("TravelersPerPage")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than zero.");

            try
            {
                var travelersDto = await _travelerService.GetAllWithPaginationAsync(page, pageSize);

                return travelersDto != null ?
                        Ok(travelersDto)
                       : StatusCode(500, new { Error = "No data found." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
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
        [HttpPost]
        public async Task<IActionResult> Create(AddUpdateTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }

            try
            {
                if (travelerDto == null)
                    return BadRequest("Traveler data is null.");

                var result = await _travelerService.AddAsync(travelerDto);

                return result.IsSuccess ?
                    CreatedAtAction(nameof(GetById), new { id = travelerDto.Id }, travelerDto) :
                    StatusCode(500, new { Error = "Error creating traveler." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, AddUpdateTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }

            try
            {
                if (travelerDto == null || travelerDto.Id != id)
                    return BadRequest("Traveler data is invalid.");

                var result = await _travelerService.UpdateAsync(travelerDto);
                return result.IsSuccess ?
                    NoContent() :
                    StatusCode(500, new { Error = "Error updating traveler." });

            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" }); // Handle general exceptions
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

           try
            {
                var result = await _travelerService.DeleteAsync(id);
               
                return result.IsSuccess ?
                    NoContent() : 
                    StatusCode(500, new { Error = "Error deleting traveler." });

            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" }); // Handle general exceptions
            }
        }
    }
}
