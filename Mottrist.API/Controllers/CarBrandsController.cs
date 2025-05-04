using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mottrist.API.Response;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Utilities.Identity;
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

        /// <summary>
        /// Retrieves a car brand by its unique identifier.
        /// </summary>
        /// <param name="Id">The ID of the brand to retrieve.</param>
        /// <returns>The car brand data if found.</returns>
        /// <response code="200">Brand retrieved successfully.</response>
        /// <response code="400">Invalid brand ID.</response>
        /// <response code="404">Brand not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("{Id:int}", Name = "GetBrandByIdAsync")]
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

                return brandDto != null ? SuccessResponse(brandDto, "Brand retrieved successfully.")
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

        /// <summary>
        /// Retrieves all car brands.
        /// </summary>
        /// <returns>List of all car brands.</returns>
        /// <response code="200">Brands retrieved successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all", Name = "GetAllBrandsAsync")]
        [ProducesResponseType(typeof(CarBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _brandService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Brands retrieved successfully.")
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
        /// Retrieves paginated list of car brands.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns>Paginated result of car brands.</returns>
        /// <response code="200">Paginated brands retrieved successfully.</response>
        /// <response code="400">Invalid pagination parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/paged", Name = "GetAllBrandsWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<CarBrandDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDriversWithPaginationAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");

                var result = await _brandService.GetAllWithPaginationAsync(page, pageSize);

                return result != null
                    ? SuccessResponse(result, "Paginated brands retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error:");
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
        /// Adds a new car brand.
        /// </summary>
        /// <param name="addCarBrandDto">The car brand data to add.</param>
        /// <returns>The created brand with its ID.</returns>
        /// <response code="201">Brand created successfully.</response>
        /// <response code="400">Validation errors.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpPost(Name = "AddNewBrandAsync")]
        [ProducesResponseType(typeof(CarBrandDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] AddCarBrandDto addCarBrandDto)
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
                var result = await _brandService.AddAsync(addCarBrandDto);

                return result.IsSuccess
                    ? CreatedResponse("GetBrandByIdAsync", new { id = result.Data?.Id }, result.Data, "Brand created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create brand.");
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
        /// Updates an existing car brand.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="updateCarBrandDto">The updated brand data.</param>
        /// <returns>Updated brand information.</returns>
        /// <response code="200">Brand updated successfully.</response>
        /// <response code="400">Invalid brand ID or validation errors.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpPut("{id:int}", Name = "UpdateBrandAsync")]
        [ProducesResponseType(typeof(CarBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateCarBrandDto updateCarBrandDto)
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
                return BadRequestResponse("InvalidId", $"The parameter '{nameof(id)}' must be a positive integer.");

            try
            {
                var result = await _brandService.UpdateAsync(updateCarBrandDto);

                return result.IsSuccess
                    ? SuccessResponse(result.Data, "Brand updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a car brand by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the brand to delete.</param>
        /// <returns>A success message if deleted; otherwise, an error message.</returns>
        /// <response code="200">Brand deleted successfully.</response>
        /// <response code="400">Invalid brand ID.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpDelete("{id:int}", Name = "DeleteBrandAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
                return BadRequestResponse("InvalidId", "The brand ID provided is invalid.");

            try
            {
                var result = await _brandService.DeleteAsync(id);

                return result.IsSuccess
                    ? SuccessResponse("Brand deleted successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "DeletionError", "Failed to delete the brand.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
