using Mottrist.Domain.Global;
using Mottrist.Service.Features.Drivers.DTOs;


namespace Mottrist.Service.Features.Drivers.Interfaces
{
    public interface IDriverService
    {
        Task<Result> AddDriverAsync(AddUpdateDriverDto driverDto);
    }
}
