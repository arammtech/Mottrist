using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using global::Mottrist.API.Response;
using Microsoft.AspNetCore.Mvc;
using static Mottrist.API.Response.ApiResponseHelper;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Mottrist.Utilities.Identity;
using Mottrist.Domain.Global;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API Controller for managing driver-related operations.
    /// </summary>
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        /// <summary>
        /// Driver service instance for handling driver-related operations.
        /// </summary>
        private readonly IDriverService _driverService;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="DriversController"/> class.
        /// </summary>
        /// <param name="driverService">The driver service to manage driver-related operations.</param>
        /// <remarks>
        /// The driver service is injected using dependency injection.
        /// </remarks>
        public DriversController(IDriverService driverService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _driverService = driverService;
        }

        /// <summary>
        /// Retrieves a driver by their unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver.</param>
        /// <returns>
        /// The driver details corresponding to the provided ID.
        /// </returns>
        /// <response code="200">Returns the driver details.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="404">Driver not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetDriverByIdAsync")]
        [ProducesResponseType(typeof(ApiResponse<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequestResponse("InvalidId", "The provided driver ID is invalid.");
            }

            try
            {
                var driver = await _driverService.GetByIdAsync(id);

                if(driver is null)
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");

                return SuccessResponse(driver, "Driver retrieved successfully.");
            }

            catch (Exception ex)
            {
                // Catch-all for any unexpected exceptions.
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of all drivers.
        /// </summary>
        /// <returns>
        /// A data result containing details of all available drivers.
        /// </returns>
        /// <response code="200">Returns a list of drivers.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all", Name = "GetAllDriversAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _driverService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a paginated list of drivers.
        /// </summary>
        /// <param name="page">The page number of the paginated result. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <returns>
        /// A paginated result containing driver details.
        /// </returns>
        /// <response code="200">Returns a paginated list of drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all/paged", Name = "GetAllDriversWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDriversWithPaginationAsync(int page = 1,int pageSize = 10)
        {
            try
            {
                // Validate pagination input parameters.
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var dataResult = await _driverService.GetAllWithPaginationAsync(page, pageSize);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of top-rated drivers.
        /// </summary>
        /// <param name="count">The number of top-rated drivers to retrieve. Default is 3.</param>
        /// <returns>
        /// A data result containing details of the top-rated drivers.
        /// </returns>
        /// <response code="200">Returns a list of top-rated drivers.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("top-rated", Name = "GetTopRatedDriversAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopRatedAsync(int count = 3)
        {
            try
            {
                var dataResult = await _driverService.GetTopRatedAsync(count);

                return dataResult != null 
                    ? SuccessResponse(dataResult, "Top-rated drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves a list of drivers based on the specified country.
        /// </summary>
        /// <param name="countryId">The unique identifier of the country.</param>
        /// <returns>
        /// A data result containing driver details filtered by country.
        /// </returns>
        /// <response code="200">Returns a list of drivers for the specified country.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("by-country/{countryId:int}", Name = "GetDriversByCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryAsync(int countryId)
        {
            if (countryId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID parameter is required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country only.
                var dataResult = await _driverService.GetByCountryAsync(countryId);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of drivers based on the specified country and city.
        /// </summary>
        /// <param name="countryId">The unique identifier of the country.</param>
        /// <param name="cityId">The unique identifier of the city.</param>
        /// <returns>
        /// A data result containing driver details filtered by country and city.
        /// </returns>
        /// <response code="200">Returns a list of drivers for the specified country and city.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("by-country/{countryId:int}/city/{cityId:int}", Name = "GetDriversByCountryAndCityAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryAndCityAsync( int countryId, int cityId)
        {
            if (countryId < 1 || cityId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Both country and city ID parameters are required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country and city.
                var dataResult = await _driverService.GetByCountryAndCityAsync(countryId, cityId);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of drivers filtered by country, city, and availability date.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier for the country to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="cityId">
        /// The unique identifier for the city to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="date">
        /// The specific date when the driver should be available.
        /// Only drivers available on this date or marked as available all the time will be included.
        /// </param>
        /// <returns>
        /// A data result containing drivers that match the specified criteria.
        /// </returns>
        /// <response code="200">Returns a list of matching drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("by-country/{countryId:int}/city/{cityId:int}/date/{date:string}", Name = "GetDriversByCountryCityAndDateAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryCityAndDateAsync(int countryId,int cityId,DateTime date)
        {
            if (countryId < 1 || cityId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Both country and city ID parameters are required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country, city, and date.
                var dataResult = await _driverService.GetByCountryCityAndDateAsync(countryId, cityId, date);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a paginated list of drivers based on the specified country.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier of the country.
        /// </param>
        /// <param name="page">
        /// The page number of the paginated result. Default is 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of items per page. Default is 10.
        /// </param>
        /// <returns>
        /// A paginated result containing driver details filtered by country.
        /// </returns>
        /// <response code="200">Returns a paginated list of drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("paged/by-country/{countryId:int}", Name = "GetDriversByCountryWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryWithPaginationAsync(int countryId,int page = 1,int pageSize = 10)
        {
            if (countryId  < 1 || page < 1 || pageSize < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID, city ID, page, and page size must be greater than 0.");
            }

            try
            {
                var dataResult = await _driverService.GetByCountryWithPaginationAsync(countryId, page, pageSize);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a paginated list of drivers filtered by country and city.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier of the country.
        /// </param>
        /// <param name="cityId">
        /// The unique identifier of the city.
        /// </param>
        /// <param name="page">
        /// The page number of the paginated result. Default is 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of items per page. Default is 10.
        /// </param>
        /// <returns>
        /// A paginated result containing driver details filtered by country and city.
        /// </returns>
        /// <response code="200">Returns a paginated list of drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("paged/by-country/{countryId:int}/city/{cityId:int}", Name = "GetDriversByCountryAndCityWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryAndCityWithPaginationAsync(int countryId,int cityId,int page = 1,int pageSize = 10)
        {
            if (countryId < 1 || page < 1 || pageSize < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID, city ID, page, and page size must be greater than 0.");
            }

            try
            {
                var dataResult = await _driverService.GetByCountryAndCityWithPaginationAsync(countryId, cityId,page, pageSize);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves a paginated list of drivers filtered by country, city, and availability date.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier for the country.
        /// </param>
        /// <param name="cityId">
        /// The unique identifier for the city.
        /// </param>
        /// <param name="date">
        /// The specific date when the driver should be available.
        /// Only drivers available on this date or marked as available all the time will be included.
        /// </param>
        /// <param name="page">
        /// The page number of the paginated result. Default is 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of items per page. Default is 10.
        /// </param>
        /// <returns>
        /// A paginated result containing driver details filtered by country, city, and availability date.
        /// </returns>
        /// <response code="200">Returns a paginated list of matching drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("paged/by-country/{countryId:int}/city/{cityId:int}/date/{date:string}", Name = "GetDriversByCountryAndCityAndDateWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCountryCityAndDateWithPaginationAsync(int countryId,int cityId,DateTime date,int page = 1,int pageSize = 10)
        {
            if (countryId < 1 || page < 1 || pageSize < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID, city ID, page, and page size must be greater than 0.");
            }

            try
            {
                var dataResult = await _driverService.GetByCountryCityAndDateWithPaginationAsync(countryId,cityId,date, page, pageSize);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a paginated list of drivers filtered by status.
        /// </summary>
        /// <param name="status">
        /// The status used to filter drivers (e.g., "Approved", "Rejected", "Pending").
        /// </param>
        /// <param name="page">
        /// The page number of the paginated result. Default is 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of items per page. Default is 10.
        /// </param>
        /// <returns>
        /// A paginated result containing driver details filtered by status.
        /// </returns>
        /// <response code="200">Returns a paginated list of drivers.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpGet("paged/by-status", Name = "GetDriversByStatusWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStatusWithPaginationAsync(string status,int page = 1,int pageSize = 10)
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

                var dataResult = await _driverService.GetAllWithPaginationAsync(
                    page, pageSize, driver => driver.Status == (DriverStatus)parsedStatus);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of drivers filtered by status.
        /// </summary>
        /// <param name="status">
        /// The status used to filter drivers (e.g., "Approved", "Rejected", "Pending").
        /// </param>
        /// <returns>
        /// A data result containing driver details filtered by status.
        /// </returns>
        /// <response code="200">Returns a list of drivers with the specified status.</response>
        /// <response code="400">Bad request due to invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpGet("by-status", Name = "GetDriversByStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStatusAsync(string status)
        {
            try
            {
                if (!Enum.TryParse(typeof(DriverStatus), status, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{status}' is invalid.");
                }

                var dataResult = await _driverService.GetAllAsync(driver => driver.Status == (DriverStatus)parsedStatus);

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", "Unexpected error occurred while retrieving drivers.");
            }

            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new driver to the system.
        /// </summary>
        /// <param name="driverDto">
        /// The driver data transfer object containing the details of the driver to be added.
        /// </param>
        /// <returns>
        /// A result containing the newly created driver details.h
        /// </returns>
        /// <response code="201">Driver successfully created.</response>
        /// <response code="400">Bad request due to invalid input.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpPost (Name = "AddNewDriverAsync")]
        [ProducesResponseType(typeof(ApiResponse<Result<DriverDto>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
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
                
                var result = await _driverService.AddAsync(driverDto);


                return result.IsSuccess
                    ? CreatedResponse("GetDriverByIdAsync", new { id = result.Data?.Id }, result.Data, "Driver created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create driver.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the details of an existing driver.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the driver to be updated.
        /// </param>
        /// <param name="driverDto">
        /// The driver data transfer object containing updated details.
        /// </param>
        /// <returns>
        /// A result containing the updated driver details.
        /// </returns>
        /// <response code="200">Driver details successfully updated.</response>
        /// <response code="400">Bad request due to validation errors.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleDriver}")]
        [HttpPut("{id:int}", Name = "UpdateDriverDetailsAsync")]
        [ProducesResponseType(typeof(ApiResponse<Result<DriverDto>>), StatusCodes.Status200OK)] // Successful update
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Validation errors
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateDriverDto driverDto)
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
                driverDto.Id = id;
                var result = await _driverService.UpdateAsync(driverDto);

                return result.IsSuccess
                    ? SuccessResponse(result, "Driver details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the status of a specific driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose status is being updated.
        /// </param>
        /// <param name="newStatus">
        /// The new status to assign to the driver (e.g., "Approved", "Rejected", "Pending").
        /// </param>
        /// <returns>
        /// An API response indicating the result of the update operation.
        /// </returns>
        /// <response code="200">Status successfully updated.</response>
        /// <response code="400">Bad request due to invalid parameters or input.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}")]
        [HttpPatch("update-status/{driverId:int}", Name = "UpdateDriverStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStatusAsync(int driverId, string newStatus)
        {
            try
            {
                // Validate and parse the provided status string
                if (!Enum.TryParse(typeof(DriverStatus), newStatus, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{newStatus}' is invalid.");
                }

                var result = await _driverService.UpdateStatusAsync(driverId, (DriverStatus)parsedStatus);

                return result.IsSuccess
                    ? SuccessResponse("Driver details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the availability status of a specific driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose availability is being updated.
        /// </param>
        /// <param name="availableFrom">
        /// The optional start date from which the driver is available.
        /// If null, availability start date is not specified.
        /// </param>
        /// <param name="availableTo">
        /// The optional end date until which the driver is available.
        /// If null, availability end date is not specified.
        /// </param>
        /// <param name="availableAllTime">
        /// Indicates whether the driver is available at all times.
        /// If true, the driver is always available regardless of specific dates.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the update operation.
        /// </returns>
        /// <response code="200">Availability successfully updated.</response>
        /// <response code="400">Bad request due to invalid parameters or input.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleDriver}")]
        [HttpPatch("update-availability/{driverId:int}", Name = "UpdateDriverAvailabilityAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAvailabilityAsync(int driverId,DateTime? availableFrom,DateTime? availableTo,bool availableAllTime)
        {
            if (driverId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Driver ID must be greater than 0.");
            }

            try
            {
                var result = await _driverService.UpdateAvailabilityAsync(driverId, availableFrom, availableTo, availableAllTime);

                return result.IsSuccess
                ? SuccessResponse("Driver availability updated successfully.")
                : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the price per hour for a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique ID of the driver whose price is being updated.
        /// </param>
        /// <param name="newPricePerHour">
        /// The new price per hour to set for the driver.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the update operation.
        /// </returns>
        /// <response code="200">Price updated successfully.</response>
        /// <response code="400">Bad request due to invalid driver ID or price.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleDriver}")]
        [HttpPatch("update-price/{driverId:int}", Name = "UpdateDriverPriceAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePriceAsync(int driverId,decimal newPricePerHour)
        {
            if (driverId < 1 || newPricePerHour <= 0)
            {
                return BadRequestResponse("InvalidRequest", "Driver ID and price must be greater than 0.");
            }

            try
            {
                var result = await _driverService.UpdatePriceAsync(driverId, newPricePerHour);

                return result.IsSuccess
                    ? SuccessResponse("Driver price updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Allows a user to like or dislike a specific driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver being liked or disliked.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user performing the action.
        /// </param>
        /// <param name="isLiked">
        /// Indicates whether the user likes (true), dislikes (false), or removes their previous action (null).
        /// </param>
        /// <returns>
        /// An API response indicating the result of the operation.
        /// </returns>
        /// <response code="200">Successfully recorded the like/dislike action.</response>
        /// <response code="400">Bad request due to invalid parameters or input.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpPost("like-dislike/{driverId:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LikeOrDislikeAsync(int driverId,int userId,bool? isLiked)
        {
            if (driverId < 1 || userId < 1)
                return BadRequestResponse("InvalidRequest", "Driver ID and User ID must be greater than 0.");

            try
            {
                var result = await _driverService.LikeOrDislikeAsync(driverId, userId, isLiked);

                return result.IsSuccess
                    ? SuccessResponse("Reaction updated successfully.")
                    : BadRequestResponse("UpdateFailed", result.Errors.FirstOrDefault() ?? string.Empty);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Increments the view count for a specific driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose view count is being updated.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user viewing the driver.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the operation.
        /// </returns>
        /// <response code="200">View count successfully incremented.</response>
        /// <response code="400">Bad request due to invalid parameters or input.</response>
        /// <response code="404">Driver not found.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpPost("increment-view/{driverId:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementViewCountAsync( int driverId,[FromQuery]int userId)
        {
            // Validate input parameters
            if (driverId < 1 || userId < 1)
                return BadRequestResponse("InvalidRequest", "Driver ID and User ID must be greater than 0.");

            try
            {             
                var result = await _driverService.IncrementViewCountAsync(driverId, userId);

                return result.IsSuccess 
                    ? SuccessResponse("View count recorded successfully.") 
                    : BadRequestResponse("UpdateFailed", result?.Errors?.FirstOrDefault() ?? string.Empty);
            }
            catch (Exception ex) 
            { 
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}"); 
            }
          
        }

        /// <summary>
        /// Deletes a driver based on the specified ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the driver to be deleted.
        /// </param>
        /// <returns>
        /// An API response indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">Driver deleted successfully.</response>
        /// <response code="400">Bad request due to an invalid driver ID.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize(Roles = $"{AppUserRoles.RoleAdmin}, {AppUserRoles.RoleEmployee}, {AppUserRoles.RoleDriver}")]
        [HttpDelete("{id:int}",Name = "DeleteAsync")]
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

        /// <summary>
        /// Retrieves all necessary form fields for driver registration, including car-related fields, languages, countries, and cities.
        /// </summary>
        /// <returns>
        /// A response containing all required form fields for driver registration.
        /// </returns>
        /// <response code="200">Successfully retrieved form fields.</response>
        /// <response code="204">No content available.</response>
        /// <response code="500">Internal server error.</response>
        [AllowAnonymous]
        [HttpGet("all-driver-form-fields", Name = "GetAllDriverFormFields")]
        [ProducesResponseType(typeof(ApiResponse<DriverFormFieldsDto>), StatusCodes.Status200OK)] // Success response
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status204NoContent)] // No content
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)] // Server error
        public async Task<IActionResult> GetAllFormFields()
        {
            try
            {
                var dataResult = await _driverService.GetAllDriverFormFields();

                if (dataResult == null)
                    return StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");

                return SuccessResponse(dataResult, "All Driver's Form Fields retrieved successfully.");
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