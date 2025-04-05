using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car models.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarModelService : IBaseService
    {
        /// <summary>
        /// Retrieves a list of all car models asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car models</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarModelDto objects</returns>
        Task<DataResult<CarModelDto>> GetAllAsync(Expression<Func<Model, bool>>? filter = null);
    }
}
