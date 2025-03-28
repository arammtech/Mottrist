using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.General;
using System.Linq.Expressions;


namespace Mottrist.Service.Features.Drivers.Interfaces
{
    public interface IDriverService : IBaseService
    {
        Task<ISet<DriverDto>?> GetAllAsync(Expression<Func<DriverDto, bool>>? filter = null);
        Task<DriverDto?> GetByIdAsync(int driverId);
        Task<Result> AddAsync(AddUpdateDriverDto driverDto);

        Task<Result> DeleteAsync(int driverId);
        Task<Result> UpdateAsync(AddUpdateDriverDto driverDto);
    }
}
