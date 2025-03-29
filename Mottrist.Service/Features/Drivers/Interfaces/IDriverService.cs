using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;


namespace Mottrist.Service.Features.Drivers.Interfaces
{
    public interface IDriverService : IBaseService
    {
        Task<DataResult<DriverDto>?> GetAllAsync(Expression<Func<DriverDto, bool>>? filter = null);
        Task<DriverDto?> GetByIdAsync(int driverId);
        Task<Result> AddAsync(AddDriverDto driverDto);

        Task<Result> DeleteAsync(int driverId);
        Task<Result> UpdateAsync(UpdateDriverDto driverDto);

        Task<bool> DoesDriverExistByIdAsync(int driverId, CancellationToken cancellationToken = default);
    }
}
