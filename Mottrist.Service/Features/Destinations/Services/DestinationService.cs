using Mottrist.Domain.Entities;
using Mottrist.Service.Interfaces;
using System.Linq.Expressions;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.Destinations.DTOs;
using Microsoft.EntityFrameworkCore;
using static Mottrist.Utilities.Global.GlobalFunctions;

namespace Mottrist.Service.Features.DestinationServices
{
    /// <summary>
    /// Service for managing destination-related operations, including retrieval and pagination.
    /// </summary>
    public class DestinationService : BaseService, IDestinationService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DestinationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for managing repositories and transactions.</param>
        /// <param name="mapper">Automapper for mapping entities to DTOs.</param>
        public DestinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Retrieves a list of destinations, optionally filtered by criteria.
        /// </summary>
        /// <param name="filter">Optional filter to apply to the destination query.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains:
        /// - A <see cref="DataResult{DestinationDto}"/> of destinations matching the filter.
        /// - An empty result if no destinations match or an error occurs.
        /// </returns>
        public async Task<DataResult<DestinationDTO>?> GetAllAsync(Expression<Func<Destination, bool>>? filter = null)
        {
            try
            {
                // Build query from repository
                var destinationsQuery = _unitOfWork.Repository<Destination>().Query();

                // Apply filter if provided
                if (filter != null)
                {
                    destinationsQuery = destinationsQuery.Where(filter);
                }

                // Execute query and transform results
                var destinations = await destinationsQuery
                    .Select(dest => new DestinationDTO
                    {
                        Id = dest.Id,
                        Name = dest.Name,
                        CityName = dest.City.Name,
                        Type = dest.Type,
                        ImageUrl = dest.ImageUrl,
                        Description = dest.Description
                    })
                    .ToListAsync();

                return new DataResult<DestinationDTO>
                {
                    Data = destinations.Any() ? destinations : Enumerable.Empty<DestinationDTO>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of destinations.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of destinations per page.</param>
        /// <param name="filter">Optional filter criteria.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains:
        /// - A <see cref="PaginatedResult{DestinationDTO}"/> of destinations for the specified page.
        /// - The total count of matching records.
        /// </returns>
        public async Task<PaginatedResult<DestinationDTO>?> GetAllWithPaginationAsync(
            int page = 1,
            int pageSize = 10,
            Expression<Func<Destination, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be greater than 0.");
            }

            try
            {
                var destinationsQuery = _unitOfWork.Repository<Destination>().Query();

                // Apply filters if provided
                if (filter != null)
                {
                    destinationsQuery = destinationsQuery.Where(filter);
                }

                // Get total count before pagination
                var totalRecords = await destinationsQuery.CountAsync();

                // Apply pagination
                var paginatedDestinations = await destinationsQuery
                    .OrderBy(dest => dest.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(dest => new DestinationDTO
                    {
                        Id = dest.Id,
                        Name = dest.Name,
                        CityName = dest.City.Name,
                        Type = dest.Type,
                        ImageUrl = dest.ImageUrl,
                        Description = dest.Description
                    })
                    .ToListAsync();

                return new PaginatedResult<DestinationDTO>
                {
                    Data = paginatedDestinations,
                    TotalRecordsCount = totalRecords,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = paginatedDestinations.Count
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a destination by its unique identifier.
        /// </summary>
        /// <param name="destinationId">The unique identifier of the destination.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains:
        /// - A <see cref="DestinationDto"/> object if found.
        /// - Null if not found or an error occurs.
        /// </returns>
        public async Task<DestinationDTO?> GetByIdAsync(int destinationId)
        {
            try
            {
                var dest = await _unitOfWork.Repository<Destination>().GetAsync(d => d.Id == destinationId);
                if (dest == null) return null;

                return new DestinationDTO
                {
                    Id = dest.Id,
                    Name = dest.Name,
                    CityName = dest.City.Name,
                    Type = dest.Type,
                    ImageUrl = dest.ImageUrl,
                    Description = dest.Description
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new destination to the database.
        /// </summary>
        /// <param name="destinationDto">The DTO containing destination details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure.
        /// </returns>
        public async Task<Result> AddAsync(AddDestinationDTO destinationDto)
        {
            if (destinationDto == null)
                return Result.Failure("Invalid destination object.");

            try
            {
                if(destinationDto.Image != null)
                {
                    var savedImageUrl = await SaveImageAsync(destinationDto.Image, "destinations");
                    destinationDto.ImageUrl = savedImageUrl;
                }

                var destination = new Destination
                {
                    Name = destinationDto.Name,
                    CityId = destinationDto.CityId,
                    Type = destinationDto.Type,
                    ImageUrl = destinationDto.ImageUrl,
                    Description = destinationDto.Description
                };
                destination.Id = 0;
                await _unitOfWork.Repository<Destination>().AddAsync(destination);

                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (destination.Id <= 0 || !saveResult.IsSuccess)
                    return Result.Failure("Failed to save destination.");

                destinationDto.Id = destination.Id;
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error creating destination: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a destination entity with new data.
        /// </summary>
        /// <param name="destinationDto">DTO containing updated destination details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure.
        /// </returns>
        public async Task<Result> UpdateAsync(AddDestinationDTO destinationDto)
        {
            if (destinationDto == null)
                return Result.Failure("Invalid destination object.");

            try
            {
                var destination = await _unitOfWork.Repository<Destination>().GetAsync(d => d.Id == destinationDto.Id);
                if (destination == null) return Result.Failure("Destination not found.");

                destination.Name = destinationDto.Name;
                destination.CityId = destinationDto.CityId;
                destination.Type = destinationDto.Type;
                destination.ImageUrl = destinationDto.ImageUrl;
                destination.Description = destinationDto.Description;

                await _unitOfWork.Repository<Destination>().UpdateAsync(destination);
               var result = await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error updating destination: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a destination by its unique identifier.
        /// </summary>
        /// <param name="destinationId">The unique identifier of the destination to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure.
        /// </returns>
        public async Task<Result> DeleteAsync(int destinationId)
        {
            // Validate input parameter
            if (destinationId < 1)
                return Result.Failure("Invalid destination ID.");

            try
            {
                var destination = await _unitOfWork.Repository<Destination>().GetAsync(d => d.Id == destinationId);

                if (destination == null)
                    return Result.Failure("Destination not found.");

                await _unitOfWork.Repository<Destination>().DeleteAsync(destination);
                var result = await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error while deleting destination: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks whether a destination exists in the database by its unique identifier.
        /// </summary>
        /// <param name="destinationId">The unique identifier of the destination.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// - Returns `true` if the destination exists.
        /// - Returns `false` if the destination does not exist.
        /// </returns>
        public async Task<bool> DoesDestinationExistByIdAsync(int destinationId)
        {
            if (destinationId < 1)
                return false; // Invalid ID

            try
            {
                return await _unitOfWork.Repository<Destination>()
                    .Table.AnyAsync(d => d.Id == destinationId);
            }
            catch (Exception)
            {
                return false; // Default to false if an error occurs
            }
        }

    }
}
