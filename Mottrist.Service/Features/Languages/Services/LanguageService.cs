using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Languages.DTOs;
using Mottrist.Service.Features.Languages.Interfaces;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Languages.Services
{
    /// <summary>
    /// Provides operations related to language entities, including retrieval and transformation to DTOs.
    /// </summary>
    public class LanguageService : BaseService, ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for accessing repositories.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves all language records optionally filtered by the provided expression and maps them to <see cref="LanguageDto"/>.
        /// </summary>
        /// <param name="filter">An optional filter expression for querying languages.</param>
        /// <returns>
        /// A <see cref="DataResult{LanguageDto}"/> containing the list of languages; if no languages are found, returns an empty enumerable.
        /// Returns null if an exception occurs.
        /// </returns>
        public async Task<DataResult<LanguageDto>?> GetAllAsync(Expression<Func<Language, bool>>? filter = null)
        {
            try
            {
                var languageQuery = _unitOfWork.Repository<Language>().Query().AsQueryable();

                if (filter != null)
                {
                    languageQuery = languageQuery.Where(filter);
                }

                var languageDtos = await languageQuery
                    .AsNoTracking()
                    .Select(l => new LanguageDto
                    {
                        Id = l.Id,
                        Name = l.Name
                    })
                    .ToListAsync();

                return new DataResult<LanguageDto>
                {
                    Data = languageDtos.Any() ? languageDtos : Enumerable.Empty<LanguageDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a specific language by its unique identifier and maps it to <see cref="LanguageDto"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the language.</param>
        /// <returns>
        /// A <see cref="LanguageDto"/> representing the language; returns null if the language is not found or if an error occurs.
        /// </returns>
        public async Task<LanguageDto?> GetByIdAsync(int id)
        {
            try
            {
                var language = await _unitOfWork.Repository<Language>().GetAsync(l => l.Id == id);

                if (language == null)
                {
                    return null;
                }

                return _mapper.Map<LanguageDto>(language);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
