using Mottrist.Domain.Entities;
using Mottrist.Service.Features.Destinations.DTOs;
using System.Linq.Expressions;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;

namespace Mottrist.Service.Interfaces
{
    /// <summary>
    /// Interface for destination service, defining operations for retrieving and managing destinations.
    /// </summary>
    public interface IDestinationService
    {
        /// <summary>
        /// Retrieves a list of destinations, optionally filtered by criteria.
        /// </summary>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a list of destinations.
        /// </returns>
        Task<DataResult<DestinationDto>?> GetAllAsync(Expression<Func<Destination, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of destinations.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning paginated results.
        /// </returns>
        Task<PaginatedResult<DestinationDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Destination, bool>>? filter = null);

        /// <summary>
        /// Retrieves a destination by its unique identifier.
        /// </summary>
        /// <param name="destinationId">The unique identifier of the destination.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning the destination if found.
        /// </returns>
        Task<DestinationDto?> GetByIdAsync(int destinationId);

        /// <summary>
        /// Adds a new destination.
        /// </summary>
        /// <param name="destinationDto">The DTO containing destination details.</param>
        /// <returns>
        /// A task representing the asynchronous operation, indicating success or failure.
        /// </returns>
        Task<Result<DestinationDto>> AddAsync(AddDestinationDto destinationDto);

        /// <summary>
        /// Updates an existing destination.
        /// </summary>
        /// <param name="destinationDto">The DTO containing updated destination details.</param>
        /// <returns>
        /// A task representing the asynchronous operation, indicating success or failure.
        /// </returns>
        Task<Result<DestinationDto>> UpdateAsync(UpdateDestinationDto destinationDto);

        /// <summary>
        /// Deletes a destination by its unique identifier.
        /// </summary>
        /// <param name="destinationId">The unique identifier of the destination to delete.</param>
        /// <returns>
        /// A task representing the asynchronous operation, indicating success or failure.
        /// </returns>
        Task<Result> DeleteAsync(int destinationId);
    }
}
