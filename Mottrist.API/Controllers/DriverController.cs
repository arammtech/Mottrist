using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;

namespace Mottrist.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace Mottrist.Service.Features.Drivers.Controllers
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
            /// Example endpoint for uploading files (if implemented).
            /// </summary>
            /// <remarks>
            /// To implement file uploads, use the <see cref="IFormFile"/> parameter in a dedicated method.
            /// </remarks>
            /// <returns>A placeholder result indicating success or failure of file upload implementation.</returns>
            [HttpPost("upload")]
            public IActionResult UploadFile(IFormFile file)
            {
                return Ok("File upload functionality has yet to be implemented.");
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
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetByIdAsync(int id)
            {
                // Validate input
                if (id <= 0)
                {
                    return BadRequest(new { Error = "Invalid driver ID." });
                }

                try
                {
                    // Fetch the driver details from the service
                    DriverDto? result = await _driverService.GetByIdAsync(id);

                    // Return appropriate response based on the service result
                    return result != null
                        ? Ok(result)
                        : NotFound(new { Error = "Driver not found." });
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exceptions
                    return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
                }
                catch (Exception ex)
                {
                    // Handle unexpected exceptions
                    return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
                }
            }

            /// <summary>
            /// Retrieves all drivers from the service.
            /// </summary>
            /// <returns>
            /// Returns an HTTP 200 OK status with the list of drivers if successful, 
            /// or an HTTP 500 Internal Server Error in case of failure.
            /// </returns>
            [HttpGet]
            public async Task<IActionResult> GetAllAsync()
            {
                try
                {
                    var dataResult = await _driverService.GetAllAsync();

                    if(dataResult?.DataRecordsCount?.Equals(0) ?? false)
                    {
                        return StatusCode(203, "No data Content.");
                    }

                    // Check if the result is not null and return accordingly
                    return dataResult != null
                        ? Ok(dataResult)
                        : StatusCode(500, "No data found.");
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exceptions and return a structured error response
                    return StatusCode(500, new { Error = ex.Message });
                }
                catch (Exception ex)
                {
                    // Handle general exceptions and return a structured error response
                    return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
                }
            }

            /// <summary>
            /// Adds a new driver using the provided data transfer object.
            /// </summary>
            /// <param name="driverDto">The DTO containing the details of the driver to be added.</param>
            /// <returns>
            /// Returns an HTTP 200 OK status if the operation is successful, 
            /// HTTP 400 Bad Request if there are validation errors, or 
            /// HTTP 500 Internal Server Error for any exceptions or failures.
            /// </returns>
            [HttpPost]
            public async Task<IActionResult> AddAsync(AddDriverDto driverDto)
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
                    var result = await _driverService.AddAsync(driverDto);

                    // Use ternary operator for concise response handling
                    if (result.IsSuccess)
                    {
                        return CreatedAtAction("GetById", new { id = driverDto.Id }, driverDto);
                    }
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
                    return StatusCode(500, new { Error = ex.Message }); // Handle HttpRequest exception
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" }); // Handle general exceptions
                }
            }

            /// <summary>
            /// Updates the details of a driver using the provided data transfer object.
            /// </summary>
            /// <param name="driverDto">The DTO containing driver details to be updated.</param>
            /// <returns>
            /// Returns an HTTP 200 OK status if the operation is successful, 
            /// HTTP 400 Bad Request if there are validation errors, or 
            /// HTTP 500 Internal Server Error for any exceptions or failures.
            /// </returns>
            [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateAsync(UpdateDriverDto driverDto)
            {
                if (!ModelState.IsValid)
                {
                    // Extract and return validation errors as a bad request response
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { Errors = errors });
                }

                try
                {
                    var result = await _driverService.UpdateAsync(driverDto);

                    // Return a success response if the operation succeeds
                    // or an internal server error with the first error message otherwise
                    return result.IsSuccess
                        ? Ok(result)
                        : StatusCode(500, new { Error = result.Errors.FirstOrDefault() });
                }
                catch (HttpRequestException ex)
                {
                    // Return a structured error response for HTTP request exceptions
                    return StatusCode(500, new { Error = ex.Message });
                }
                catch (Exception ex)
                {
                    // Return a generic error response for unexpected exceptions
                    return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
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
            public async Task<IActionResult> DeleteAsync(int id)
            {
                if (id <= 0)
                {
                    return BadRequest(new { Error = "Invalid driver ID." });
                }

                try
                {
                    var result = await _driverService.DeleteAsync(id);

                    // Return success or error based on the service result
                    return result.IsSuccess
                        ? Ok("Deleted Successfully")
                        : StatusCode(500, new { Error = result.Errors.FirstOrDefault() });
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exceptions
                    return StatusCode(500, new { Error = $"Service error: {ex.Message}" });
                }
                catch (Exception ex)
                {
                    // Handle unexpected exceptions
                    return StatusCode(500, new { Error = $"Unexpected error: {ex.Message}" });
                }
            }
        }
    }
}
