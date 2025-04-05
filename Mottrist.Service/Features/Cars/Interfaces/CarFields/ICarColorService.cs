using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    /// <summary>
    /// Interface defining operations for car colors.
    /// Inherits from IBaseService.
    /// </summary>
    public interface ICarColorService : IBaseService
    {
        /// <summary>
        /// Retrieves a list of all car colors asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying car colors</param>
        /// <returns>A Task representing the asynchronous operation, containing a DataResult with a list of CarColorDto objects</returns>
        Task<DataResult<CarColorDto>> GetAllAsync(Expression<Func<Color, bool>>? filter = null);
    }
}
