using Feature.Car.DTOs;
using Microsoft.AspNetCore.Http;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.DTOs;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces
{
    /// <summary>
    /// Interface for managing cars, their details, and associated images.
    /// </summary>
    public interface ICarService : IBaseService
    {
        #region Get Methods

        /// <summary>
        /// Retrieves all cars optionally filtered by a criteria.
        /// </summary>
        Task<DataResult<CarDto>?> GetAllAsync(Expression<Func<Car, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of cars.
        /// </summary>
        Task<PaginatedResult<CarDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Car, bool>>? filter = null);

        /// <summary>
        /// Retrieves a car by its unique ID.
        /// </summary>
        Task<CarDto?> GetByIdAsync(int carId);

        /// <summary>
        /// Retrieves all car fields (dropdowns, etc.).
        /// </summary>
        Task<CarFieldsDto> GetAllCarFieldsAsync();

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new car to the system.
        /// </summary>
        Task<Result<CarDto>> AddAsync(AddCarDto carDto);

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        Task<Result<CarDto>> UpdateAsync(UpdateCarDto carDto, int carId);

        /// <summary>
        /// Deletes a car by ID.
        /// </summary>
        Task<Result> DeleteAsync(int carId);

        #endregion

        #region Image Methods

        /// <summary>
        /// Updates a car image (file or metadata).
        /// </summary>
        Task<Result> UpdateImageAsync(int carId, string? imageUrl, IFormFile? newImageFile, bool isMain);

        /// <summary>
        /// Retrieves all images of a specific car.
        /// </summary>
        Task<IEnumerable<CarImageDto>?> GetCarImagesAsync(int carId);

        /// <summary>
        /// Adds or updates a car image.
        /// </summary>
        Task<Result> AddCarImageAsync(CarImageDto carImageDto);

        /// <summary>
        /// Removes a car image by URL and car ID.
        /// </summary>
        Task<Result> RemoveCarImageAsync(string imageUrl, int carId);

        /// <summary>
        /// Sets a car image as the main image.
        /// </summary>
        Task<Result> SetMainImageAsync(int carId, string imageUrl);

        #endregion
    }
}
