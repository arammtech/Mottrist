using Feature.Car.DTOs;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using AutoMapper;
using Mottrist.Service.Features.Cars.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Service.Features.General;

namespace Mottrist.Service.Features.Cars.Services
{
    public class CarService :BaseService , ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync(Expression<Func<Car, bool>>? filter = null)
        {
            try
            {
                // Build the query from the repository's Table property
                var carsQuery = _unitOfWork.Repository<Car>().Table.AsQueryable();

                // Apply filter if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                var cars = await carsQuery.ToListAsync();

                if (!cars.Any())
                    return Enumerable.Empty<CarDto>();

                return _mapper.Map<IEnumerable<CarDto>>(cars);
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism in place)
                // Log.Error(ex, "Error occurred while fetching cars");

                return Enumerable.Empty<CarDto>();
            }
        }

        public async Task<(IEnumerable<CarDto>? Cars, int? TotalRecords)> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Car, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
            {
                return (null, null);
            }

            try
            {
                // Start building the query
                var carsQuery = _unitOfWork.Repository<Car>().Table.AsQueryable();

                // Apply filters if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                // Get the total count of records before applying pagination
                var totalRecords =  carsQuery.Count();

                // Apply pagination
                var paginatedCars = await carsQuery
                    .OrderBy(c => c.Id) // Sort by primary key (Id) or any other field
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Map the results to CarDto
                var carDtos = _mapper.Map<IEnumerable<CarDto>>(paginatedCars);

                return (carDtos, totalRecords);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return (null, null);
            }
        }

        public async Task<CarDto?> GetByIdAsync(int carId)
        {
            try
            {
                var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == carId);
                if (car == null) return null;

                var carDto = _mapper.Map<CarDto>(car);
                return carDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Result> AddAsync(CarDto carDto)
        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                await _unitOfWork.Repository<Car>().AddAsync(car);
                     
                if(car.Id <= 0) return Result.Failure("Failed to save the car to the database.");

                carDto.Id = car.Id;

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to save the car to the database.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error creating car: {ex.Message}");
            }
        }

        public async Task<Result> UpdateAsync(CarDto carDto)
        {
            try
            {
                var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == carDto.Id);
                if (car == null) return Result.Failure("Car not found.");

                _mapper.Map(carDto, car);
                _unitOfWork.Repository<Car>().Update(car);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to update the car.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error updating car: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(int carId)
        {
            try
            {
                var car = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == carId);
                if (car == null) return Result.Failure("Car not found.");

                _unitOfWork.Repository<Car>().Delete(car);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to delete the car.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting car: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CarImageDto>> GetCarImagesAsync(int carId)
        {
            try
            {
                // Check if the car exists to ensure valid carId
                var carExists = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == carId) != null;
                if (!carExists) return Enumerable.Empty<CarImageDto>();

                // Retrieve car images for the given carId from the CarImage repository
                var carImages = await _unitOfWork.Repository<CarImage>().GetAllAsync(ci => ci.CarId == carId);
                if (!carImages.Any()) return Enumerable.Empty<CarImageDto>();

                // Map the CarImage entities to CarImageDto
                var carImageDtos = _mapper.Map<IEnumerable<CarImageDto>>(carImages);

                return carImageDtos;
            }
            catch (Exception ex)
            {
                // Log the exception if needed, e.g., using ILogger
                // Log.Error(ex, "Error occurred while retrieving car images");

                // Return an empty collection as a fallback
                return Enumerable.Empty<CarImageDto>();
            }
        }
        public async Task<Result> AddCarImageAsync(CarImageDto carImageDto)
        {
            try
            {
                var carImage = _mapper.Map<CarImage>(carImageDto);
                await _unitOfWork.Repository<CarImage>().AddAsync(carImage);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to save the car image.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error adding car image: {ex.Message}");
            }
        }

        public async Task<Result> RemoveCarImageAsync(string imageUrl, int carId)
        {
            try
            {
                var carImage = await _unitOfWork.Repository<CarImage>().GetAsync(ci => ci.ImageUrl == imageUrl && ci.CarId == carId);
                if (carImage == null) return Result.Failure($"Car image not found. ImageUrl: {imageUrl}, CarId: {carId}");

                _unitOfWork.Repository<CarImage>().Delete(carImage);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to delete the car image.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting car image: {ex.Message}");
            }
        }

        public async Task<Result> SetMainImageAsync(int carId, string imageUrl)
        {
            try
            {
                var carImages = await _unitOfWork.Repository<CarImage>().GetAllAsync(ci => ci.CarId == carId);
                if (carImages is null || !carImages.Any())
                    return Result.Failure($"No car images found for CarId: {carId}");

                var targetImage = carImages.FirstOrDefault(ci => ci.ImageUrl == imageUrl);
                if (targetImage == null)
                    return Result.Failure($"Image not found. ImageUrl: {imageUrl}, CarId: {carId}");

                foreach (var image in carImages)
                {
                    image.IsMain = false;
                    _unitOfWork.Repository<CarImage>().Update(image);
                }

                targetImage.IsMain = true;
                _unitOfWork.Repository<CarImage>().Update(targetImage);

                var saveResult = await _unitOfWork.SaveChangesAsync();
                return saveResult.IsSuccess
                    ? Result.Success()
                    : Result.Failure("Failed to set the main image.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error setting main image: {ex.Message}");
            }
        }

    }
}
