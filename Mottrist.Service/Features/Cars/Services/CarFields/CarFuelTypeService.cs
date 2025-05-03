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
    /// Service class responsible for handling operations related to car fuel types.
    /// Implements ICarFuelTypeService.
    /// </summary>
    public class CarFuelTypeService : BaseService, ICarFuelTypeService
    {
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for database operations
        private readonly IMapper _mapper;  // AutoMapper instance for mapping entities to DTOs

        /// <summary>
        /// Initializes a new instance of the CarFuelTypeService class.
        /// Throws ArgumentNullException if any dependency is null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public CarFuelTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves all car fuel types asynchronously, with an optional filter.
        /// </summary>
        /// <param name="filter">Optional expression filter for querying fuel types</param>
        /// <returns>A DataResult containing a list of CarFuelTypeDto objects</returns>
        public async Task<DataResult<CarFuelTypeDto>> GetAllAsync(Expression<Func<FuelType, bool>>? filter = null)
        {
            try
            {
                // Retrieve the fuel type query from the repository
                var fuelTypesQuery = _unitOfWork.Repository<FuelType>().Table.AsQueryable();

                // Apply filter if provided
                if (filter != null)
                    fuelTypesQuery = fuelTypesQuery.Where(filter);

                // Map to DTO and execute the query asynchronously
                var fuelTypes = await fuelTypesQuery
                    .ProjectTo<CarFuelTypeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return the data result
                return new DataResult<CarFuelTypeDto>
                {
                    Data = fuelTypes.Any() ? fuelTypes : Enumerable.Empty<CarFuelTypeDto>()
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
