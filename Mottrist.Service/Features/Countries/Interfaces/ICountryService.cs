using Mottrist.Service.Features.General;

namespace Mottrist.Service.Features.Countries.Interfaces
{
    public interface ICountryService : IBaseService
    {

        bool CountryExists(int Id);

    }
}
