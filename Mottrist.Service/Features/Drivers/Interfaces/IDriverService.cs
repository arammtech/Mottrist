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

        /// <summary>
        /// Retrieves all necessary form fields required for driver registration,  
        /// including car-related fields, languages, countries, and cities.
        /// </summary>
        /// <returns>
        /// A <see cref="DriverFormFieldsDto"/> object containing car fields, languages, countries, and cities.  
        /// Returns null if an error occurs.
        /// </returns>
        Task<DriverFormFieldsDto?> GetAllDriverFormFields();
        Task<DataResult<DriverDto>?> GetByCountryAsync(int countryId);
     
        Task<DataResult<DriverDto>?> GetByCountryAndCityAsync(int countryId, int cityId);

        Task<DataResult<DriverDto>?> GetByCountryCityAndDateAsync(int countryId, int cityId, DateTime date);

        Task<PaginatedResult<DriverDto>?> GetByCountryWithPaginationAsync(int countryId,int page = 1,int pageSize = 10);
        Task<PaginatedResult<DriverDto>?> GetByCountryAndCityWithPaginationAsync(int countryId, int cityId,int page = 1,int pageSize = 10);
        Task<PaginatedResult<DriverDto>?> GetByCountryCityAndDateWithPaginationAsync(int countryId, int cityId,DateTime date,int page = 1,int pageSize = 10);
        #endregion

        #region Create Operation

        /// <summary>
        /// Adds a new driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The data transfer object containing details of the driver to be added.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the addition operation.
        /// </returns>
        Task<Result<DriverDto>> AddAsync(AddDriverDto driverDto);

        #endregion

        #region Update Operation

        /// <summary>
        /// Updates an existing driver using the provided data transfer object.
        /// </summary>
        /// <param name="driverDto">The data transfer object containing updated details of the driver.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the update operation.
        /// </returns>
        Task<Result<DriverDto>> UpdateAsync(UpdateDriverDto driverDto);

        /// <summary>
        /// Updates the availability details of a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose availability is being updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="availableFrom">
        /// The date when the driver becomes available.
        /// If null, the availability start date remains unchanged.
        /// </param>
        /// <param name="availableTo">
        /// The date when the driver is no longer available.
        /// If null, the availability end date remains unchanged.
        /// </param>
        /// <param name="availableAllTime">
        /// Indicates whether the driver is available all the time.
        /// If true, the availability dates may be ignored.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> object indicating whether the update was successful or if an error occurred.
        /// </returns>
        Task<Result> UpdateAvailabilityAsync(
            int driverId,
            DateTime? availableFrom,
            DateTime? availableTo,
            bool availableAllTime);

        /// <summary>
        /// Updates the price per hour for a specified driver.
        /// </summary>
        /// <param name="driverId">
        /// The unique identifier of the driver whose pricing is being updated.
        /// Must be greater than 0.
        /// </param>
        /// <param name="newPricePerHour">
        /// The new price per hour to set for the driver.
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the update was successful or if an error occurred.
        /// </returns>
        Task<Result> UpdatePriceAsync(int driverId, decimal newPricePerHour);
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
        Task<bool> DoesExistByIdAsync(int driverId, CancellationToken cancellationToken = default);


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
        Task<bool> DoesExistByEmailAsync(string email, CancellationToken cancellationToken = default);


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

        #region Driver Status Operations
        /// <summary>
        /// Updates the status of a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver to update.</param>
        /// <param name="newStatus">The new status to assign to the driver.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the success or failure of the operation.
        /// </returns>
        Task<Result> UpdateStatusAsync(int driverId, DriverStatus newStatus);

        /// <summary>
        /// Retrieves the current status of a driver.
        /// </summary>
        /// <param name="driverId">The ID of the driver.</param>
        /// <returns>
        /// The current <see cref="DriverStatus"/> of the driver, or null if the driver does not exist.
        /// </returns>
        Task<DriverStatus?> GetStatusAsync(int driverId);

        #endregion
        #region Driver Interaction Operations

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
        /// A <see cref="Result"/> indicating whether the update was successful.
        /// </returns>
        Task<Result> LikeOrDislikeAsync(int driverId, int userId, bool? isLiked);

        /// <summary>
        /// Records a user's first view of a driver, ensuring each user views a driver only once.
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
        /// A <see cref="Result"/> object indicating whether the view was successfully recorded 
        /// or if the user has already viewed the driver.
        /// </returns>
        Task<Result> IncrementViewCountAsync(int driverId, int userId);

        #endregion
    }
}
