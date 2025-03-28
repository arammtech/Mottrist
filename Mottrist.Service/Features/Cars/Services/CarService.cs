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
    public class CarService : BaseService , ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of cars, optionally filtered by the specified criteria.
        /// </summary>
        /// <param name="filter">Optional: A filter expression to apply to the car query. Defaults to null.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains:
        /// - An <see cref="IEnumerable{CarDto}"/> of cars matching the filter, or
        /// - An empty enumerable if no cars match the filter or an exception occurs.
        /// </returns>
        public async Task<IEnumerable<CarDto>?> GetAllAsync(Expression<Func<Car, bool>>? filter = null)
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
                return Enumerable.Empty<CarDto>();
            }
        }

        /// <summary>
        /// Retrieves a paginated list of cars based on the specified criteria.
        /// </summary>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of cars per page. Defaults to 10.</param>
        /// <param name="filter">Optional: A filter expression to apply to the car query. Defaults to null.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains:
        /// - Cars: An <see cref="IEnumerable{CarDto}"/> of cars for the specified page, or null if an error occurs.
        /// - TotalRecords: The total count of cars matching the criteria, or null if an error occurs.
        /// </returns>
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
                var totalRecords = carsQuery.Count();

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

        /// <summary>
        /// Retrieves a car by its unique identifier.
        /// </summary>
        /// <param name="carId">The unique identifier of the car to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains:
        /// - A <see cref="CarDto"/> object if the car is found, or
        /// - Null if the car is not found or an error occurs.
        /// </returns>
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


        /// <summary>
        /// Adds a new car to the database.
        /// </summary>
        /// <param name="carDto">The data transfer object containing the car details to be added.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation:
        /// - Returns <see cref="Result.Success"/> if the car is successfully saved.
        /// - Returns <see cref="Result.Failure"/> with an appropriate error message if the operation fails.
        /// </returns>
        public async Task<Result> AddAsync(CarDto carDto)
        {
            if (carDto == null)
            {
                return Result.Failure("Invalid Car Object Be Null.");
            }

            try
            {
                var car = _mapper.Map<Car>(carDto);
                await _unitOfWork.Repository<Car>().AddAsync(car);

                if (car.Id <= 0)
                    return Result.Failure("Failed to save the car to the database.");

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

        /// <summary>
        /// Updates a car entity with the provided data.
        /// </summary>
        /// <param name="carDto">The data transfer object containing the updated car information.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the update operation was successful or not.
        /// </returns>
        public async Task<Result> UpdateAsync(CarDto carDto)
        {
            if (carDto == null)
            {
                return Result.Failure("Invalid Car Object Be Null.");
            }

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

        /// <summary>
        /// Deletes a car by its unique identifier.
        /// </summary>
        /// <param name="carId">The unique identifier of the car to delete.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the deletion was successful or not.
        /// </returns>
        public async Task<Result> DeleteAsync(int carId)
        {
            if (carId <= 0)
            {
                return Result.Failure("Invalid CarId.");
            }

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

        /// <summary>
        /// Retrieves all images associated with a specific car.
        /// </summary>
        /// <param name="carId">The unique identifier of the car whose images are to be retrieved.</param>
        /// <returns>
        /// A collection of <see cref="CarImageDto"/> objects, or null if no images are found.
        /// </returns>
        public async Task<IEnumerable<CarImageDto>?> GetCarImagesAsync(int carId)
        {
            if (carId <= 0)
            {
                return null;
            }

            try
            {
                // Check if the car exists to ensure valid carId
                var carExists = await _unitOfWork.Repository<Car>().GetAsync(c => c.Id == carId) != null;
                if (!carExists) return Enumerable.Empty<CarImageDto>();

                // Retrieve car images for the given carId from the CarImage repository
                var carImages = await _unitOfWork.Repository<CarImage>().GetAllAsync(ci => ci.CarId == carId);

                if (!carImages.Any())
                {
                    return null;
                }

                var carImageDtos = _mapper.Map<IEnumerable<CarImageDto>>(carImages);

                return carImageDtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new image to a car.
        /// </summary>
        /// <param name="carImageDto">The data transfer object representing the new car image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the operation was successful or not.
        /// </returns>
        public async Task<Result> AddCarImageAsync(CarImageDto carImageDto)
        {
            if (carImageDto == null)
            {
                return Result.Failure("Invalid Cant Object Be Null.");
            }

            if (carImageDto.CarId <= 0)
            {
                return Result.Failure("Invalid CarId.");
            }

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


        /// <summary>
        /// Removes a specific car image by its URL and associated car identifier.
        /// </summary>
        /// <param name="imageUrl">The URL of the car image to remove.</param>
        /// <param name="carId">The unique identifier of the associated car.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the operation was successful or not.
        /// </returns>
        public async Task<Result> RemoveCarImageAsync(string imageUrl, int carId)
        {
            if(string.IsNullOrWhiteSpace(imageUrl))
            {
                return Result.Failure("Invalid ImageUrl.");
            }

            if (carId <= 0)
            {
                return Result.Failure("Invalid CarId.");
            }

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


        /// <summary>
        /// Sets a specific car image as the main image for the car.
        /// </summary>
        /// <param name="carId">The unique identifier of the car.</param>
        /// <param name="imageUrl">The URL of the image to set as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the operation was successful or not.
        /// </returns>
        public async Task<Result> SetMainImageAsync(int carId, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Result.Failure("Invalid ImageUrl.");
            }

            if (carId <= 0)
            {
                return Result.Failure("Invalid CarId.");
            }

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
