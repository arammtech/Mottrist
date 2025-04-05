using Microsoft.AspNetCore.Http;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;
using System.Threading;

namespace Mottrist.Service.Features.Drivers.Interfaces
{
    /// <summary>
    /// Interface for Driver service containing CRUD operations, utilities, and image management for drivers' cars.
    /// </summary>
    public interface IDriverService : IBaseService
    {
        #region Read Operations

        /// <summary>
        /// Retrieves all drivers based on the provided filter expression.
        /// </summary>
        /// <param name="filter">An optional expression to filter drivers.</param>
        /// <returns>
        /// A <see cref="DataResult{DriverDto}"/> object containing a list of drivers or null if no records are found.
        /// </returns>
        Task<DataResult<DriverDto>?> GetAllAsync(Expression<Func<Driver, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of drivers based on the provided parameters.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of records per page. Defaults to 10.</param>
        /// <param name="filter">An optional filter expression to filter drivers.</param>
        /// <returns>
        /// A <see cref="PaginatedResult{DriverDto}"/> object containing a paginated list of drivers or null if no records are found.
        /// </returns>
        Task<PaginatedResult<DriverDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Driver, bool>>? filter = null);

        /// <summary>
        /// Retrieves a driver by its unique identifier.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver.</param>
        /// <returns>
        /// A <see cref="DriverDto"/> object containing driver details, or null if the driver does not exist.
        /// </returns>
        Task<DriverDto?> GetByIdAsync(int driverId);

        /// <summary>
        /// Retrieves a single driver based on the provided filter expression.
        /// </summary>
        /// <param name="filter">An expression used to filter the driver.</param>
        /// <returns>
        /// A <see cref="DriverDto"/> object containing driver details if a match is found; otherwise, null.
        /// </returns>
        Task<DriverDto?> GetAsync(Expression<Func<Driver, bool>> filter);

        #endregion

        #region Create Operation

        /// <summary>
        /// Adds a new driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The data transfer object containing details of the driver to be added.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the addition operation.
        /// </returns>
        Task<Result> AddAsync(AddDriverDto driverDto);

        #endregion

        #region Update Operation

        /// <summary>
        /// Updates an existing driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The data transfer object containing updated details of the driver.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the update operation.
        /// </returns>
        Task<Result> UpdateAsync(UpdateDriverDto driverDto);

        #endregion

        #region Delete Operation

        /// <summary>
        /// Deletes a driver by its unique identifier.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver to be deleted.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the deletion operation.
        /// </returns>
        Task<Result> DeleteAsync(int driverId);

        #endregion

        #region Validation

        /// <summary>
        /// Checks if a driver exists by its unique identifier.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if necessary.</param>
        /// <returns>
        /// A boolean value indicating whether the driver exists.
        /// </returns>
        Task<bool> DoesDriverExistByIdAsync(int driverId, CancellationToken cancellationToken = default);


        /// <summary>
        /// Asynchronously checks whether a user with the specified email exists,
        /// ensuring email uniqueness across all user types managed by the identity system.
        /// </summary>
        /// <param name="email">The email address to search for in the user table.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is <c>true</c> if a user
        /// with the specified email exists; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> DoesDriverExistByEmailAsync(string email, CancellationToken cancellationToken = default);


        #endregion

        #region Car Image Operations

        /// <summary>
        /// Deletes a specific car image associated with a driver's car.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be deleted.</param>
        /// <param name="imageUrl">The URL of the image to be deleted.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> DeleteCarImageAsync(int driverId, string imageUrl);

        /// <summary>
        /// Sets a specific car image as the main image for a driver's car.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be set as the main image.</param>
        /// <param name="imageUrl">The URL of the image to set as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> SetMainCarImageAsync(int driverId, string imageUrl);

        /// <summary>
        /// Updates a car image, either by replacing the image file or updating metadata.
        /// </summary>
        /// <param name="driverId">The ID of the driver whose car's image is to be updated.</param>
        /// <param name="imageUrl">Optional: The URL of the existing image to update metadata.</param>
        /// <param name="newImageFile">Optional: A new image file to replace the existing image.</param>
        /// <param name="isMain">Specifies whether the image should be marked as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> UpdateCarImageAsync(int driverId, string? imageUrl, IFormFile? newImageFile, bool isMain);

        #endregion

        #region Driver Cities Operations

        /// <summary>
        /// Adds a list of cities to a driver with the specified work status.
        /// </summary>
        /// <param name="dto">The DTO containing driver ID, city IDs, and work status.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        Task<Result> AddDriverCitiesAsync(AddDriverCitiesDto dto);

        /// <summary>
        /// Updates the work status for a specific driver-city association.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="cityId">The ID of the city.</param>
        /// <param name="newStatus">The new work status to set.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        Task<Result> UpdateDriverCityWorkStatusAsync(int driverId, int cityId, WorkStatus newStatus);

        /// <summary>
        /// Removes a city association from a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <param name="cityId">The ID of the city.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        Task<Result> RemoveDriverCityAsync(int driverId, int cityId);

        /// <summary>
        /// Retrieves all cities linked to a specific driver in the system.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver whose cities are to be retrieved.</param>
        /// <returns>
        /// A <see cref="GetDriverCitiesDto"/> object containing the list of associated cities, or null if no cities are found.
        /// </returns>
        Task<GetDriverCitiesDto?> GetDriverCitiesAsync(int driverId);

        #endregion

    }
}
