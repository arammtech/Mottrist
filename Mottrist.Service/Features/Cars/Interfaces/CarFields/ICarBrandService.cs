using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.DTOs;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car brands.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarBrandService : IBaseService
    {
        #region Get Methods
        /// <summary>
        /// Retrieves a list of all car brands asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car brands</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarBrandDto objects</returns>
        Task<DataResult<CarBrandDto>> GetAllAsync(Expression<Func<Brand, bool>>? filter = null);


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
        Task<PaginatedResult<CarBrandDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Brand, bool>>? filter = null);

        /// <summary>
        /// Retrieves a specific car by its unique identifier.
        /// </summary>
        /// <param name="carId">The ID of the car to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result contains the <see cref="AddCarDto"/> of the car, or null if not found.
        /// </returns>
        Task<CarBrandDto?> GetByIdAsync(int brandId);
        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new car to the system.
        /// </summary>
        /// <param name="carDto">The DTO containing the car details to add.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result<CarBrandDto>> AddAsync(AddCarBrandDto addCarBrandDto);

        /// <summary>
        /// Updates an existing car in the system.
        /// </summary>
        /// <param name="carDto">The DTO containing the updated car details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result<CarBrandDto>> UpdateAsync(UpdateCarBrandDto updateCarBrandDto);

        /// <summary>
        /// Deletes a car from the system by its unique identifier.
        /// </summary>
        /// <param name="carId">The ID of the car to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> DeleteAsync(int brandId);

        #endregion
    }
}
