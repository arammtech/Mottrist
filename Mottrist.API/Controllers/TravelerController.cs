using Microsoft.AspNetCore.Mvc;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using System.Linq.Expressions;

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

        // GET: api/Traveler/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            var travelerDto = await _travelerService.GetByIdAsync(id);
            if (travelerDto == null)
                return NotFound($"Traveler with Id {id} was not found.");

            return Ok(travelerDto);
        }

        // GET: api/Traveler
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var travelersDto = await _travelerService.GetAllAsync();
            if (travelersDto == null || !travelersDto.Any())
                return NotFound("No travelers found.");

            return Ok(travelersDto);
        }

        // GET: api/Traveler/TravelersChunk
        [HttpGet("TravelersChunk")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than zero.");

            try
            {
                var travelersDto = await _travelerService.GetAllWithPaginationWithDtoAsync(page, pageSize);

                if (travelersDto == null || !travelersDto.Travelers.Any())
                    return NotFound("No travelers found for the given page.");

                return Ok(travelersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST: api/Traveler
        [HttpPost]
        public async Task<IActionResult> Create(AddUpdateTravelerDto travelerDto)
        {
            if (travelerDto == null)
                return BadRequest("Traveler data is null.");

            var result = await _travelerService.AddAsync(travelerDto);
            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating traveler.");

            // Return the newly created traveler details using CreatedAtAction
            return CreatedAtAction(nameof(GetById), new { id = travelerDto.Id }, travelerDto);
        }

        // PUT: api/Traveler/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, AddUpdateTravelerDto travelerDto)
        {
            if (travelerDto == null || travelerDto.Id != id)
                return BadRequest("Traveler data is invalid.");

            var result = await _travelerService.UpdateAsync(travelerDto);
            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating traveler.");

            // 204 No Content for a successful update without returning content
            return NoContent();
        }

        // DELETE: api/Traveler/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            var deleteResult = await _travelerService.DeleteAsync(id);
            if (!deleteResult.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting traveler.");

            // 204 No Content indicates the deletion was successful
            return NoContent();
        }


    }
}
