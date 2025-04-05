using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Services.CarFields
{
    /// <summary>
    /// Service class responsible for handling car brand-related operations.
    /// Implements ICarBrandService.
    /// </summary>
    public class CarBrandService : BaseService, ICarBrandService
    {
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for database interactions
        private readonly IMapper _mapper;  // AutoMapper instance for DTO mapping
        private readonly ITravelerService _travelerService;  // Traveler service dependency (unused in this class)

        /// <summary>
        /// Initializes a new instance of the CarBrandService class.
        /// Throws ArgumentNullException if any dependency is null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance for database access</param>
        /// <param name="mapper">AutoMapper instance for mapping entities</param>
        public CarBrandService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves all car brands, optionally filtered based on the provided expression.
        /// </summary>
        /// <param name="filter">An optional filter expression</param>
        /// <returns>A DataResult containing a list of CarBrandDto objects</returns>
        public async Task<DataResult<CarBrandDto>> GetAllAsync(Expression<Func<Brand, bool>>? filter = null)
        {
            try
            {
                // Retrieve the brand query from the repository
                var brandsQuery = _unitOfWork.Repository<Brand>().Query().AsQueryable();

                // Apply the filter if provided
                if (filter != null)
                    brandsQuery = brandsQuery.Where(filter);

                // Map to DTO and execute the query asynchronously
                var brands = await brandsQuery
                    .ProjectTo<CarBrandDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return data result
                return new DataResult<CarBrandDto>
                {
                    Data = brands.Any() ? brands : Enumerable.Empty<CarBrandDto>()
                };
            }
            catch (Exception)
            {
                // Handle any exceptions gracefully and return null
                return null;
            }
        }
    }
}
