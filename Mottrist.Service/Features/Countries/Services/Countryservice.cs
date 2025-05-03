using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Cities.Mappers;
using Mottrist.Service.Features.Cities.Interfaces;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.Countries.Interfaces;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;


namespace Mottrist.Service.Features.Countries.Services
{
    /// <summary>
    /// Service for managing countries.
    /// Provides methods to retrieve country data with or without filtering.
    /// </summary>
    public class CountryService : BaseService, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work for handling repository operations.</param>
        /// <param name="mapper">AutoMapper instance for mapping entities.</param>
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a country by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>A <see cref="CountryDto"/> representing the country, or null if not found.</returns>
        public CountryDto? GetById(int id)
        {
            try
            {
                var country = _unitOfWork.Repository<Country>()
                    .Table
                    .Include(c => c.Cities)
                    .FirstOrDefault(i => i.Id == id);

                if (country == null)
                    return null;

                return _mapper.Map<CountryDto>(country);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Asynchronously retrieves a country by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>A task that resolves to a <see cref="CountryDto"/> or null if not found.</returns>
        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            try
            {
                var country = await _unitOfWork.Repository<Country>()
                 .Table
                 .Include(c => c.Cities) 
                 .FirstOrDefaultAsync(i => i.Id == id);

                if (country == null)
                    return null;

                return _mapper.Map<CountryDto>(country);
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        /// <summary>
        /// Retrieves all countries, optionally filtered.
        /// </summary>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>A <see cref="DataResult{T}"/> containing a list of countries.</returns>
        public DataResult<CountryDto>? GetAll(Expression<Func<Country, bool>>? filter = null)
        {
            try
            {
                var countryQuery = _unitOfWork.Repository<Country>().Table
                    .Include(c => c.Cities)
                    .AsQueryable();

                if (filter != null)
                    countryQuery = countryQuery.Where(filter);

                var countries = countryQuery
                    .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                    .ToList();

                return new DataResult<CountryDto>
                {
                    Data = countries
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Asynchronously retrieves all countries, optionally filtered.
        /// </summary>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>A task that resolves to a <see cref="DataResult{T}"/> containing a list of countries.</returns>
        public async Task<DataResult<CountryDto>?> GetAllAsync(Expression<Func<Country, bool>>? filter = null)
        {
            try
            {
                var countryQuery = _unitOfWork.Repository<Country>().Table
                    .Include(c => c.Cities)
                    .AsQueryable();

                if (filter != null)
                    countryQuery = countryQuery.Where(filter);

                var countries = await countryQuery
                  .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                  .ToListAsync();

                return new DataResult<CountryDto>
                {
                    Data = countries.Any() ? countries : Enumerable.Empty<CountryDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
