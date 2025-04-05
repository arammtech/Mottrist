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
    /// Service class for handling operations related to Car Body Types.
    /// Implements ICarBodyTypeService.
    /// </summary>
    public class CarBodyTypeService : BaseService, ICarBodyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for database operations
        private readonly IMapper _mapper;  // AutoMapper for DTO mapping

        /// <summary>
        /// Constructor initializes the service with Unit of Work and AutoMapper.
        /// Throws ArgumentNullException if dependencies are null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public CarBodyTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of car body types asynchronously, optionally filtered.
        /// </summary>
        /// <param name="filter">Optional expression filter</param>
        /// <returns>A DataResult containing a list of CarBodyTypeDto objects</returns>
        public async Task<DataResult<CarBodyTypeDto>> GetAllAsync(Expression<Func<BodyType, bool>>? filter = null)
        {
            try
            {
                // Fetch body types query from the repository
                var bodyTypesQuery = _unitOfWork.Repository<BodyType>().Query().AsQueryable();

                // Apply filter if provided
                if (filter != null)
                    bodyTypesQuery = bodyTypesQuery.Where(filter);

                // Project to DTO and execute the query asynchronously
                var bodyTypes = await bodyTypesQuery
                    .ProjectTo<CarBodyTypeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return the result with data
                return new DataResult<CarBodyTypeDto>
                {
                    Data = bodyTypes.Any() ? bodyTypes : Enumerable.Empty<CarBodyTypeDto>()
                };
            }
            catch (Exception)
            {
                // Return null in case of an exception
                return null;
            }
        }
    }
}