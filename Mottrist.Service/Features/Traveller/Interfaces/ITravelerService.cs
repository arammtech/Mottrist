/// <summary>
/// Interface for managing Traveler-related operations.
/// </summary>
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Interfaces
{
    public interface ITravelerService
    {
        #region Get
        /// <summary>
        /// Retrieves a traveler by their unique ID.
        /// </summary>
        /// <param name="travelerId">The ID of the traveler.</param>
        /// <returns>The traveler details if found.</returns>
        Task<TravelerDto?> GetByIdAsync(int travelerId);

        /// <summary>
        /// Retrieves all travelers asynchronously based on an optional filter.
        /// </summary>
        /// <param name="filter">Optional filter for retrieving travelers.</param>
        /// <returns>Data result containing travelers.</returns>
        Task<DataResult<TravelerDto>?> GetAllAsync(Expression<Func<Traveler, bool>>? filter = null);

        /// <summary>
        /// Retrieves all travelers with pagination support.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <param name="filter">Optional filter for retrieving travelers.</param>
        /// <returns>Paginated result of travelers.</returns>
        Task<PaginatedResult<TravelerDto>?> GetAllWithPaginationAsync(
         int page,
         int pageSize = 10,
         Expression<Func<Traveler, bool>>? filter = null);
        #endregion

        #region Add
        /// <summary>
        /// Adds a new traveler asynchronously.
        /// </summary>
        /// <param name="travelerDto">Traveler details.</param>
        /// <returns>Result of the operation.</returns>
        Task<Result<TravelerDto>> AddAsync(AddTravelerDto travelerDto);

        #endregion

        #region Update
        /// <summary>
        /// Updates an existing traveler asynchronously.
        /// </summary>
        /// <param name="travelerDto">Updated traveler details.</param>
        /// <returns>Result of the update operation.</returns>
        Task<Result<TravelerDto>> UpdateAsync(UpdateTravelerDto travelerDto);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a traveler asynchronously by their ID.
        /// </summary>
        /// <param name="travelerId">The ID of the traveler to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        Task<Result> DeleteAsync(int travelerId);
        #endregion
    }
}