using Mottrist.Domain.Global;

namespace Mottrist.Service.Interfaces
{
    public interface IBaseService
    {
        Task<Result> SaveChangesAsync();
        Result SaveChanges();
    }
}
