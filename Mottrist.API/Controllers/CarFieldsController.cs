using Microsoft.AspNetCore.Mvc;
using Mottrist.API.Response;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.Cities.Dtos;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// Controller for managing car-related fields such as brands, body types, fuel types, models, and colors.
    /// </summary>
    [Route("api/CarFields")]
    [ApiController]
    public class CarFieldsController : ControllerBase
    {
        /// <summary>
        /// Service for handling general car operations.
        /// </summary>
        private readonly ICarService _carService;

        /// <summary>
        /// Service for managing car brands.
        /// </summary>
        private readonly ICarBrandService _carBrandService;

        /// <summary>
        /// Service for managing car body types.
        /// </summary>
        private readonly ICarBodyTypeService _carBodyTypeService;

        /// <summary>
        /// Service for managing car fuel types.
        /// </summary>
        private readonly ICarFuelTypeService _carFuelTypeService;

        /// <summary>
        /// Service for managing car models.
        /// </summary>
        private readonly ICarModelService _carModelService;

        /// <summary>
        /// Service for managing car colors.
        /// </summary>
        public readonly ICarColorService _carColorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarFieldsController"/> class.
        /// </summary>
        /// <param name="carService">Service for handling general car operations.</param>
        /// <param name="carModelService">Service for managing car models.</param>
        /// <param name="carColorService">Service for managing car colors.</param>
        /// <param name="carBrandService">Service for managing car brands.</param>
        /// <param name="carBodyTypeService">Service for managing car body types.</param>
        /// <param name="carFuelTypeService">Service for managing car fuel types.</param>
        public CarFieldsController(ICarService carService, ICarModelService carModelService, ICarColorService carColorService, ICarBrandService carBrandService, ICarBodyTypeService carBodyTypeService, ICarFuelTypeService carFuelTypeService)
        {
            _carService = carService;
            _carBrandService = carBrandService;
            _carBodyTypeService = carBodyTypeService;
            _carFuelTypeService = carFuelTypeService;
            _carModelService = carModelService;
            _carColorService = carColorService;
        }

        /// <summary>
        /// Retrieves all car body types.
        /// </summary>
        /// <returns>A list of car body types.</returns>
        /// - HTTP 200 OK with the list of car body types if successful.
        /// - HTTP 204 No Content if no car body types are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/BodyTypes", Name = "GetAllCarBodyTypes")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBodyTypes()
        {
            try
            {
                var dataResult = await _carBodyTypeService.GetAllAsync();
                if (dataResult?.DataRecordsCount == 0)
                {
                    return NoContentResponse("No Car's body types found.");
                }
                return SuccessResponse(dataResult, "Car's body types retrieved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all car brands.
        /// </summary>
        /// <returns>A list of car brands.</returns>
        /// - HTTP 200 OK with the list of car brands if successful.
        /// - HTTP 204 No Content if no car brands are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/Brands", Name = "GetAllCarBrands")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var dataResult = await _carBrandService.GetAllAsync();
                if (dataResult?.DataRecordsCount == 0)
                {
                    return NoContentResponse("No Car's brands found.");
                }
                return SuccessResponse(dataResult, "Car's brands retrieved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all car colors.
        /// </summary>
        /// <returns>A list of car colors.</returns>
        /// - HTTP 200 OK with the list of car colors if successful.
        /// - HTTP 204 No Content if no car colors are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/Colors", Name = "GetAllCarColors")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var dataResult = await _carColorService.GetAllAsync();
                if (dataResult?.DataRecordsCount == 0)
                {
                    return NoContentResponse("No Car's colors found.");
                }
                return SuccessResponse(dataResult, "Car's colors retrieved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all car fuel types.
        /// </summary>
        /// <returns>A list of car fuel types.</returns>
        /// - HTTP 200 OK with the list of car fuel types if successful.
        /// - HTTP 204 No Content if no car fuel types are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/FuelTypes", Name = "GetAllFuelTypes")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFuelTypes()
        {
            try
            {
                var dataResult = await _carFuelTypeService.GetAllAsync();
                if (dataResult?.DataRecordsCount == 0)
                {
                    return NoContentResponse("No Car's fuel types found.");
                }
                return SuccessResponse(dataResult, "Car's fuel types retrieved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all car models.
        /// </summary>
        /// <returns>A list of car models.</returns>
        /// - HTTP 200 OK with the list of car models if successful.
        /// - HTTP 204 No Content if no car models are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/Models", Name = "GetAllModels")]
        [ProducesResponseType(typeof(ApiResponse<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllModels()
        {
            try
            {
                var dataResult = await _carModelService.GetAllAsync();

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No Car's models  found.");
                }

                return SuccessResponse(dataResult, "Car's models retrieved successfully.");
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
        /// Retrieves all car fields.
        /// </summary>
        /// <returns>A list of car fields.</returns>
        /// - HTTP 200 OK with the list of car fields if successful.
        /// - HTTP 204 No Content if no car fields are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [HttpGet("All/CarFields", Name = "GetAllCarFields")]
        [ProducesResponseType(typeof(ApiResponse<CarFieldsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCarFields()
        {
            try
            {
                var carModelDto = await _carService.GetAllCarFieldsAsync();

                return SuccessResponse(carModelDto, "Car's models retrieved successfully.");
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
