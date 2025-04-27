using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Interfaces;
using Mottrist.Service.Features.Destinations.DTOs;
using System;
using System.Threading.Tasks;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing destination-related operations.
    /// </summary>
    [Route("api/destinations")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        /// <summary>
        /// Destination service instance for handling destination-related operations.
        /// </summary>
        private readonly IDestinationService _destinationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DestinationController"/> class.
        /// </summary>
        /// <param name="destinationService">The destination service to manage operations.</param>
        /// <remarks>
        /// The destination service is injected using dependency injection.
        /// </remarks>
        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService ?? throw new ArgumentNullException(nameof(destinationService));
        }

        /// <summary>
        /// Retrieves a destination by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the destination to retrieve.</param>
        /// <returns>
        /// - HTTP 200 OK with the destination details if found.
        /// - HTTP 404 Not Found if no destination exists with the given ID.
        /// - HTTP 400 Bad Request if the provided ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("{id:int}", Name = "GetDestinationByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<DestinationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // Validate the provided ID
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The provided destination ID is invalid.");
            }

            try
            {
                bool destinationExists = await _destinationService.DoesDestinationExistByIdAsync(id);

                if (!destinationExists)
                {
                    return NotFoundResponse("DestinationNotFound", "No destination found with the provided ID.");
                }

                var destination = await _destinationService.GetByIdAsync(id);

                return SuccessResponse(destination, "Destination retrieved successfully.");
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
        /// Retrieves all destinations from the service.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of destinations if successful.
        /// - HTTP 204 No Content if no destinations are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("all", Name = "GetAllDestinationsAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DestinationDTO>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _destinationService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No data content available.");
                }

                return dataResult != null
                    ? SuccessResponse(dataResult, "Destinations retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
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

        /// <summary>
        /// Retrieves a paginated list of destinations based on the specified page and page size.
        /// </summary>
        /// <param name="page">
        /// The page number for paginated results.
        /// Must be greater than 0. Defaults to 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of records per page.
        /// Must be greater than 0. Defaults to 10.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a paginated list of destinations.
        /// - HTTP 204 No Content if no destinations are found.
        /// - HTTP 400 Bad Request if pagination parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("all/page/{page:int}/size/{pageSize:int}", Name = "GetAllDestinationsWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DestinationDTO>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDestinationsWithPaginationAsync(
           int page = 1,
           int pageSize = 10)
        {
            try
            {
                // Validate pagination input parameters.
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var result = await _destinationService.GetAllWithPaginationAsync(page, pageSize);

                // If no destinations are found, return a NoContent response.
                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No destinations found for the specified parameters.");
                }

                return result != null
                    ? SuccessResponse(result, "Paginated destinations retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error:");
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

        /// <summary>
        /// Adds a new destination using the provided data transfer object.
        /// </summary>
        /// <param name="destinationDto">The DTO containing the details of the destination to be added.</param>
        /// <returns>
        /// - HTTP 201 Created if the operation is successful.
        /// - HTTP 400 Bad Request if there are validation errors.
        /// - HTTP 409 Conflict if a destination with the same unique details already exists.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPost(Name = "AddNewDestinationAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] DestinationDTO destinationDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return BadRequestResponse("ValidationError", "Invalid data provided.", errors.ToArray());
            }

            try
            {
                var result = await _destinationService.AddAsync(destinationDto);

                return result.IsSuccess
                    ? CreatedResponse("GetDestinationByIdAsync", new { id = destinationDto.Id }, new { id = destinationDto.Id }, "Destination created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create destination.");
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

        /// <summary>
        /// Updates an existing destination using the provided data transfer object.
        /// </summary>
        /// <param name="id">The unique identifier of the destination to be updated.</param>
        /// <param name="destinationDto">The data transfer object containing updated destination details.</param>
        /// <returns>
        /// - HTTP 200 OK if the update is successful.
        /// - HTTP 400 Bad Request if validation fails.
        /// - HTTP 404 Not Found if the destination does not exist.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("{id:int}", Name = "UpdateDestinationDetailsAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DestinationDTO destinationDto)
        {
            // Validate the input model
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequestResponse("ValidationError", "Invalid data provided.", errors.ToArray());
            }

            // Ensure a valid id is provided
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", $"The parameter '{nameof(id)}' must be a positive integer.");
            }

            try
            {
                bool isFound = await _destinationService.DoesDestinationExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DestinationNotFound", "No destination found with the provided ID.");
                }

                // Set the destination id based on the route parameter.
                destinationDto.Id = id;
                var result = await _destinationService.UpdateAsync(destinationDto);

                return result.IsSuccess
                    ? SuccessResponse(result, "Destination details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
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

        /// <summary>
        /// Deletes a destination by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the destination to be deleted.</param>
        /// <returns>
        /// - HTTP 200 OK if the deletion is successful.
        /// - HTTP 400 Bad Request if the destination ID is invalid.
        /// - HTTP 404 Not Found if the destination does not exist.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("{id:int}", Name = "DeleteDestinationAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The destination ID provided is invalid.");
            }

            try
            {
                bool isFound = await _destinationService.DoesDestinationExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DestinationNotFound", "No destination found with the provided ID.");
                }

                var result = await _destinationService.DeleteAsync(id);

                return result.IsSuccess
                    ? SuccessResponse("Destination deleted successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "DeletionError", "Failed to delete the destination.");
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
