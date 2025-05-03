using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.General.Images.Interface;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Utilities.Identity;
using System.Linq.Expressions;
using static Mottrist.Utilities.Global.GlobalFunctions;

namespace Mottrist.Service.Features.Traveller.Services
{
    /// <summary>
    /// Service for managing travelers.
    /// </summary>
    public class TravelerService : BaseService , ITravelerService
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IImageService _imageService;
        public TravelerService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IImageService imageService) : base(unitOfWork)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<DataResult<TravelerDto>?> GetAllAsync(Expression<Func<Traveler, bool>>? filter = null)
        {
            try
            {
                 var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                   .Include(t => t.User)
                   .Include(t => t.Country) 
                   .Include(t => t.City)
                   .Include(t => t.PreferredLanguage)
                   .AsQueryable();

                if (filter != null)
                    travelerQuery = travelerQuery.Where(filter);

                var travelers = await _mapper.ProjectTo<TravelerDto>(travelerQuery).ToListAsync();


                return new DataResult<TravelerDto>()
                {
                    Data = travelers.Any() ? travelers : Enumerable.Empty<TravelerDto>()
                };
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginatedResult<TravelerDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Traveler, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
                return null;

            try
            {
                var travelerQuery = _unitOfWork.Repository<Traveler>().Table
                   .Include(t => t.User)
                   .Include(t => t.Country)
                   .Include(t => t.City)
                   .Include(t => t.PreferredLanguage)
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

                var travelersDto = _mapper.Map<IEnumerable<TravelerDto>>(paginatedCars);

                PaginatedResult<TravelerDto> paginationTravelerDto = new()
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
        public async Task<TravelerDto?> GetByIdAsync(int travelerId)
        {
            try
            {
                var traveler = await _unitOfWork.Repository<Traveler>()
                   .Include(t => t.User)
                   .Include(t => t.Country)
                   .Include(t => t.City)
                   .Include(t => t.PreferredLanguage)
                   .FirstOrDefaultAsync(t => t.Id == travelerId);

                return _mapper.Map<TravelerDto>(traveler);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Result<TravelerDto>> AddAsync(AddTravelerDto travelerDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();

            if (!transactionResult.IsSuccess)
                return Result<TravelerDto>.Failure("Failed to start the transaction");

            try
            {
                // Add user
                ApplicationUser user = _mapper.Map<ApplicationUser>(travelerDto);

                var addUserResult = await _userManager.CreateAsync(user, travelerDto.Password);

                if (!addUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to save the traveler to the database.");
                }

                // Assign role to user
                var roleResult = await _userManager.AddToRoleAsync(user, AppUserRoles.RoleTraveler);
                if (!roleResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to add the role to the user");
                }

                // Add traveler
                Traveler newTraveler = _mapper.Map<Traveler>(travelerDto);

                newTraveler.UserId = user.Id;
                
                if (travelerDto.ProfileImage != null)
                {
                    // Save the profile image
                    string? savedImageUrl = await _imageService.SaveImageAsync(travelerDto.ProfileImage, ImageCategory.Profiles);
                    if (string.IsNullOrEmpty(savedImageUrl))
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<TravelerDto>.Failure("Failed to save the profile image.");
                    }
                    newTraveler.ProfileImageUrl = savedImageUrl;
                }
                
                await _unitOfWork.Repository<Traveler>().AddAsync(newTraveler);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to Add the traveler.");
                }

                if (newTraveler.Id <= 0) return Result<TravelerDto>.Failure("Failed to save the traveler to the database.");


                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to complete the transaction.");
                }


                return Result<TravelerDto>.Success(await GetByIdAsync(newTraveler.Id));

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<TravelerDto>.Failure($"Error creating a traveler: {ex.Message}");
            }
        }

        public async Task<Result<TravelerDto>> UpdateAsync(UpdateTravelerDto travelerDto)
        {
            var transactionResult = await _unitOfWork.StartTransactionAsync();
            if (!transactionResult.IsSuccess)
                return Result<TravelerDto>.Failure("Failed to start the transaction");

            try
            {
                // Fetch the existing Traveler including User and Country
                var existingTraveler = await _unitOfWork.Repository<Traveler>()
                    .Include(t => t.User)
                    .Include(t => t.Country)
                    .Include(t => t.PreferredLanguage)
                    .Include(t => t.City)
                    .FirstOrDefaultAsync(t => t.Id == travelerDto.Id);

                if (existingTraveler == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Traveler not found.");
                }


                _mapper.Map(travelerDto, existingTraveler);

                // Update User details using the existing User instance (so SecurityStamp is preserved)
                var existingUser = await _userManager.FindByIdAsync(existingTraveler.UserId.ToString());

                if (existingUser == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("User not found.");
                }

                _mapper.Map(travelerDto, existingUser);

                // Ensure SecurityStamp is NOT null before updating
                if (string.IsNullOrEmpty(existingUser.SecurityStamp))
                {
                    existingUser.SecurityStamp = Guid.NewGuid().ToString();
                }

                if (travelerDto.ProfileImage != null)
                {
                    // Save the new profile image
                    string? savedImageUrl = await _imageService.ReplaceImageAsync(travelerDto.ProfileImage,existingTraveler.ProfileImageUrl,ImageCategory.Profiles);

                    if (string.IsNullOrEmpty(savedImageUrl))
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<TravelerDto>.Failure("Failed to save the profile image.");
                    }

                    existingTraveler.ProfileImageUrl = savedImageUrl;
                }

                var updateUserResult = await _userManager.UpdateAsync(existingUser);
                if (!updateUserResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to update the user.");
                }

                // Update Traveler in the database
                await _unitOfWork.Repository<Traveler>().UpdateAsync(existingTraveler);

                // Commit transaction
                var commitResult = await _unitOfWork.CommitAsync();
                if (!commitResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<TravelerDto>.Failure("Failed to complete the transaction");
                }

                return Result<TravelerDto>.Success(await GetByIdAsync(existingTraveler.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<TravelerDto>.Failure($"Error updating traveler: {ex.Message}");
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
    }
}
