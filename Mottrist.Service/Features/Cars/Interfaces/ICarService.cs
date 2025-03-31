using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces
{
    /// <summary>
    /// Interface defining car service methods for managing cars, their details, and associated images.
    /// </summary>
    public interface ICarService : IBaseService
    {
        #region Get Methods

        /// <summary>
        /// Retrieves all cars, optionally filtered by the specified criteria.
        /// </summary>
        /// <param name="filter">Optional: A filter expression to apply to the car query.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result contains an <see cref="IEnumerable{AddCarDto}"/> of cars matching the filter, 
        /// or null if no cars match the filter.
        /// </returns>
        Task<DataResult<CarDto>?> GetAllAsync(Expression<Func<Car, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of cars based on the specified criteria.
        /// </summary>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of cars per page. Defaults to 10.</param>
        /// <param name="filter">Optional: A filter expression to apply to the car query.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result contains:
        /// - Cars: An <see cref="IEnumerable{AddCarDto}"/> of cars for the specified page.
        /// - TotalRecords: The total count of cars matching the criteria.
        /// </returns>
        Task<PaginatedResult<CarDto>?> GetAllWithPaginationAsync(
                    int page,
                    int pageSize = 10,
                    Expression<Func<Car, bool>>? filter = null);

        /// <summary>
        /// Retrieves a specific car by its unique identifier.
        /// </summary>
        /// <param name="carId">The ID of the car to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result contains the <see cref="AddCarDto"/> of the car, or null if not found.
        /// </returns>
        Task<CarDto?> GetByIdAsync(int carId);

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new car to the system.
        /// </summary>
        /// <param name="carDto">The DTO containing the car details to add.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> AddAsync(AddCarDto carDto);

        /// <summary>
        /// Updates an existing car in the system.
        /// </summary>
        /// <param name="carDto">The DTO containing the updated car details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> UpdateAsync(UpdateCarDto carDto);

        /// <summary>
        /// Deletes a car from the system by its unique identifier.
        /// </summary>
        /// <param name="carId">The ID of the car to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> DeleteAsync(int carId);

        #endregion

        #region Image Methods

        /// <summary>
        /// Updates a car image, either by replacing the image file or updating metadata.
        /// </summary>
        /// <param name="carId">The ID of the car associated with the image.</param>
        /// <param name="imageUrl">Optional: The URL of the existing image to update metadata.</param>
        /// <param name="newImageFile">Optional: A new image file to replace the existing image.</param>
        /// <param name="isMain">Specifies whether the image should be marked as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> UpdateImageAsync(int carId, string? imageUrl, IFormFile? newImageFile, bool isMain);
        
        /// <summary>
        /// Retrieves all images associated with a specific car.
        /// </summary>
        /// <param name="carId">The ID of the car whose images are to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result contains an <see cref="IEnumerable{CarImageDto}"/> of car images, or null if no images exist.
        /// </returns>
        Task<IEnumerable<CarImageDto>?> GetCarImagesAsync(int carId);

        /// <summary>
        /// Adds or updates a car image.
        /// </summary>
        /// <param name="carImageDto">The DTO containing the car image details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> AddCarImageAsync(CarImageDto carImageDto);

        /// <summary>
        /// Removes a specific car image by its URL and associated car identifier.
        /// </summary>
        /// <param name="imageUrl">The URL of the image to remove.</param>
        /// <param name="carId">The ID of the car associated with the image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> RemoveCarImageAsync(string imageUrl, int carId);

        /// <summary>
        /// Sets a specific car image as the main image for the car.
        /// </summary>
        /// <param name="carId">The ID of the car associated with the image.</param>
        /// <param name="imageUrl">The URL of the image to set as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> SetMainImageAsync(int carId, string imageUrl);

        #endregion
    }
}
