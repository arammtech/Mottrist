
using Mottrist.Service.Features.General;

namespace Mottrist.Service.Features.Country.Interfaces
{
    public interface ICountryService : IBaseService
    {

        bool CountryExists(int Id);

    }
}
