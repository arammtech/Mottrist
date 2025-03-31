using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using global::Mottrist.API.Response;
using Microsoft.AspNetCore.Mvc;
using static Mottrist.API.Response.ApiResponseHelper;
using Mottrist.Domain.Entities;
using System.Linq.Expressions;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing driver-related operations.
    /// </summary>
    [Route("api/Driver")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        /// <summary>
        /// Driver service instance for handling driver-related operations.
        /// </summary>
        private readonly IDriverService _driverService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverController"/> class.
        /// </summary>
        /// <param name="driverService">The driver service to manage driver-related operations.</param>
        /// <remarks>
        /// The driver service is injected using dependency injection.
        /// </remarks>
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        }

        /// <summary>
        /// Retrieves a driver by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to retrieve.</param>
        /// <returns>
        /// Returns:
        /// - HTTP 200 OK with the driver details if successful.
        /// - HTTP 404 Not Found if no driver is found with the given ID.
        /// - HTTP 400 Bad Request if the driver ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("{id:int}", Name = "GetDriverByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidId", "The driver ID provided is invalid.");
            }

            try
            {
                var result = await _driverService.GetByIdAsync(id);
                return result != null
                    ? SuccessResponse(result, "Driver retrieved successfully.")
                    : NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
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
        /// Retrieves all drivers from the service.
        /// </summary>
        /// <returns>
        /// Returns an HTTP 200 OK status with the list of drivers if successful, 
        /// or an HTTP 500 Internal Server Error in case of failure.
        /// </returns>
        [HttpGet("All", Name = "GetAllDriversAsync")]
        [ProducesResponseType(typeof(ApiResponse<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _driverService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No data content available.");
                }

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
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
        /// Retrieves a paginated list of drivers based on the provided parameters.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of records per page. Defaults to 10.</param>
        /// <param name="filter">An optional filter expression to filter drivers (optional).</param>
        /// <returns>
        /// An HTTP 200 OK response with a paginated list of drivers, 
        /// or an HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("AllWithPagination", Name = "GetAllDriversWithPaginationAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithPaginationAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Validate pagination inputs
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse(
                        "PaginationError",
                        "Both page and pageSize must be greater than 0.");
                }

                // Fetch paginated drivers
                var result = await _driverService.GetAllWithPaginationAsync(page, pageSize);

                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified parameters.");
                }

                // Return paginated drivers
                return SuccessResponse(result, "Paginated drivers retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.StatusCodeResponse(
                    StatusCodes.Status500InternalServerError,
                    "InternalServerError",
                    "An unexpected error occurred.",
                    ex.Message);
            }
        }

        /// <summary>
        /// Adds a new driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The DTO containing the details of the driver to be added.</param>
        /// <returns>
        /// Returns:
        /// - HTTP 201 Created if the operation is successful.
        /// - HTTP 400 Bad Request if there are validation errors.
        /// - HTTP 409 Conflict if a driver with the same unique details already exists.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPost("Add", Name = "AddNewDriverAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Successful creation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Validation errors
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Duplicate resource
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> AddAsync([FromForm]AddDriverDto driverDto)
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
                var result = await _driverService.AddAsync(driverDto);

                return result.IsExist
                    ? StatusCodeResponse(StatusCodes.Status409Conflict, "DuplicateUser", "Driver already exists.")
                    : result.IsSuccess
                        ? CreatedResponse("GetByIdAsync", new { id = driverDto.Id }, driverDto, "Driver created successfully.")
                        : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create driver.");
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
        /// Updates the details of an existing driver in the system using the provided data transfer object.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to be updated.</param>
        /// <param name="driverDto">The data transfer object containing updated driver details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - HTTP 200 OK if the operation is successful.
        /// - HTTP 400 Bad Request if validation fails.
        /// - HTTP 404 Not Found if the driver does not exist.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPut("{id:int}", Name = "UpdateDriverDetailsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Successful update
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Validation errors
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Driver not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDriverDto driverDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequestResponse("ValidationError", "Invalid data provided.", errors.ToArray());
            }

            if (id < 1)
            {
                return BadRequestResponse("InvalidId", $"The parameter '{nameof(id)}' must be a positive integer.");
            }

            try
            {
                // Pass ID and DTO to the service layer; let it handle existence validation
                driverDto.Id = id;
                var result = await _driverService.UpdateAsync(driverDto);

                return result.IsSuccess
                    ? SuccessResponse(result, "Driver details updated successfully.")
                    : result.IsNotFound
                        ? NotFoundResponse("DriverNotFound", "No driver found with the provided ID.")
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
        /// Deletes a driver by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to be deleted.</param>
        /// <returns>
        /// Returns:
        /// - HTTP 200 OK if the deletion is successful.
        /// - HTTP 400 Bad Request if the driver ID is invalid.
        /// - HTTP 500 Internal Server Error with detailed error information for failures.
        /// </returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Deletion successful
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Invalid ID
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The driver ID provided is invalid.");
            }

            try
            {
                var result = await _driverService.DeleteAsync(id);
                return result.IsSuccess
                    ? SuccessResponse("Driver deleted successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "DeletionError", "Failed to delete the driver.");
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
