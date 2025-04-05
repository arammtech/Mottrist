using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Countries.Interfaces
{
    /// <summary>
    /// Defines the contract for country-related operations.
    /// Provides methods to retrieve country data synchronously and asynchronously.
    /// </summary>
    public interface ICountryService : IBaseService
    {
        /// <summary>
        /// Retrieves a country by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>A <see cref="GetCountryDto"/> representing the country, or null if not found.</returns>
        GetCountryDto? GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a country by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>A task that resolves to a <see cref="GetCountryDto"/> or null if not found.</returns>
        Task<GetCountryDto?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves a list of countries, optionally filtered.
        /// </summary>
        /// <param name="filter">An optional filter expression to apply.</param>
        /// <returns>A <see cref="DataResult{T}"/> containing a list of country DTOs.</returns>
        DataResult<GetCountryDto>? GetAll(Expression<Func<Country, bool>>? filter = null);

        /// <summary>
        /// Asynchronously retrieves a list of countries, optionally filtered.
        /// </summary>
        /// <param name="filter">An optional filter expression to apply.</param>
        /// <returns>A task that resolves to a <see cref="DataResult{T}"/> containing a list of country DTOs.</returns>
        Task<DataResult<GetCountryDto>?> GetAllAsync(Expression<Func<Country, bool>>? filter = null);
    }
}
