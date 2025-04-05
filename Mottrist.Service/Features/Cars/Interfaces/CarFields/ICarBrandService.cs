using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car brands.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarBrandService : IBaseService
    {
        /// <summary>
        /// Retrieves a list of all car brands asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car brands</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarBrandDto objects</returns>
        Task<DataResult<CarBrandDto>> GetAllAsync(Expression<Func<Brand, bool>>? filter = null);
    }
}
