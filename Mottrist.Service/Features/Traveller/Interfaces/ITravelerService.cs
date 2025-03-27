using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Traveller.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Interfaces
{
    public interface ITravelerService
    {
        #region Get
        Task<IEnumerable<GetTravelerDto>> GetAllAsync(Expression<Func<Traveler, bool>>? filter = null);
        Task<(IEnumerable<GetTravelerDto>? Travellers, int? TotalRecords)> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Traveler, bool>>? filter = null);
        Task<PaginationTravelerDto> GetAllWithPaginationWithDtoAsync(
         int page,
         int pageSize = 10,
         Expression<Func<Traveler, bool>>? filter = null);
        
        Task<GetTravelerDto> GetByIdAsync(int travelerId);
        #endregion

        Task<Result> AddAsync(AddUpdateTravelerDto travelerDto);
        Task<Result> UpdateAsync(AddUpdateTravelerDto travelerDto);
        Task<Result> DeleteAsync(int travelerId);
    }
}
