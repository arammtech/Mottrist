using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining service methods for managing car brands.
    /// </summary>
    public interface ICarBrandService : IBaseService
    {
        #region Get Methods

        /// <summary>
        /// Retrieves all car brands, optionally filtered by the specified criteria.
        /// </summary>
        /// <param name="filter">Optional: A filter expression to apply to the brand query.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains a <see cref="DataResult{CarBrandDto}"/> with the matching brands.
        /// </returns>
        Task<DataResult<CarBrandDto>> GetAllAsync(Expression<Func<Brand, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of car brands based on the specified criteria.
        /// </summary>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of brands per page. Defaults to 10.</param>
        /// <param name="filter">Optional: A filter expression to apply to the brand query.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains a <see cref="PaginatedResult{CarBrandDto}"/> with paged data.
        /// </returns>
        Task<PaginatedResult<CarBrandDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Brand, bool>>? filter = null);

        /// <summary>
        /// Retrieves a specific car brand by its unique identifier.
        /// </summary>
        /// <param name="brandId">The ID of the brand to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains the <see cref="CarBrandDto"/> of the brand, or null if not found.
        /// </returns>
        Task<CarBrandDto?> GetByIdAsync(int brandId);

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new car brand to the system.
        /// </summary>
        /// <param name="addCarBrandDto">The DTO containing the brand details to add.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation, along with the added brand.
        /// </returns>
        Task<Result<CarBrandDto>> AddAsync(AddCarBrandDto addCarBrandDto);

        /// <summary>
        /// Updates an existing car brand in the system.
        /// </summary>
        /// <param name="updateCarBrandDto">The DTO containing the updated brand details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation, along with the updated brand.
        /// </returns>
        Task<Result<CarBrandDto>> UpdateAsync(UpdateCarBrandDto updateCarBrandDto);

        /// <summary>
        /// Deletes a car brand from the system by its unique identifier.
        /// </summary>
        /// <param name="brandId">The ID of the brand to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        Task<Result> DeleteAsync(int brandId);

        #endregion
    }
}
