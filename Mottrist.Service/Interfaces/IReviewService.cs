using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;

namespace Mottrist.Service.Interfaces
{
    public interface IReviewService : IBaseService
    {
        Task<Result> AddAsync(Review departmentDto);
        Task<Result> UpdateAsync(Review departmentDto);
        Task<Result> DeleteAsync(int id);
    }

}
