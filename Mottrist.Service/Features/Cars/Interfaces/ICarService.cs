using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces
{
    public interface ICarService
    {
        #region Get
        Task<IEnumerable<CarDto>> GetAllAsync(Expression<Func<Car, bool>>? filter = null);
        Task<(IEnumerable<CarDto>? Cars, int? TotalRecords)> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Car, bool>>? filter = null);
        Task<CarDto> GetByIdAsync(int carId);

        #endregion
        Task<Result> AddAsync(CarDto carDto);
        Task<Result> UpdateAsync(CarDto carDto);
        Task<Result> DeleteAsync(int carId);
        Task<IEnumerable<CarImageDto>> GetCarImagesAsync(int carId);
        Task<Result> AddCarImageAsync(CarImageDto carImageDto);
        Task<Result> RemoveCarImageAsync(string imageUrl, int carId);
        Task<Result> SetMainImageAsync(int carId, string imageUrl);
    }
}
