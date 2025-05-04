using Microsoft.AspNetCore.Authorization;
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
    [Route("api/carFields")]
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
        /// Service for managing car colors.
        /// </summary>
        public readonly ICarColorService _carColorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarFieldsController"/> class.
        /// </summary>
        /// <param name="carService">Service for handling general car operations.</param>
        /// <param name="carColorService">Service for managing car colors.</param>
        /// <param name="carBrandService">Service for managing car brands.</param>
        /// <param name="carBodyTypeService">Service for managing car body types.</param>
        /// <param name="carFuelTypeService">Service for managing car fuel types.</param>
        public CarFieldsController(ICarService carService, ICarColorService carColorService, ICarBrandService carBrandService, ICarBodyTypeService carBodyTypeService, ICarFuelTypeService carFuelTypeService)
        {
            _carService = carService;
            _carBrandService = carBrandService;
            _carBodyTypeService = carBodyTypeService;
            _carFuelTypeService = carFuelTypeService;
            _carColorService = carColorService;
        }

        /// <summary>
        /// Retrieves all car body types.
        /// </summary>
        /// <returns>A list of car body types.</returns>
        /// - HTTP 200 OK with the list of car body types if successful.
        /// - HTTP 204 No Content if no car body types are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        [AllowAnonymous]
        [HttpGet("all/bodyTypes", Name = "GetAllCarBodyTypes")]
        [ProducesResponseType(typeof(ApiResponse<CarBodyTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBodyTypes()
        {
            try
            {
                var dataResult = await _carBodyTypeService.GetAllAsync();

                return  dataResult != null
                  ? SuccessResponse(dataResult, "Car's body types retrieved successfully.")
                   : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of all available car colors.
        /// </summary>
        /// <returns>
        /// An API response containing car color details.
        /// </returns>
        /// <response code="200">Successfully retrieved car colors.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/colors", Name = "GetAllCarColors")]
        [ProducesResponseType(typeof(ApiResponse<CarColorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var dataResult = await _carColorService.GetAllAsync();

                return dataResult != null
                ? SuccessResponse(dataResult, "Car's colors retrieved successfully.")
                : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a list of all available car fuel types.
        /// </summary>
        /// <returns>
        /// An API response containing car fuel type details.
        /// </returns>
        /// <response code="200">Successfully retrieved fuel types.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/fuelTypes", Name = "GetAllFuelTypes")]
        [ProducesResponseType(typeof(ApiResponse<CarFuelTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFuelTypes()
        {
            try
            {
                var dataResult = await _carFuelTypeService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Car's fuel types retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", ex.Message);
            }
        }


        /// <summary>
        /// Retrieves all necessary car-related fields.
        /// </summary>
        /// <returns>
        /// An API response containing car field details.
        /// </returns>
        /// <response code="200">Successfully retrieved car fields.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/carFields", Name = "GetAllCarFields")]
        [ProducesResponseType(typeof(ApiResponse<CarFieldsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCarFields()
        {
            try
            {
                var carModelDto = await _carService.GetAllCarFieldsAsync();

                return carModelDto != null
                    ? SuccessResponse(carModelDto, "Car's models retrieved successfully.")
                   : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");

            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

    }
}
