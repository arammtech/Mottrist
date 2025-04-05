using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car fuel types.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarFuelTypeService : IBaseService
    {
        /// <summary>
        /// Retrieves a list of all car fuel types asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car fuel types</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarFuelTypeDto objects</returns>
        Task<DataResult<CarFuelTypeDto>> GetAllAsync(Expression<Func<FuelType, bool>>? filter = null);
    }
}
