using Feature.Car.DTOs;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using AutoMapper;
using Mottrist.Service.Features.Cars.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Cars.DTOs;
using Microsoft.AspNetCore.Http;
using static Mottrist.Utilities.Global.GlobalFunctions;
namespace Mottrist.Service.Features.Cars.Services
{
    public class CarService : BaseService, ICarService
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
        /// - A <see cref="DataResult{CarDto}"/> of cars matching the filter.
        /// - An empty result if no cars match the filter or an exception occurs.
        /// </returns>
        public async Task<DataResult<CarDto>?> GetAllAsync(Expression<Func<Car, bool>>? filter = null)
        {
            try
            {
                // Build the query from the repository's Table property
                var carsQuery = _unitOfWork.Repository<Car>().Query();

                // Apply filter if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                // Execute query and transform to CarDto
                var cars = await carsQuery
                    .Select(car => new CarDto
                    {
                        Id = car.Id,
                        Brand = car.Brand.Name,
                        Model = car.Model.Name,
                        Year = car.Year,
                        FuelType = car.FuelType.Type,
                        BodyType = car.BodyType.Type,
                        Color = car.Color.Name,
                        NumberOfSeats = car.NumberOfSeats,
                        MainCarImageUrl = car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl ?? string.Empty,
                        AdditionalCarImageUrls = car.CarImages.Where(ci => !ci.IsMain).Select(ci => ci.ImageUrl).ToList()
                    })
                    .ToListAsync();

                // Return results in DataResult format
                return new DataResult<CarDto>
                {
                    Data = cars.Any() ? cars : Enumerable.Empty<CarDto>()
                };
            }
            catch (Exception)
            {
                return new DataResult<CarDto>
                {
                    Data = Enumerable.Empty<CarDto>()
                };
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
        /// - Cars: A <see cref="PaginatedResult{CarDto}"/> of cars for the specified page.
        /// - TotalRecords: The total count of cars matching the criteria.
        /// </returns>
        public async Task<PaginatedResult<CarDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Car, bool>>? filter = null)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be greater than 0.");
            }

            try
            {
                // Build the base query with necessary includes
                var carsQuery = _unitOfWork.Repository<Car>().Query()
                    .Include(car => car.Brand)
                    .Include(car => car.Model)
                    .Include(car => car.FuelType)
                    .Include(car => car.BodyType)
                    .Include(car => car.Color)
                    .Include(car => car.CarImages)
                    .AsQueryable();

                // Apply filters if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                // Get the total count of records before applying pagination
                var totalRecords = await carsQuery.CountAsync();

                // Apply pagination
                var paginatedCars = await carsQuery
                    .OrderBy(car => car.Id) // Sort by primary key or any other field
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(car => new CarDto
                    {
                        Id = car.Id,
                        Brand = car.Brand.Name,
                        Model = car.Model.Name,
                        Year = car.Year,
                        FuelType = car.FuelType.Type,
                        BodyType = car.BodyType.Type,
                        Color = car.Color.Name,
                        NumberOfSeats = car.NumberOfSeats,
                        MainCarImageUrl = car.CarImages.FirstOrDefault(ci => ci.IsMain).ImageUrl,
                        AdditionalCarImageUrls = car.CarImages.Where(ci => !ci.IsMain).Select(ci => ci.ImageUrl).ToList()
                    })
                    .ToListAsync();

                // Return paginated results
                return new PaginatedResult<CarDto>
                {
                    Data = paginatedCars,
                    TotalRecordsCount = totalRecords,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = paginatedCars.Count
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a car by its unique identifier.
        /// </summary>
        /// <param name="carId">The unique identifier of the car to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains:
        /// - A <see cref="AddCarDto"/> object if the car is found, or
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
            catch (Exception)
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
        public async Task<Result> AddAsync(AddCarDto carDto)
        {
            if (carDto == null)
            {
                return Result.Failure("Invalid Car Object Be Null.");
            }

            try
            {
                var car = _mapper.Map<Car>(carDto);
                car.Id = 0;
                await _unitOfWork.Repository<Car>().AddAsync(car);

                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (car.Id <= 0 | !saveResult.IsSuccess)
                    return Result.Failure("Failed to save the car to the database.");

                carDto.Id = car.Id;

                return Result.Success();
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
        public async Task<Result> UpdateAsync(UpdateCarDto carDto)
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
        /// Adds or updates the main car image based on the provided details.
        /// </summary>
        /// <param name="carImageDto">The DTO containing car image details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> AddCarImageAsync(CarImageDto carImageDto)
        {
            // Validate the input DTO
            if (carImageDto == null)
            {
                return Result.Failure("Car image object cannot be null.");
            }

            if (carImageDto.CarId <= 0)
            {
                return Result.Failure("Invalid CarId. CarId must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(carImageDto.ImageUrl))
            {
                return Result.Failure("Invalid ImageUrl. ImageUrl cannot be null or empty.");
            }


            try
            {
                if(carImageDto.IsMain)
                {
                    var HasMain = await _unitOfWork.Repository<CarImage>().GetAsync(ci => ci.CarId == carImageDto.CarId && ci.IsMain);
                    if(HasMain != null )
                    {
                        HasMain.IsMain = false;
                       await _unitOfWork.Repository<CarImage>().UpdateAsync(HasMain);
                        var result = await _unitOfWork.SaveChangesAsync();
                        if (!result.IsSuccess)
                        {
                            return Result.Failure("Failed to update the car image.");
                        }

                    }
                }

                var isImageExists = await _unitOfWork.Repository<CarImage>().GetAsync(ci => ci.CarId == carImageDto.CarId && ci.ImageUrl == carImageDto.ImageUrl);

                if(isImageExists != null)
                {
                    isImageExists.IsMain = carImageDto.IsMain;

                    await _unitOfWork.Repository<CarImage>().UpdateAsync(isImageExists);
                    var result = await _unitOfWork.SaveChangesAsync();
                    if (!result.IsSuccess)
                    {
                        return Result.Failure("Failed to update the car image.");
                    }

                    return result;
                }

                // Create a new main car image if none exists
                var newCarImageEntity = new CarImage
                {
                    CarId = carImageDto.CarId,
                    ImageUrl = carImageDto.ImageUrl,
                    IsMain = carImageDto.IsMain
                };
                await _unitOfWork.Repository<CarImage>().AddAsync(newCarImageEntity);


                // Save changes to the database
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update or add the car image.");
                }

                // Commit the transaction
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback the transaction on failure
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"An error occurred: {ex.Message}");
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
        /// Adds multiple car images for a car and sets one as the main image based on the provided index.
        /// </summary>
        /// <param name="carImageDtos">The list of car image DTOs to add.</param>
        /// <param name="mainImageIndex">The index of the image to set as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> AddCarImagesAsync(IEnumerable<CarImageDto> carImageDtos, byte mainImageIndex = 0)
        {
            // Validate the input
            if (carImageDtos == null || !carImageDtos.Any())
            {
                return Result.Failure("Car image list cannot be null or empty.");
            }

            try
            {
                // Start a transaction
                var transaction = await _unitOfWork.StartTransactionAsync();
                if (!transaction.IsSuccess)
                {
                    return Result.Failure("Failed to start transaction.", true);
                }

                var carId = carImageDtos.FirstOrDefault()?.CarId ?? 0;
                if (carId <= 0)
                {
                    return Result.Failure("Invalid CarId. CarId must be greater than zero.");
                }

                // Retrieve existing car images
                var existingCarImages = await _unitOfWork.Repository<CarImage>()
                    .GetAllAsync(ci => ci.CarId == carId);

                // Process the provided images
                var index = 0;
                foreach (var carImageDto in carImageDtos)
                {
                    bool isMain = index == mainImageIndex; // Set this image as main if the index matches

                    // Check if the image already exists
                    var existingImage = existingCarImages?.FirstOrDefault(ci => ci.ImageUrl == carImageDto.ImageUrl);
                    if (existingImage != null)
                    {
                        // Update the existing image's main status
                        existingImage.IsMain = isMain;
                        _unitOfWork.Repository<CarImage>().Update(existingImage);
                    }
                    else
                    {
                        // Add new image
                        var newCarImageEntity = new CarImage
                        {
                            CarId = carId,
                            ImageUrl = carImageDto.ImageUrl,
                            IsMain = isMain
                        };
                        await _unitOfWork.Repository<CarImage>().AddAsync(newCarImageEntity);
                    }

                    index++;
                }

                // Ensure only one image is set as main
                if (mainImageIndex >= 0 && mainImageIndex < carImageDtos.Count())
                {
                    foreach (var image in existingCarImages)
                    {
                        image.IsMain = false;
                        _unitOfWork.Repository<CarImage>().Update(image);
                    }

                    var mainImage = existingCarImages.FirstOrDefault(ci => ci.ImageUrl == carImageDtos.ElementAt(mainImageIndex).ImageUrl);
                    if (mainImage != null)
                    {
                        mainImage.IsMain = true;
                        _unitOfWork.Repository<CarImage>().Update(mainImage);
                    }
                }

                // Save changes to the database
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to save car images.");
                }

                // Commit the transaction
                await _unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Rollback the transaction on failure
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"An error occurred: {ex.Message}");
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

        /// <summary>
        /// Updates a car image, either by replacing the image file or updating metadata.
        /// </summary>
        /// <param name="carId">The ID of the car associated with the image.</param>
        /// <param name="imageUrl">Optional: The URL of the existing image to update metadata.</param>
        /// <param name="newImageFile">Optional: A new image file to replace the existing image.</param>
        /// <param name="isMain">Specifies whether the image should be marked as the main image.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> UpdateImageAsync(int carId, string? imageUrl, IFormFile? newImageFile, bool isMain)
        {
            if (carId <= 0)
            {
                return Result.Failure("Invalid CarId. It must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl) && newImageFile == null)
            {
                return Result.Failure("Either imageUrl or newImageFile must be provided.");
            }

            try
            {
                // Start a transaction to ensure atomicity
                var transactionResult = await _unitOfWork.StartTransactionAsync();
                if (!transactionResult.IsSuccess)
                {
                    return Result.Failure("Failed to start a transaction.", true);
                }

                // Metadata Update (using URL)
                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    var existingImage = await _unitOfWork.Repository<CarImage>()
                        .GetAsync(ci => ci.CarId == carId && ci.ImageUrl == imageUrl);

                    if (existingImage == null)
                    {
                        return Result.Failure($"No image found with the provided URL: {imageUrl}");
                    }

                    // Update metadata (e.g., IsMain flag)
                    existingImage.IsMain = isMain;
                    _unitOfWork.Repository<CarImage>().Update(existingImage);
                }

                // File Replacement (uploading a new image)
                if (newImageFile != null)
                {
                    // Save the new image file
                    var newImageUrl = await SaveImageAsync(newImageFile, "car-images");

                    // Create a new image entity for the replacement
                    var newImageEntity = new CarImage
                    {
                        CarId = carId,
                        ImageUrl = newImageUrl,
                        IsMain = isMain
                    };
                    await _unitOfWork.Repository<CarImage>().AddAsync(newImageEntity);

                    // Optionally: Remove the old image if needed (based on business requirements)
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        var oldImage = await _unitOfWork.Repository<CarImage>()
                            .GetAsync(ci => ci.CarId == carId && ci.ImageUrl == imageUrl);

                        if (oldImage != null)
                        {
                            _unitOfWork.Repository<CarImage>().Delete(oldImage);
                        }
                    }
                }

                // Save changes to the database
                var saveResult = await _unitOfWork.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result.Failure("Failed to update car image.");
                }

                // Commit the transaction
                await _unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
