using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.User.Inerfaces;
using Mottrist.Utilities.Identity;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public TravelerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,IUserService userService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<Result> AddAsync(AddUpdateTravelerDto travelerDto)
        {
            try {
                // Add user
                var transactionResult = await _unitOfWork.StartTransactionAsync();
                if (!transactionResult.IsSuccess)
                {
                    return Result.Failure("Failed to start the transaction");
                }

                ApplicationUser user = _mapper.Map<ApplicationUser>(travelerDto);
                var addUserResult = await _userManager.CreateAsync(user);

                if (!addUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    Result.Failure("Failed to save the traveler to the database.");
                }

                // Assign role to user
                var roleResult = await _userManager.AddToRoleAsync(user,AppUserRoles.RoleTraveler);
                if (!roleResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to add the role to the user");
                }

                // Add traveler
                Traveler traveler = _mapper.Map<Traveler>(travelerDto);
                traveler.UserId = user.Id;

                await _unitOfWork.Repository<Traveler>().AddAsync(traveler);

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to complete the transaction");
                }

                if (traveler.Id <= 0) return Result.Failure("Failed to save the traveler to the database.");
                travelerDto.Id = traveler.Id;

                return Result.Success();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"Error creating a traveler: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(int travelerId)
        {
            try
            {
                var traveler = await _unitOfWork.Repository<Traveler>().GetAsync(t => t.Id == travelerId);
                if (traveler == null) return Result.Failure("Traveler not found.");

                // Delete traveler
                await _unitOfWork.Repository<Traveler>().DeleteAsync(traveler);

                // Delete user
                await _userService.DeleteUserAsync(traveler.UserId);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to delete the traveler.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting traveler: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AddUpdateTravelerDto>> GetAllAsync(Expression<Func<Car, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<AddUpdateTravelerDto>? Travellers, int? TotalRecords)> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Traveler, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<AddUpdateTravelerDto> GetByIdAsync(int travelerId)
        {
            try
            {
                var traveler = await _unitOfWork.Repository<Traveler>().GetAsync(t => t.Id == travelerId);
                if (traveler == null) return null;

                var travelerDto = _mapper.Map<AddUpdateTravelerDto>(traveler);
                return travelerDto;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<Result> UpdateAsync(AddUpdateTravelerDto travelerDto)
        {
            throw new NotImplementedException();
        }
    }
}
