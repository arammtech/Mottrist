using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Domain.Common;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.General.DTOs;

namespace Mottrist.Service.Features.Cities.Interfaces
{
    /// <summary>
    /// Defines the operations related to city entities.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Retrieves all cities matching the optional filter.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>A <see cref="Task{DataResult{CityDto}}"/> containing the list of cities.</returns>
        Task<DataResult<CityDto>?> GetAllAsync(Expression<Func<City, bool>>? filter = null);

        /// <summary>
        /// Retrieves all cities along with their country information, matching the optional filter.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>A <see cref="Task{DataResult{CityWithCountryDto}}"/> containing the list of cities with country data.</returns>
        Task<DataResult<CityWithCountryDto>?> GetAllWithCountryAsync(Expression<Func<City, bool>>? filter = null);

        /// <summary>
        /// Retrieves a city by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <returns>A <see cref="Task{CityDto}"/> representing the city.</returns>
        Task<CityDto?> GetByIdAsync(int id);
    }
}
