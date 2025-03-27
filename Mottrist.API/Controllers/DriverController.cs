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


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int driverId)
        {
            try
            {
                // Call the service method
                DriverDto? result = await _driverService.GetByIdAsync(driverId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "not fund");
                }
            }
            catch (HttpRequestException ex)
            {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> Delete(int driverId)
        {
            try
            {
                // Call the service method
                var result = await _driverService.DeleteAsync(driverId);
                if (result.IsSuccess)
                {
                    return Ok("Success");
                }
                else
                {
                    return StatusCode(500, $"Error: {result.Errors.FirstOrDefault()}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Call the service method
                var result = await _driverService.GetAllAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "not fund");
                }
            }
            catch (HttpRequestException ex)
            {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddUpdateDriverDto driverDto)
        {
            try
            {

                // Call the service method
                var result = await _driverService.AddAsync(driverDto);

                if (result.IsSuccess)
                {
                    return Ok("Success");
                }
                else
                {
                    return StatusCode(500, $"Error: {result.Errors.FirstOrDefault()}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(AddUpdateDriverDto driverDto)
        {
            try
            {

                // Call the service method
                var result = await _driverService.UpdateAsync(driverDto);

                if (result.IsSuccess)
                {
                    return Ok("Success");
                }
                else
                {
                    return StatusCode(500, $"Error: {result.Errors.FirstOrDefault()}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
