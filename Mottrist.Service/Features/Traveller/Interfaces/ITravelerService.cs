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
        Task<GetTravelerDto>? GetByIdAsync(int travelerId);

        /// <summary>
        /// Retrieves a traveler based on a specified filter.
        /// </summary>
        /// <param name="filter">Expression filter to find the traveler.</param>
        /// <returns>The matched traveler details.</returns>
        GetTravelerDto? Get(Expression<Func<Traveler, bool>> filter);

        /// <summary>
        /// Retrieves all travelers based on an optional filter.
        /// </summary>
        /// <param name="filter">Optional filter for retrieving travelers.</param>
        /// <returns>List of travelers.</returns>
        IEnumerable<GetTravelerDto>? GetAll(Expression<Func<Traveler, bool>>? filter = null);

        /// <summary>
        /// Retrieves all travelers asynchronously based on an optional filter.
        /// </summary>
        /// <param name="filter">Optional filter for retrieving travelers.</param>
        /// <returns>Data result containing travelers.</returns>
        Task<DataResult<GetTravelerDto>>? GetAllAsync(Expression<Func<Traveler, bool>>? filter = null);

        /// <summary>
        /// Retrieves all travelers with pagination support.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <param name="filter">Optional filter for retrieving travelers.</param>
        /// <returns>Paginated result of travelers.</returns>
        Task<PaginatedResult<GetTravelerDto>>? GetAllWithPaginationAsync(
         int page,
         int pageSize = 10,
         Expression<Func<Traveler, bool>>? filter = null);
        #endregion

        #region Add
        /// <summary>
        /// Adds a new traveler.
        /// </summary>
        /// <param name="travelerDto">Traveler details.</param>
        /// <returns>Result of the operation.</returns>
        Result Add(AddTravelerDto travelerDto);

        /// <summary>
        /// Adds a new traveler asynchronously.
        /// </summary>
        /// <param name="travelerDto">Traveler details.</param>
        /// <returns>Result of the operation.</returns>
        Task<Result> AddAsync(AddTravelerDto travelerDto);

        /// <summary>
        /// Adds multiple travelers.
        /// </summary>
        /// <param name="travelerDtos">List of travelers to add.</param>
        /// <returns>Result of the operation.</returns>
        Result AddRange(IEnumerable<AddTravelerDto> travelerDtos);

        /// <summary>
        /// Adds multiple travelers asynchronously.
        /// </summary>
        /// <param name="travelerDtos">List of travelers to add.</param>
        /// <returns>Result of the operation.</returns>
        Task<Result> AddRangeAsync(IEnumerable<AddTravelerDto> travelerDtos);
        #endregion

        #region Update
        /// <summary>
        /// Updates an existing traveler.
        /// </summary>
        /// <param name="travelerDto">Updated traveler details.</param>
        /// <returns>Result of the update operation.</returns>
        Result Update(UpdateTravelerDto travelerDto);

        /// <summary>
        /// Updates an existing traveler asynchronously.
        /// </summary>
        /// <param name="travelerDto">Updated traveler details.</param>
        /// <returns>Result of the update operation.</returns>
        Task<Result> UpdateAsync(UpdateTravelerDto travelerDto);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a traveler by their ID.
        /// </summary>
        /// <param name="travelerId">The ID of the traveler to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        Result Delete(int travelerId);

        /// <summary>
        /// Deletes a traveler asynchronously by their ID.
        /// </summary>
        /// <param name="travelerId">The ID of the traveler to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        Task<Result> DeleteAsync(int travelerId);
        #endregion
    }
}