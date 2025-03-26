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

        [HttpGet("AddDriver")]
        public async Task<IActionResult> AddDriver()
        {
            try
            {
                // Create a sample driver
                AddUpdateDriverDto driverDto = new AddUpdateDriverDto
                {
                    WhatsAppNumber = "+2012tr34567890",
                    NationailtyId = 1,
                    FirstName = "J3ohr3tghn",
                    LastName = "D3totrhge",
                    Email = "j3orthnr7tr.doghhge@examplhghge.com",
                    UserName = "johtn7eg3rereregrtrhggdoe",
                    PhoneNumber = "1234567hg890",
                    PasswordHash = "Pas3s@1234",
                    LicenseImageUrl = "https://example.com/images/johnw.jpg",
                    ProfileImageUrl = "https://example.com/images/john.jpg",
                    PassportImageUrl = "https://example.com/images/johnp.jpg",
                    YearsOfExperience = 5,
                    Bio = "I am a professional driver",
                    HasCar = true,
                    BrandId = 1,
                    Year = 2022,
                    NumberOfSeats = 5,
                    ModelId = 1,
                    ColorId = 1,
                    BodyTypeId = 1,
                    FuelTypeId = 1,
                    CarImageUrl = "https://example.com/car_image.jpg"
                };

                // Call the service method
                var result = await _driverService.AddDriverAsync(driverDto);

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
