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
        Task<GetTravelerDto>? GetByIdAsync(int travelerId);
        GetTravelerDto? Get(Expression<Func<Traveler, bool>> filter);
        IEnumerable<GetTravelerDto>? GetAll(Expression<Func<Traveler, bool>>? filter = null);
        Task<DataResult<GetTravelerDto>>? GetAllAsync(Expression<Func<Traveler, bool>>? filter = null);
        Task<PaginatedResult<GetTravelerDto>>? GetAllWithPaginationAsync(
         int page,
         int pageSize = 10,
         Expression<Func<Traveler, bool>>? filter = null);
        #endregion

        #region Add
        Result Add(AddTravelerDto travelerDto);
        Task<Result> AddAsync(AddTravelerDto travelerDto);
        Result AddRange(IEnumerable<AddTravelerDto> travelerDtos);
        Task<Result> AddRangeAsync(IEnumerable<AddTravelerDto> travelerDtos);
        #endregion

        #region Update
        Result Update(UpdateTravelerDto travelerDto);
        Task<Result> UpdateAsync(UpdateTravelerDto travelerDto);
        #endregion

        #region Delete
        Result Delete(int travelerId);
        Task<Result> DeleteAsync(int travelerId);
        #endregion
    }
}
