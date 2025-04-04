﻿using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(new { Error = "Invalid Traveler Id." });

            try
            {
                GetTravelerDto? travelerDto = await _travelerService.GetByIdAsync(id);

                return travelerDto != null ?
                       Ok(travelerDto)
                     : NotFound(new { Error = $"Traveler with Id {id} was not found." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" });
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
                var travelerDtos = await _travelerService.GetAllAsync();

                return  travelerDtos != null ?
                        Ok(travelerDtos)
                       : StatusCode(StatusCodes.Status500InternalServerError, new { Error = "No data found." } );
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" });
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
                return BadRequest(new { Error = "Page and pageSize must be greater than zero." });

            try
            {
                var travelerDtos = await _travelerService.GetAllWithPaginationAsync(page, pageSize);

                return travelerDtos != null ?
                        Ok(travelerDtos)
                       : StatusCode(StatusCodes.Status500InternalServerError, new { Error = "No data found." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" });
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
        public async Task<IActionResult> Create(AddTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new {Message = "Validation error.", Errors = errors });
            }

            try
            {
                if (travelerDto == null)
                    return BadRequest(new { Error = "Traveler data is null." });

                // to make sure for now
                travelerDto.Id = 0;

                var result = await _travelerService.AddAsync(travelerDto);

                if (result.IsSuccess)
                    return CreatedAtAction(nameof(GetById), new { id = travelerDto.Id }, travelerDto);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" });
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
        public async Task<IActionResult> Update(int id, UpdateTravelerDto travelerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new {Message = "Validation error.", Errors = errors });
            }

            try
            {
                if (travelerDto.Id != id)
                    return BadRequest(new { Error = $"Not allowed to change Traveler's Id value which is {id}" });

                if (travelerDto == null)
                    return BadRequest(new { Error = "Traveler data is invalid." });

                var result = await _travelerService.UpdateAsync(travelerDto);

                if (result.IsSuccess)
                    return Ok(travelerDto);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" }); // Handle general exceptions
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
                return BadRequest(new { Error = "Invalid Traveler Id." });

           try
            {
                var result = await _travelerService.DeleteAsync(id);

                if (result.IsSuccess)
                    return NoContent();
                else
                {
                    var errors = result.Errors?.ToList() ?? new List<string>();

                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Message = "Error deleting traveler.",
                        Errors = errors
                    });
                }
             

            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Service error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = $"Unexpected error: {ex.Message}" }); // Handle general exceptions
            }
        }
    }
}
