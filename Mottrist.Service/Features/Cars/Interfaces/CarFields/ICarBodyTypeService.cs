using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car body types.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarBodyTypeService : IBaseService
    {
        /// <summary>
        /// Retrieves a list of all car body types asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car body types</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarBodyTypeDto objects</returns>
        Task<DataResult<CarBodyTypeDto>> GetAllAsync(Expression<Func<BodyType, bool>>? filter = null);
    }
}
