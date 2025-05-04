using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Interfaces;
using Mottrist.Service.Features.Destinations.DTOs;
using System;
using System.Threading.Tasks;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;
using Microsoft.AspNetCore.Authorization;
using Mottrist.Utilities.Identity;
using System.Data;

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
            _destinationService = destinationService;
        }

        /// <summary>
        /// Retrieves the details of a driver based on the specified ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the driver to retrieve.
        /// </param>
        /// <returns>
        /// An API response containing driver details.
        /// </returns>
        /// <response code="200">Successfully retrieved driver details.</response>
        /// <response code="400">Bad request due to invalid ID.</response>
        /// <response code="404">Driver not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetDestinationByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<DestinationDto>), StatusCodes.Status200OK)]
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
                var destination = await _destinationService.GetByIdAsync(id);

                if (destination is null)
                    return NotFoundResponse("DestinationNotFound", "No destination found with the provided ID.");

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
        /// Retrieves a list of all available destinations.
        /// </summary>
        /// <returns>
        /// An API response containing destination details.
        /// </returns>
        /// <response code="200">Successfully retrieved destination data.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all", Name = "GetAllDestinationsAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DestinationDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _destinationService.GetAllAsync();

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
        /// Retrieves a paginated list of destinations.
        /// </summary>
        /// <param name="page">
        /// The page number to retrieve (default is 1).
        /// </param>
        /// <param name="pageSize">
        /// The number of destinations per page (default is 10).
        /// </param>
        /// <returns>
        /// An API response containing a paginated list of destinations.
        /// </returns>
        /// <response code="200">Successfully retrieved destination data.</response>
        /// <response code="400">Bad request due to invalid pagination parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/paged", Name = "GetAllDestinationsWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DestinationDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDestinationsWithPaginationAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                // Validate pagination input parameters.
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var result = await _destinationService.GetAllWithPaginationAsync(page, pageSize);

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
        /// Adds a new destination.
        /// </summary>
        /// <param name="destinationDto">
        /// The data transfer object containing the destination details.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the operation.
        /// </returns>
        /// <response code="201">Destination successfully created.</response>
        /// <response code="400">Bad request due to invalid input.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="401">Unauthorized access. Authentication is required.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpPost(Name = "AddNewDestinationAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] AddDestinationDto destinationDto)
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
                    ? CreatedResponse("GetDestinationByIdAsync", new { id = result.Data?.Id }, result.Data, "Destination created successfully.")
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
        /// Updates the details of an existing destination.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the destination to update.
        /// </param>
        /// <param name="destinationDto">
        /// The data transfer object containing the updated destination details.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the update operation.
        /// </returns>
        /// <response code="200">Destination details updated successfully.</response>
        /// <response code="400">Bad request due to invalid input.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="401">Unauthorized access. Authentication is required.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpPut("{id:int}", Name = "UpdateDestinationDetailsAsync")]
        [ProducesResponseType(typeof(ApiResponse<DestinationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateDestinationDto destinationDto)
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
                // Set the destination id based on the route parameter.
                var result = await _destinationService.UpdateAsync(destinationDto);

                return result.IsSuccess
                    ? SuccessResponse(result.Data, "Destination details updated successfully.")
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
        /// Deletes a destination based on the specified ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the destination to be deleted.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">Destination deleted successfully.</response>
        /// <response code="400">Bad request due to invalid destination ID.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="401">Unauthorized access. Authentication is required.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpDelete("{id:int}", Name = "DeleteDestinationAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The destination ID provided is invalid.");
            }

            try
            {
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
