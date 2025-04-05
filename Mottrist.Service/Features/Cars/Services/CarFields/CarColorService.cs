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
    /// Service class for managing car colors.
    /// Implements ICarColorService.
    /// </summary>
    public class CarColorService : BaseService, ICarColorService
    {
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for database transactions
        private readonly IMapper _mapper;  // AutoMapper instance for DTO mapping

        /// <summary>
        /// Initializes a new instance of the CarColorService class.
        /// Throws ArgumentNullException if dependencies are null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance for database operations</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public CarColorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves all car colors asynchronously, optionally filtered by a provided expression.
        /// </summary>
        /// <param name="filter">Optional filter expression for querying colors</param>
        /// <returns>A DataResult containing a list of CarColorDto objects</returns>
        public async Task<DataResult<CarColorDto>> GetAllAsync(Expression<Func<Color, bool>>? filter = null)
        {
            try
            {
                // Retrieve color data from the repository
                var colorsQuery = _unitOfWork.Repository<Color>().Query().AsQueryable();

                // Apply filter if provided
                if (filter != null)
                    colorsQuery = colorsQuery.Where(filter);

                // Map to DTO and execute the query asynchronously
                var colors = await colorsQuery
                    .ProjectTo<CarColorDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return data result
                return new DataResult<CarColorDto>
                {
                    Data = colors.Any() ? colors : Enumerable.Empty<CarColorDto>()
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
