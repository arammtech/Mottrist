using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Countries.Interfaces;
using Mottrist.Service.Features.Countries.Services;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsControllers : ControllerBase
    {

        private readonly ICarService _carService;

        public CarsControllers(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var dataResult = await _carService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No countries found.");
                }

                return SuccessResponse(dataResult, "Countries retrieved successfully.");
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
