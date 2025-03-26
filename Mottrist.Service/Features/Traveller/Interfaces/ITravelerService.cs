using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Traveller.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Traveller.Interfaces
{
    public interface ITravelerService
    {
        #region Get
        Task<IEnumerable<AddUpdateTravelerDto>> GetAllAsync(Expression<Func<Car, bool>>? filter = null);
        Task<(IEnumerable<AddUpdateTravelerDto>? Travellers, int? TotalRecords)> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Traveler, bool>>? filter = null);
        Task<AddUpdateTravelerDto> GetByIdAsync(int travelerId);
        #endregion
        Task<Result> AddAsync(AddUpdateTravelerDto travelerDto);
        Task<Result> UpdateAsync(AddUpdateTravelerDto travelerDto);
        Task<Result> DeleteAsync(int travelerId);
    }
}
