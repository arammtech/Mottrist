using Mottrist.Domain.Global;

namespace Mottrist.Service.Features.General
{
    public interface IBaseService
    {
        Task<Result> SaveChangesAsync();
        Result SaveChanges();
    }
}
