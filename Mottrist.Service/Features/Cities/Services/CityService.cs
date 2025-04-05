using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.General;
using System.Linq.Expressions;
using Mottrist.Service.Features.Cities.Interfaces;

namespace Mottrist.Service.Features.Cities.Services
{
    /// <summary>
    /// Provides operations related to city entities, including retrieval and transformation to DTOs.
    /// </summary>
    public class CityService : BaseService, ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for accessing repositories.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of cities based on the provided filter and maps them to <see cref="CityDto"/>.
        /// </summary>
        /// <param name="filter">An optional filter expression for querying cities.</param>
        /// <returns>A <see cref="DataResult{CityDto}"/> containing the list of cities.</returns>
        public async Task<DataResult<CityDto>?> GetAllAsync(Expression<Func<City, bool>>? filter = null)
        {
            try
            {
                var cityQuery = _unitOfWork.Repository<City>().Query()
                    .Include(c => c.Country)
                    .AsQueryable();

                if (filter != null)
                {
                    cityQuery = cityQuery.Where(filter);
                }

                var cities = await cityQuery.Select(city => new CityDto
                {
                    Id = city.Id,
                    Name = city.Name
                }).ToListAsync();

                return new DataResult<CityDto>
                {
                    Data = cities.Any() ? cities : Enumerable.Empty<CityDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list of cities with their corresponding countries and maps them to <see cref="CityWithCountryDto"/>.
        /// </summary>
        /// <param name="filter">An optional filter expression for querying cities.</param>
        /// <returns>A <see cref="DataResult{CityWithCountryDto}"/> containing the list of cities with country information.</returns>
        public async Task<DataResult<CityWithCountryDto>?> GetAllWithCountryAsync(Expression<Func<City, bool>>? filter = null)
        {
            try
            {
                var cityQuery = _unitOfWork.Repository<City>().Query()
                    .Include(c => c.Country)
                    .AsQueryable();

                if (filter != null)
                {
                    cityQuery = cityQuery.Where(filter);
                }

                var cities = await cityQuery.Select(city => new CityWithCountryDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    Country = city.Country.Name
                }).ToListAsync();

                return new DataResult<CityWithCountryDto>
                {
                    Data = cities.Any() ? cities : Enumerable.Empty<CityWithCountryDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a city by its unique identifier and maps it to <see cref="CityDto"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <returns>A <see cref="CityDto"/> representing the city.</returns>
        public async Task<CityDto?> GetByIdAsync(int id)
        {
            try
            {
                var city = await _unitOfWork.Repository<City>().GetAsync(i => i.Id == id);

                if (city == null)
                {
                    return null;
                }

                var cityDto = _mapper.Map<CityDto>(city);

                return cityDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
