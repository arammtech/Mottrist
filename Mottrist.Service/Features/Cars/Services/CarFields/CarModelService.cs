using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Services.CarFields
{
    /// <summary>
    /// Service class for managing car models.
    /// Implements ICarModelService.
    /// </summary>
    public class CarModelService : BaseService, ICarModelService
    {
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for managing database operations
        private readonly IMapper _mapper;  // AutoMapper instance for mapping entities to DTOs

        /// <summary>
        /// Initializes a new instance of the CarModelService class.
        /// Throws ArgumentNullException if any dependency is null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance</param>
        /// <param name="mapper">AutoMapper instance for mapping objects</param>
        public CarModelService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves all car models asynchronously, with an optional filter for querying.
        /// </summary>
        /// <param name="filter">Optional filter expression to query car models</param>
        /// <returns>A DataResult containing a list of CarModelDto objects</returns>
        public async Task<DataResult<CarModelDto>> GetAllAsync(Expression<Func<Model, bool>>? filter = null)
        {
            try
            {
                // Retrieve models from the repository as a queryable
                var modelsQuery = _unitOfWork.Repository<Model>().Table.AsQueryable();

                // Apply filter if provided
                if (filter != null)
                    modelsQuery = modelsQuery.Where(filter);

                // Project the result into DTO and execute asynchronously
                var models = await modelsQuery
                    .ProjectTo<CarModelDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return the result
                return new DataResult<CarModelDto>
                {
                    Data = models.Any() ? models : Enumerable.Empty<CarModelDto>()
                };
            }
            catch (Exception)
            {
                // Return null in case of any exception
                return null;
            }
        }
    }
}
