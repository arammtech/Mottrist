using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.DTOs;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq;
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
        /// <summary>
        /// Initializes a new instance of the CarBrandService class.
        /// Throws ArgumentNullException if any dependency is null.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance for database access</param>
        /// <param name="mapper">AutoMapper instance for mapping entities</param>
        public CarBrandService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CarBrandDto>> AddAsync(AddCarBrandDto addCarBrandDto)
        {
            if (addCarBrandDto == null)
            {
                return Result<CarBrandDto>.Failure("Invalid Brand Object Be Null.");
            }

            try
            {
                var brand = _mapper.Map<Brand>(addCarBrandDto);
                await _unitOfWork.Repository<Brand>().AddAsync(brand);
                var saveResult = await SaveChangesAsync();

                if (saveResult.IsSuccess)
                    return Result<CarBrandDto>.Success(_mapper.Map<CarBrandDto>(brand));
                return Result<CarBrandDto>.Failure($"Error creating Brand");
            }
            catch (Exception ex)
            {
                return Result<CarBrandDto>.Failure($"Error creating Brand: {ex.Message}");
            }

        }

        public async Task<Result> DeleteAsync(int brandId)
        {
            if (brandId < 0)
            {
                return Result.Failure("Invalid Brand id.");
            }

            try
            {
                var brand = await _unitOfWork.Repository<Brand>().Table.FirstOrDefaultAsync(x => x.Id == brandId);

                if (brand == null)
                    return Result.Failure($"Brand have Id: {brandId} Not Found");


                await _unitOfWork.Repository<Brand>().DeleteAsync(brand);
                var saveResult = await SaveChangesAsync();

                if (saveResult.IsSuccess)
                    return Result<CarBrandDto>.Success(_mapper.Map<CarBrandDto>(brand));
                return Result<CarBrandDto>.Failure($"Error deleting Brand");
            }
            catch (Exception ex)
            {
                return Result<CarBrandDto>.Failure($"Error deleting Brand: {ex.Message}");
            }
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
                var brandsQuery = _unitOfWork.Repository<Brand>().Table;

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

        public async Task<PaginatedResult<CarBrandDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Brand, bool>>? filter = null)
        {
            try
            {
                // Build the base query with necessary includes
                var BrandQuery = _unitOfWork.Repository<Brand>().Table.AsNoTracking();

                // Apply filters if provided
                if (filter != null)
                {
                    BrandQuery.Where(filter);
                }

                // Get the total count of records before applying pagination
                var totalRecords = await BrandQuery.CountAsync();

                var carBrands = await BrandQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<CarBrandDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return paginated results
                return new PaginatedResult<CarBrandDto>
                {
                    Data = carBrands,
                    TotalRecordsCount = totalRecords,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = carBrands.Count
                };
            }
            catch (Exception ex)
            {
                // log the exception
                return null;
            }
        }

        public async Task<CarBrandDto?> GetByIdAsync(int brandId)
        {
            if (brandId < 0)
            {
                return new CarBrandDto();
            }

            try
            {
                var brand =  await _unitOfWork.Repository<Brand>().Table
                    .AsNoTracking().FirstOrDefaultAsync(x => x.Id == brandId);
                return brand != null? _mapper.Map<CarBrandDto>(brand): null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Result<CarBrandDto>> UpdateAsync(UpdateCarBrandDto updateCarBrandDto)
        {
            if (updateCarBrandDto == null)
            {
                return Result<CarBrandDto>.Failure("Invalid Brand Object Be Null.");
            }

            try
            {
                var exstingBrand = await _unitOfWork.Repository<Brand>().Table.FirstOrDefaultAsync(x => x.Id == updateCarBrandDto.Id);

                if (exstingBrand == null)
                    return Result<CarBrandDto>.Failure($"Brand Not Found");

                var updatedBrand = _mapper.Map(updateCarBrandDto,exstingBrand);

                await _unitOfWork.Repository<Brand>().UpdateAsync(updatedBrand);
                var saveResult = await SaveChangesAsync();

                if (saveResult.IsSuccess)
                    return Result<CarBrandDto>.Success(_mapper.Map<CarBrandDto>(updateCarBrandDto));
                return Result<CarBrandDto>.Failure($"Error deleting Brand");
            }
            catch (Exception ex)
            {
                return Result<CarBrandDto>.Failure($"Error deleting Brand: {ex.Message}");
            }
        }
    }
}
