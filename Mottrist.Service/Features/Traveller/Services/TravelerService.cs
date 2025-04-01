using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.Traveller.Mappers;
using Mottrist.Utilities.Identity;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Services
{
    /// <summary>
    /// Service for managing travelers.
    /// </summary>
    public class TravelerService : BaseService , ITravelerService
    {
        /// <summary>
        /// Unit of work for managing database transactions and repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Mapper for handling object-to-object mapping.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// User manager for handling user-related operations, such as creating and updating users.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TravelerService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="userManager">The UserManager for ApplicationUser.</param>
        public TravelerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Retrieves all travelers with an optional filter.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>
        /// A <see cref="DataResult{GetTravelerDto}"/> containing a collection of travelers if successful; otherwise, null.
        /// </returns>
        public async Task<DataResult<GetTravelerDto>>? GetAllAsync(Expression<Func<Traveler, bool>>? filter = null)
        {
            try
            {
                 var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                   .Include(t => t.User)
                   .Include(t => t.Country) 
                   .AsQueryable();

                if (filter != null)
                    travelerQuery = travelerQuery.Where(filter);

                var travelers = await travelerQuery.ToListAsync();

                DataResult<GetTravelerDto> travelersResult = new()
                {
                    Data = _mapper.Map<IEnumerable<GetTravelerDto>>(travelers)
                };

                if (!travelers.Any())
                    travelersResult.Data = Enumerable.Empty<GetTravelerDto>();

                return travelersResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves travelers with pagination and an optional filter.
        /// </summary>
        /// <param name="page">The page number (must be greater than 0).</param>
        /// <param name="pageSize">The number of travelers per page (default is 10).</param>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>
        /// A <see cref="PaginatedResult{GetTravelerDto}"/> containing the paginated travelers if successful; otherwise, null.
        /// </returns>
        public async Task<PaginatedResult<GetTravelerDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Traveler, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
                return null;

            try
            {
                var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                           .Include(t => t.User)
                           .Include(t => t.Country)
                           .AsQueryable();

                var totalRecordsCount = travelerQuery.Count();

                if (filter != null)
                    travelerQuery = travelerQuery.Where(filter);

                var dataRecordsCount= travelerQuery.Count();

                // Apply pagination
                var paginatedCars = await travelerQuery
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var travelersDto = _mapper.Map<IEnumerable<GetTravelerDto>>(paginatedCars);

                PaginatedResult<GetTravelerDto> paginationTravelerDto = new()
                {
                    Data = travelersDto,
                    PageNumber= page,
                    PageSize = pageSize,
                    DataRecordsCount= dataRecordsCount,
                    TotalRecordsCount = totalRecordsCount
                };

                return paginationTravelerDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a traveler by their unique identifier.
        /// </summary>
        /// <param name="travelerId">The unique identifier of the traveler.</param>
        /// <returns>
        /// A <see cref="GetTravelerDto"/> containing the traveler data if found; otherwise, null.
        /// </returns>
        public async Task<GetTravelerDto>? GetByIdAsync(int travelerId)
        {
            try
            {
                var traveler = await _unitOfWork.Repository<Traveler>()
                    .Include(t => t.User).Include(t => t.Country).FirstOrDefaultAsync(t => t.Id == travelerId);

                if (traveler == null) return null;

                var travelerDto = _mapper.Map<GetTravelerDto>(traveler);

                //var existingUser = traveler.User;
                //_mapper.Map(travelerDto, existingUser);

                return travelerDto;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a new traveler.
        /// </summary>
        /// <param name="travelerDto">The traveler data transfer object.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the creation operation.
        /// </returns>
        public async Task<Result> AddAsync(AddTravelerDto travelerDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();

            if (!transactionResult.IsSuccess)
                return Result.Failure("Failed to start the transaction");

            try
            {
                // Add user
                //ApplicationUser user = _mapper.Map<ApplicationUser>(travelerDto);
                ApplicationUser user = new();

                TravelerMapper.Map(travelerDto, user);

                var addUserResult = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!addUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    Result.Failure("Failed to save the traveler to the database.");
                }

                // Assign role to user
                var roleResult = await _userManager.AddToRoleAsync(user, AppUserRoles.RoleTraveler);
                if (!roleResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to add the role to the user");
                }

                // Add traveler
                //Traveler newTraveler = _mapper.Map<Traveler>(travelerDto);
                Traveler newTraveler = new();
                TravelerMapper.Map(travelerDto, newTraveler);
                newTraveler.UserId = user.Id;

                await _unitOfWork.Repository<Traveler>().AddAsync(newTraveler);

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to complete the transaction.");
                    //return Result.Failure($"Failed to complete the transaction,\n Errors: {string.Join("\n", commitResult.Errors?.ToList())}");
                }

                if (newTraveler.Id <= 0) return Result.Failure("Failed to save the traveler to the database.");
                travelerDto.Id = newTraveler.Id;

                return Result.Success();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error creating a traveler: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing traveler.
        /// </summary>
        /// <param name="travelerDto">The traveler data transfer object with updated information.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the update operation.
        /// </returns>
        public async Task<Result> UpdateAsync(UpdateTravelerDto travelerDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();
            if (!transactionResult.IsSuccess)
                return Result.Failure("Failed to start the transaction");

            try
            {
                // Fetch the existing Traveler including User and Country
                var existingTraveler = await _unitOfWork.Repository<Traveler>()
                    .Include(t => t.User)
                    .Include(t => t.Country)
                    .FirstOrDefaultAsync(t => t.Id == travelerDto.Id);

                if (existingTraveler == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Traveler not found.");
                }

                //// Update Traveler details
                TravelerMapper.Map(travelerDto, existingTraveler);

                // Update User details using the existing User instance (so SecurityStamp is preserved)
                var existingUser = existingTraveler.User;
                TravelerMapper.Map(travelerDto, existingUser);

                // Ensure SecurityStamp is NOT null before updating
                if (string.IsNullOrEmpty(existingUser.SecurityStamp))
                {
                    existingUser.SecurityStamp = Guid.NewGuid().ToString();
                }

                var updateUserResult = await _userManager.UpdateAsync(existingUser);
                if (!updateUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update the user.");
                }

                // Update Traveler in the database
                await _unitOfWork.Repository<Traveler>().UpdateAsync(existingTraveler);

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to complete the transaction");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error updating traveler: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a traveler by their unique identifier.
        /// </summary>
        /// <param name="travelerId">The unique identifier of the traveler to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the deletion operation.
        /// </returns>
        public async Task<Result> DeleteAsync(int travelerId)
        {
            try
            {
                var transactionResult = await _unitOfWork.StartTransactionAsync();

                if (!transactionResult.IsSuccess)
                    return Result.Failure("Failed to start the transaction");

                var existingTraveler = await _unitOfWork.Repository<Traveler>()
                   .Include(t => t.User).Include(t => t.Country).FirstOrDefaultAsync(t => t.Id == travelerId);

                if (existingTraveler == null) return Result.Failure("Traveler not found.");

                var user = existingTraveler.User;

                // Delete traveler
                await _unitOfWork.Repository<Traveler>().DeleteAsync(existingTraveler);
            
                // Delete user
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete the traveler.");
                }

                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to delete the traveler.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting traveler: {ex.Message}");
            }
        }

        /// <summary>
        /// Not implemented: Retrieves a traveler based on a given filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>A <see cref="GetTravelerDto"/> matching the filter.</returns>
        GetTravelerDto? ITravelerService.Get(Expression<Func<Traveler, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Retrieves all travelers based on a given filter.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>A collection of <see cref="GetTravelerDto"/>.</returns>
        IEnumerable<GetTravelerDto>? ITravelerService.GetAll(Expression<Func<Traveler, bool>>? filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Adds a traveler.
        /// </summary>
        /// <param name="travelerDto">The traveler data transfer object.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Result ITravelerService.Add(AddTravelerDto travelerDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Adds a range of travelers.
        /// </summary>
        /// <param name="travelerDtos">A collection of traveler data transfer objects.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Result ITravelerService.AddRange(IEnumerable<AddTravelerDto> travelerDtos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Asynchronously adds a range of travelers.
        /// </summary>
        /// <param name="travelerDtos">A collection of traveler data transfer objects.</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="Result"/>.</returns>
        Task<Result> ITravelerService.AddRangeAsync(IEnumerable<AddTravelerDto> travelerDtos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Updates a traveler.
        /// </summary>
        /// <param name="travelerDto">The traveler data transfer object.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Result ITravelerService.Update(UpdateTravelerDto travelerDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented: Deletes a traveler.
        /// </summary>
        /// <param name="travelerId">The unique identifier of the traveler to delete.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Result ITravelerService.Delete(int travelerId)
        {
            throw new NotImplementedException();
        }
    }
}
