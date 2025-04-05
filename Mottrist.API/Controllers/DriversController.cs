using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using global::Mottrist.API.Response;
using Microsoft.AspNetCore.Mvc;
using static Mottrist.API.Response.ApiResponseHelper;
using Mottrist.Service.Features.General.DTOs;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Global;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing driver-related operations.
    /// </summary>
    [Route("api/Drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        /// <summary>
        /// Driver service instance for handling driver-related operations.
        /// </summary>
        private readonly IDriverService _driverService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriversController"/> class.
        /// </summary>
        /// <param name="driverService">The driver service to manage driver-related operations.</param>
        /// <remarks>
        /// The driver service is injected using dependency injection.
        /// </remarks>
        public DriversController(IDriverService driverService)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        }

        /// <summary>
        /// Retrieves a driver by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to retrieve.</param>
        /// <returns>
        /// - HTTP 200 OK with the driver details if found.
        /// - HTTP 404 Not Found if no driver exists with the given ID.
        /// - HTTP 400 Bad Request if the provided ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("{id:int}", Name = "GetDriverByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequestResponse("InvalidId", "The provided driver ID is invalid.");
            }

            try
            {
                // Attempt to retrieve the driver details by the specified ID.
                var driver = await _driverService.GetByIdAsync(id);

                if (driver == null)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                return SuccessResponse(driver, "Driver retrieved successfully.");
            }
            catch (HttpRequestException httpEx)
            {
                // This exception is typically thrown for issues related to HTTP requests.
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", httpEx.Message);
            }
            catch (Exception ex)
            {
                // Catch-all for any unexpected exceptions.
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all drivers from the service.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of drivers if successful.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("All", Name = "GetAllDriversAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _driverService.GetAllAsync();

                // If no data is found, return a NoContent response.
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
        /// <returns>
        /// - HTTP 200 OK with a paginated list of drivers.
        /// - HTTP 400 Bad Request if the pagination parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("AllWithPagination", Name = "GetAllDriversWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithPaginationAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Validate pagination input parameters.
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var result = await _driverService.GetAllWithPaginationAsync(page, pageSize);

                // If no drivers are found, return a NoContent response.
                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified parameters.");
                }

                return SuccessResponse(result, "Paginated drivers retrieved successfully.");
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
        /// Retrieves a paginated list of drivers based on their status as a string.
        /// </summary>
        /// <param name="status">The status as a string to filter drivers by.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of records per page. Defaults to 10.</param>
        /// <returns>
        /// - HTTP 200 OK with a paginated list of drivers.
        /// - HTTP 400 Bad Request if the pagination parameters or status are invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("ByStatusWithPagination", Name = "GetDriversByStatusWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByStatusWithPaginationAsync(
            string status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (!Enum.TryParse(typeof(DriverStatus), status, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{status}' is invalid.");
                }

                // Validate pagination input parameters
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var result = await _driverService.GetAllWithPaginationAsync(
                    page, pageSize, driver => driver.Status == (DriverStatus)parsedStatus);

                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified parameters.");
                }

                return SuccessResponse(result, "Paginated drivers retrieved successfully.");
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
        /// Retrieves all drivers based on their status as a string.
        /// </summary>
        /// <param name="status">The status as a string to filter drivers by.</param>
        /// <returns>
        /// - HTTP 200 OK with the list of drivers.
        /// - HTTP 400 Bad Request if the status is invalid.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("ByStatus/{status}", Name = "GetDriversByStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByStatusAsync(string status)
        {
            try
            {
                if (!Enum.TryParse(typeof(DriverStatus), status, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{status}' is invalid.");
                }

                var dataResult = await _driverService.GetAllAsync(driver => driver.Status == (DriverStatus)parsedStatus);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found with the specified status.");
                }

                return SuccessResponse(dataResult, "Drivers retrieved successfully.");
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
        /// Updates the status of a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver to update.</param>
        /// <param name="newStatus">The new status to assign to the driver (provided as a string).</param>
        /// <returns>
        /// - HTTP 200 OK if the status is updated successfully.
        /// - HTTP 400 Bad Request if the provided status is invalid.
        /// - HTTP 404 Not Found if the driver does not exist.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPut("{driverId}/Status", Name = "UpdateDriverStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDriverStatusAsync(int driverId, [FromBody] string newStatus)
        {
            try
            {
                // Validate and parse the provided status string
                if (!Enum.TryParse(typeof(DriverStatus), newStatus, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{newStatus}' is invalid.");
                }

                var isFound = await _driverService.DoesDriverExistByIdAsync(driverId);
                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                var result = await _driverService.UpdateDriverStatusAsync(driverId, (DriverStatus)parsedStatus);

                return result.IsSuccess
                    ? SuccessResponse( "Driver details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }



        /// <summary>
        /// Adds a new driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The DTO containing the details of the driver to be added.</param>
        /// <returns>
        /// - HTTP 201 Created if the operation is successful.
        /// - HTTP 400 Bad Request if there are validation errors.
        /// - HTTP 409 Conflict if a driver with the same unique details already exists.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPost("Add", Name = "AddNewDriverAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] AddDriverDto driverDto)
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
                // Validate uniqueness of the email
                var existingUser = await _driverService.DoesDriverExistByEmailAsync(driverDto.Email);
                if (existingUser)
                {
                    return StatusCodeResponse(StatusCodes.Status409Conflict, "DuplicateUser", "Driver already exists.");
                }

                var result = await _driverService.AddAsync(driverDto);


                return result.IsSuccess
                    ? CreatedResponse("GetDriverByIdAsync", new { id = driverDto.Id }, new { id = driverDto.Id }, "Driver created successfully.")
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
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)] // Successful update
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Validation errors
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)] // Driver not found
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]UpdateDriverDto driverDto)
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
                bool isFound = await _driverService.DoesDriverExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                // Set the driver id based on the route parameter.
                driverDto.Id = id;
                var result = await _driverService.UpdateAsync(driverDto);

                return result.IsSuccess
                    ? SuccessResponse(result, "Driver details updated successfully.")
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
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)] // Driver deleted successfully
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Invalid driver ID provided
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The driver ID provided is invalid.");
            }

            try
            {
                bool isFound = await _driverService.DoesDriverExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

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