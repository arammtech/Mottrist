using Mottrist.Domain.Global;
using Mottrist.Service.Features.Drivers.DTOs;


namespace Mottrist.Service.Features.Drivers.Interfaces
{
    public interface IDriverService
    {
        Task<HashSet<DriverDto>> GetAllAsync();
        Task<Result> AddAsync(AddUpdateDriverDto driverDto);
        Task<DriverDto?> GetByIdAsync(int driverId);

        Task<Result> DeleteAsync(int driverId);
        Task<Result> UpdateAsync(AddUpdateDriverDto driverDto);
    }
}
