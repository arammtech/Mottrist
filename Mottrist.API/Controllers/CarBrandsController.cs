using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.Traveller.DTOs;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly ICarBrandService _brandService;

        public CarBrandsController(ICarBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("{id:int}", Name = "GetBrandByIdAsync")]
        [ProducesResponseType(typeof(CarBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {

            if (Id <= 0)
                return BadRequestResponse("INVALID_BRAND_ID", "Brand id not valid.", "Brand Id should be positive number");

            try
            {
                CarBrandDto? brandDto = await _brandService.GetByIdAsync(Id);

                return brandDto != null ? SuccessResponse<CarBrandDto>(brandDto, "Brand retrieved successfully.")
                       : NotFoundResponse("BRAND_NOT_FOUND", "BRAND not found.", $"BRAND with Id {Id} was not found.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
