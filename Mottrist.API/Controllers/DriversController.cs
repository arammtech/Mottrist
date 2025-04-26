using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using global::Mottrist.API.Response;
using Microsoft.AspNetCore.Mvc;
using static Mottrist.API.Response.ApiResponseHelper;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;

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
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        }

        /// <summary>
        /// Retrieves a driver by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to retrieve.</param>
        /// <returns>
        /// - HTTP 200 OK with the driver details if found.
        /// - HTTP 404 Not Found if no driver exists with the given ID.
        /// - HTTP 400 Bad Request if the provided ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
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
                bool driverExits = await _driverService.DoesDriverExistByIdAsync(id);

                if(!driverExits)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                var driver = await _driverService.GetByIdAsync(id);

                return SuccessResponse(driver, "Driver retrieved successfully.");
            }
            catch (HttpRequestException httpEx)
            {
                // This exception is typically thrown for issues related to HTTP requests.
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", httpEx.Message);
            }
            catch (Exception ex)
            {
                // Catch-all for any unexpected exceptions.
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all drivers from the service.
        /// </summary>
        /// <returns>
        /// - HTTP 200 OK with the list of drivers if successful.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("all", Name = "GetAllDriversAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _driverService.GetAllAsync();

                // If no data is found, return a NoContent response.
                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No data content available.");
                }

                return dataResult != null
                    ? SuccessResponse(dataResult, "Drivers retrieved successfully.")
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
        /// Retrieves a paginated list of drivers based on the specified page and page size.
        /// </summary>
        /// <param name="page">
        /// The page number for paginated results.
        /// Must be greater than 0. Defaults to 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of records per page.
        /// Must be greater than 0. Defaults to 10.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a paginated list of drivers.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 400 Bad Request if pagination parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("all/page/{page:int}/size/{pageSize:int}", Name = "GetAllDriversWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDriversWithPaginationAsync(
            [FromRoute] int page = 1,
            [FromRoute] int pageSize = 10)
        {
            try
            {
                // Validate pagination input parameters.
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");
                }

                var result = await _driverService.GetAllWithPaginationAsync(page, pageSize);

                // If no drivers are found, return a NoContent response.
                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified parameters.");
                }

                return result != null
                     ? SuccessResponse(result, "Paginated drivers retrieved successfully.")
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
        /// Retrieves drivers filtered by country.
        /// </summary>
        /// <param name="countryId">
        /// The ID of the country to filter drivers by. This parameter is required and must be greater than 0.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with the list of drivers if successful.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 400 Bad Request if the country ID parameter is invalid.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpGet("by-country/{countryId:int}", Name = "GetDriversByCountryAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByCountryAsync([FromRoute] int countryId)
        {
            if (countryId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID parameter is required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country only.
                var dataResult = await _driverService.GetDriversByLocationAndDateAsync(countryId);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified country.");
                }

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
        /// Retrieves drivers filtered by both country and city.
        /// </summary>
        /// <param name="countryId">
        /// The unique identifier for the country to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="cityId">
        /// The unique identifier for the city to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a list of matching drivers.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 400 Bad Request if the country or city ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("by-country/{countryId:int}/city/{cityId:int}", Name = "GetDriversByCountryAndCityAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByCountryAndCityAsync([FromRoute] int countryId, [FromRoute] int cityId)
        {
            if (countryId < 1 || cityId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Both country and city ID parameters are required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country and city.
                var dataResult = await _driverService.GetDriversByLocationAndDateAsync(countryId, cityId);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified country and city.");
                }

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
        /// Retrieves drivers filtered by country, city, and availability date.
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
        /// - HTTP 200 OK with a list of matching drivers.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 400 Bad Request if the country or city ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("by-country/{countryId:int}/city/{cityId:int}/date/{date:datetime}", Name = "GetDriversByCountryCityAndDateAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByCountryCityAndDateAsync(
            [FromRoute] int countryId,
            [FromRoute] int cityId,
            [FromRoute] DateTime date)
        {
            if (countryId < 1 || cityId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Both country and city ID parameters are required and must be greater than 0.");
            }

            try
            {
                // Retrieve drivers filtering by country, city, and date.
                var dataResult = await _driverService.GetDriversByLocationAndDateAsync(countryId, cityId, date);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified country, city, and date.");
                }

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
        /// The unique identifier of the country to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="cityId">
        /// The unique identifier of the city to filter drivers.
        /// This parameter is required and must be greater than 0.
        /// </param>
        /// <param name="date">
        /// The specific date when the driver should be available.
        /// Only drivers available on this date or marked as available all the time will be included.
        /// </param>
        /// <param name="page">
        /// The page number for paginated results.
        /// Must be greater than 0. Defaults to 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of records per page.
        /// Must be greater than 0. Defaults to 10.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a paginated list of matching drivers.
        /// - HTTP 204 No Content if no drivers are found.
        /// - HTTP 400 Bad Request if input parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("by-country/{countryId:int}/city/{cityId:int}/date/{date:datetime}/page/{page:int}/size/{pageSize:int}", Name = "GetDriversByCountryCityAndDateWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<DriverDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByCountryCityAndDateWithPaginationAsync(
            [FromRoute] int countryId,
            [FromRoute] int cityId,
            [FromRoute] DateTime date,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (countryId < 1 || cityId < 1 || page < 1 || pageSize < 1)
            {
                return BadRequestResponse("InvalidRequest", "Country ID, city ID, page, and page size must be greater than 0.");
            }

            try
            {
                var dataResult = await _driverService.GetDriversByLocationAndDateWithPaginationAsync(countryId, cityId, date, page, pageSize);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified country, city, and date.");
                }

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
        /// Retrieves a paginated list of drivers based on a specified status.
        /// </summary>
        /// <param name="status">
        /// The status used to filter drivers (e.g., "Approved", "Rejected", "Pending").
        /// This parameter is required and must be a valid status.
        /// </param>
        /// <param name="page">
        /// The page number for paginated results.
        /// Must be greater than 0. Defaults to 1.
        /// </param>
        /// <param name="pageSize">
        /// The number of records per page in paginated results.
        /// Must be greater than 0. Defaults to 10.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a paginated list of drivers.
        /// - HTTP 204 No Content if no matching drivers are found.
        /// - HTTP 400 Bad Request if the pagination parameters or status are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("by-status/{status}/page/{page:int}/size/{pageSize:int}", Name = "GetDriversByStatusWithPaginationAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByStatusWithPaginationAsync(
            [FromRoute] string status,
            [FromRoute] int page = 1,
            [FromRoute] int pageSize = 10)
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

                var result = await _driverService.GetAllWithPaginationAsync(
                    page, pageSize, driver => driver.Status == (DriverStatus)parsedStatus);

                if (result?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found for the specified parameters.");
                }

                return SuccessResponse(result, "Paginated drivers retrieved successfully.");
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
        /// Retrieves a list of drivers based on the specified status.
        /// </summary>
        /// <param name="status">
        /// The status used to filter drivers (e.g., "Approved", "Rejected", "Pending").
        /// This parameter is required and must be a valid status.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK with a list of matching drivers.
        /// - HTTP 204 No Content if no drivers match the specified status.
        /// - HTTP 400 Bad Request if the status parameter is invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("by-status/{status}", Name = "GetDriversByStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<DataResult<DriverDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriversByStatusAsync([FromRoute] string status)
        {
            try
            {
                if (!Enum.TryParse(typeof(DriverStatus), status, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{status}' is invalid.");
                }

                var dataResult = await _driverService.GetAllAsync(driver => driver.Status == (DriverStatus)parsedStatus);

                if (dataResult?.DataRecordsCount?.Equals(0) ?? false)
                {
                    return NoContentResponse("No drivers found with the specified status.");
                }

                return SuccessResponse(dataResult, "Drivers retrieved successfully.");
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
        /// Adds a new driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The DTO containing the details of the driver to be added.</param>
        /// <returns>
        /// - HTTP 201 Created if the operation is successful.
        /// - HTTP 400 Bad Request if there are validation errors.
        /// - HTTP 409 Conflict if a driver with the same unique details already exists.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPost("add", Name = "AddNewDriverAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
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
                // Validate uniqueness of the email
                var existingUser = await _driverService.DoesDriverExistByEmailAsync(driverDto.Email);
                if (existingUser)
                {
                    return StatusCodeResponse(StatusCodes.Status409Conflict, "DuplicateUser", "Driver already exists.");
                }

                var result = await _driverService.AddAsync(driverDto);


                return result.IsSuccess
                    ? CreatedResponse("GetDriverByIdAsync", new { id = driverDto.Id }, new { id = driverDto.Id }, "Driver created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create driver.");
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
        /// Updates the details of an existing driver in the system using the provided data transfer object.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to be updated.</param>
        /// <param name="driverDto">The data transfer object containing updated driver details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - HTTP 200 OK if the operation is successful.
        /// - HTTP 400 Bad Request if validation fails.
        /// - HTTP 404 Not Found if the driver does not exist.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPut("{id:int}", Name = "UpdateDriverDetailsAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)] // Successful update
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Validation errors
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)] // Driver not found
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)] // Unexpected errors
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDriverDto driverDto)
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
                bool isFound = await _driverService.DoesDriverExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                // Set the driver id based on the route parameter.
                driverDto.Id = id;
                var result = await _driverService.UpdateAsync(driverDto);

                return result.IsSuccess
                    ? SuccessResponse(result, "Driver details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
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
        /// Updates the status of a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose status needs to be updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="newStatus">
        /// The new status to assign to the driver (e.g., "Approved", "Rejected", "Pending").
        /// This parameter is required and must be a valid status.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK if the status is updated successfully.
        /// - HTTP 400 Bad Request if the provided status is invalid.
        /// - HTTP 404 Not Found if no driver matches the provided ID.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-status/{driverId:int}/{newStatus}", Name = "UpdateDriverStatusAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDriverStatusAsync([FromRoute] int driverId, [FromRoute] string newStatus)
        {
            try
            {
                // Validate and parse the provided status string
                if (!Enum.TryParse(typeof(DriverStatus), newStatus, true, out var parsedStatus) || parsedStatus == null)
                {
                    return BadRequestResponse("InvalidStatus", $"The status '{newStatus}' is invalid.");
                }

                var isFound = await _driverService.DoesDriverExistByIdAsync(driverId);
                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                var result = await _driverService.UpdateDriverStatusAsync(driverId, (DriverStatus)parsedStatus);

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
        /// Updates the availability status of a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose availability needs to be updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="availableFrom">
        /// The date when the driver becomes available.
        /// If null, no specific start date is set.
        /// </param>
        /// <param name="availableTo">
        /// The date when the driver is no longer available.
        /// If null, no specific end date is set.
        /// </param>
        /// <param name="availableAllTime">
        /// Indicates whether the driver is available at all times.
        /// If true, the availability dates may be ignored.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK if the availability update is successful.
        /// - HTTP 400 Bad Request if the driver ID is invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-availability/{driverId:int}", Name = "UpdateDriverAvailabilityAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDriverAvailabilityAsync(
            [FromRoute] int driverId,
            [FromQuery] DateTime? availableFrom,
            [FromQuery] DateTime? availableTo,
            [FromQuery] bool availableAllTime)
        {
            if (driverId < 1)
            {
                return BadRequestResponse("InvalidRequest", "Driver ID must be greater than 0.");
            }

            try
            {
                var isFound = await _driverService.DoesDriverExistByIdAsync(driverId);
                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                var result = await _driverService.UpdateDriverAvailabilityAsync(driverId, availableFrom, availableTo, availableAllTime);

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
        /// <param name="driverId">The unique ID of the driver.</param>
        /// <param name="newPricePerHour">The new price per hour to set for the driver.</param>
        /// <returns>
        /// - HTTP 200 OK if the price is updated successfully.
        /// - HTTP 400 Bad Request if the driver ID or price is invalid.
        /// - HTTP 404 Not Found if the driver does not exist.
        /// - HTTP 500 Internal Server Error for unexpected errors.
        /// </returns>
        [HttpPut("update-price/{driverId:int}", Name = "UpdateDriverPriceAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDriverPriceAsync(
            [FromRoute] int driverId,
            [FromQuery] decimal newPricePerHour)
        {
            if (driverId < 1 || newPricePerHour <= 0)
            {
                return BadRequestResponse("InvalidRequest", "Driver ID and price must be greater than 0.");
            }

            try
            {
                var isFound = await _driverService.DoesDriverExistByIdAsync(driverId);

                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

                var result = await _driverService.UpdateDriverPriceAsync(driverId, newPricePerHour);

                return result.IsSuccess
                    ? SuccessResponse("Driver price updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.StatusCodeResponse(StatusCodes.Status500InternalServerError, "ServerError", $"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the like/dislike status for a driver by a logged-in user.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver.
        /// Must be greater than 0.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user making the reaction.
        /// Must be greater than 0.
        /// </param>
        /// <param name="isLiked">
        /// The reaction type: 
        /// - `true` for Like.
        /// - `false` for Dislike.
        /// - `null` to remove the reaction.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK if the reaction is updated successfully.
        /// - HTTP 400 Bad Request if input parameters are invalid.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("like-dislike/{driverId:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LikeOrDislikeDriverAsync(
            [FromRoute] int driverId,
            [FromQuery] int userId,
            [FromQuery] bool? isLiked)
        {
            if (driverId < 1 || userId < 1)
                return BadRequestResponse("InvalidRequest", "Driver ID and User ID must be greater than 0.");

            try
            {
                // Ensure the driver exists
                bool driverExists = await _driverService.DoesDriverExistByIdAsync(driverId);
                if (!driverExists)
                    return NotFoundResponse("DriverNotFound", "The specified driver does not exist.");

                // Ensure the user exists
                var userExists = await _userManager.FindByIdAsync(userId.ToString());

                if (userExists is null)
                    return NotFoundResponse("UserNotFound", "The specified user does not exist.");

                var result = await _driverService.LikeOrDislikeDriverAsync(driverId, userId, isLiked);

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
        /// Records a user's first view of a driver, ensuring each user views a driver only once.
        /// Validates that both driver and user exist before registering the view.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver being viewed.
        /// Must be greater than 0.
        /// </param>
        /// <param name="userId">
        /// The unique identifier of the user viewing the driver.
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// - HTTP 200 OK if the view is recorded successfully.
        /// - HTTP 400 Bad Request if input parameters are invalid.
        /// - HTTP 404 Not Found if the driver or user does not exist.
        /// - HTTP 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("increment-view/{driverId:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementViewCountAsync(
            [FromRoute] int driverId,
            [FromQuery] int userId)
        {
            // Validate input parameters
            if (driverId < 1 || userId < 1)
                return BadRequestResponse("InvalidRequest", "Driver ID and User ID must be greater than 0.");

            try
            {
                // Ensure the driver exists
                bool driverExists = await _driverService.DoesDriverExistByIdAsync(driverId);
                if (!driverExists)
                    return NotFoundResponse("DriverNotFound", "The specified driver does not exist.");

                // Ensure the user exists
                var userExists = await _userManager.FindByIdAsync(userId.ToString());

                if (userExists is null)
                    return NotFoundResponse("UserNotFound", "The specified user does not exist.");

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
        /// Deletes a driver by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver to be deleted.</param>
        /// <returns>
        /// Returns:
        /// - HTTP 200 OK if the deletion is successful.
        /// - HTTP 400 Bad Request if the driver ID is invalid.
        /// - HTTP 500 Internal Server Error with detailed error information for failures.
        /// </returns>
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
                bool isFound = await _driverService.DoesDriverExistByIdAsync(id);

                if (!isFound)
                {
                    return NotFoundResponse("DriverNotFound", "No driver found with the provided ID.");
                }

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
        /// An <see cref="IActionResult"/> containing:
        /// - **200 OK**: If the data is retrieved successfully.
        /// - **204 No Content**: If no data is available.
        /// - **500 Internal Server Error**: If an exception occurs.
        /// </returns>
        [HttpGet("all-driver-form-fields", Name = "GetAllDriverFormFields")]
        [ProducesResponseType(typeof(ApiResponse<DriverFormFieldsDto>), StatusCodes.Status200OK)] // Success response
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status204NoContent)] // No content
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)] // Server error
        public async Task<IActionResult> GetAllDriverFormFields()
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