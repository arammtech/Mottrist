using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Utilities.Identity;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TravelerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<IEnumerable<GetTravelerDto>> GetAllAsync(Expression<Func<Traveler, bool>>? filter = null)
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
                if (!travelers.Any())
                    return Enumerable.Empty<GetTravelerDto>();

                var travelersDto = _mapper.Map<IEnumerable<GetTravelerDto>>(travelers);
                return travelersDto;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(IEnumerable<GetTravelerDto>? Travellers, int? TotalRecords)> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Traveler, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
                return (null, null);

            try
            {
                var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                 .Include(t => t.User)
                 .Include(t => t.Country)
                 .AsQueryable();

                if (filter != null)
                    travelerQuery = travelerQuery.Where(filter);

                var totalRecords = travelerQuery.Count();

                // Apply pagination
                var paginatedCars = await travelerQuery
                    .OrderBy(c => c.Id) 
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var travelersDto = _mapper.Map<IEnumerable<GetTravelerDto>>(paginatedCars);

                return (travelersDto, totalRecords);
            }
            catch (Exception ex)
            {
                return (null, null);
            }
        }
        public async Task<PaginationTravelerDto?> GetAllWithPaginationWithDtoAsync(int page, int pageSize = 10, Expression<Func<Traveler, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
                return null;

            try
            {
                var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                           .Include(t => t.User)
                           .Include(t => t.Country)
                           .AsQueryable();

                if (filter != null)
                    travelerQuery = travelerQuery.Where(filter);

                var totalRecords = travelerQuery.Count();

                // Apply pagination
                var paginatedCars = await travelerQuery
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var travelersDto = _mapper.Map<IEnumerable<GetTravelerDto>>(paginatedCars);

                PaginationTravelerDto paginationTravelerDto = new()
                {
                    Travelers = travelersDto.ToList(),
                    TotalRecords = totalRecords
                };

                return paginationTravelerDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<GetTravelerDto> GetByIdAsync(int travelerId)
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
        public async Task<Result> AddAsync(AddUpdateTravelerDto travelerDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();

            if (!transactionResult.IsSuccess)
                return Result.Failure("Failed to start the transaction");

            try
            {
                // Add user
                ApplicationUser user = _mapper.Map<ApplicationUser>(travelerDto);
                var addUserResult = await _userManager.CreateAsync(user);

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
                Traveler newTraveler = _mapper.Map<Traveler>(travelerDto);
                newTraveler.UserId = user.Id;

                await _unitOfWork.Repository<Traveler>().AddAsync(newTraveler);

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to complete the transaction");
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
        public async Task<Result> UpdateAsync(AddUpdateTravelerDto travelerDto)
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


                // Update Traveler details WITHOUT replacing the object
                _mapper.Map(travelerDto, existingTraveler);

                // Update User details using the existing User instance (so SecurityStamp is preserved)
                var existingUser = existingTraveler.User;

                //_mapper.Map(travelerDto, existingUser);
                existingUser.FirstName = travelerDto.FirstName;
                existingUser.LastName = travelerDto.LastName;
                existingUser.Email = travelerDto.Email;
                existingUser.UserName = travelerDto.UserName;
                existingUser.PhoneNumber = travelerDto.PhoneNumber;
                existingUser.PasswordHash = travelerDto.PasswordHash;



              

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

    }
}
