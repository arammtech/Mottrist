using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Service.Features.Drivers.Services;

namespace Mottrist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        public readonly IDriverService _driverService;
        //IFormFile file; // for file upload
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
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
                var result = await _driverService.GetAllAsync();

                // Check if the result is not null and return accordingly
                return result != null
                    ? Ok(result)
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
        public async Task<IActionResult> AddAsync(AddUpdateDriverDto driverDto)
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
                return result.IsSuccess
                    ? Ok("Success")
                    : StatusCode(500, new { Error = result.Errors.FirstOrDefault() });
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
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(AddUpdateDriverDto driverDto)
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
                    ? Ok("Success") 
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
                    ? Ok(new { Message = "Driver successfully deleted." })
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
