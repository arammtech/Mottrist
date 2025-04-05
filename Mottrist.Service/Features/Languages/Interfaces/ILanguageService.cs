using System.Linq.Expressions;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Languages.DTOs;

namespace Mottrist.Service.Features.Languages.Interfaces
{
    /// <summary>
    /// Defines the contract for services that handle language-related operations,
    /// including retrieving language data and mapping entities to DTOs.
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// Retrieves all languages that satisfy the optional filter and maps them to <see cref="LanguageDto"/>.
        /// </summary>
        /// <param name="filter">An optional filter expression for querying languages.</param>
        /// <returns>
        /// A <see cref="DataResult{LanguageDto}"/> containing the list of languages; if no languages are found, returns an empty enumerable.
        /// </returns>
        Task<DataResult<LanguageDto>?> GetAllAsync(Expression<Func<Language, bool>>? filter = null);

        /// <summary>
        /// Retrieves a specific language by its unique identifier and maps it to <see cref="LanguageDto"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the language.</param>
        /// <returns>
        /// A <see cref="LanguageDto"/> representing the language; returns null if the language is not found.
        /// </returns>
        Task<LanguageDto?> GetByIdAsync(int id);
    }
}
