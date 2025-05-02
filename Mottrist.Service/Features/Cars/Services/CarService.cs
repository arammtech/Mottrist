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
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using AutoMapper.QueryableExtensions;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.Cars.Services.CarFields;
using System.Reflection.Metadata.Ecma335;
using Mottrist.Service.Features.General.Images.Interface;
namespace Mottrist.Service.Features.Cars.Services
{
    public class CarService : BaseService, ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICarBrandService _carBrandService;
        private readonly ICarBodyTypeService _carBodyTypeService;
        private readonly ICarFuelTypeService _carFuelTypeService;
        private readonly IImageService _imageService;
        private readonly ICarModelService _carModelService;
        public readonly ICarColorService _carColorService;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper, ICarModelService carModelService, ICarColorService carColorService, ICarBrandService carBrandService, ICarBodyTypeService carBodyTypeService, ICarFuelTypeService carFuelTypeService, IImageService imageService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carBrandService = carBrandService;
            _carBodyTypeService = carBodyTypeService;
            _carFuelTypeService = carFuelTypeService;
            _imageService = imageService;
            _carModelService = carModelService;
            _carColorService = carColorService;
        }

        public async Task<DataResult<CarDto>?> GetAllAsync(Expression<Func<Car, bool>>? filter = null)
        {
            try
            {
                // Build the query from the repository's Table property
                var carsQuery = _unitOfWork.Repository<Car>()
                    .Include(x=> x.Brand, x=> x.Model, x => x.FuelType, x => x.BodyType, x => x.Color, x => x.CarImages)
                    .AsNoTracking();

                // Apply the filter if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                var carDtos = await carsQuery.ToListAsync();
                // Execute the query and map the results to CarDto
                return _mapper.Map<DataResult<CarDto>>(carDtos);
            }
            catch (Exception)
            {
                return new DataResult<CarDto>
                {
                    Data = Enumerable.Empty<CarDto>()
                };
            }
        }

        public async Task<PaginatedResult<CarDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Car, bool>>? filter = null)
        {
            try
            {
                // Build the base query with necessary includes
                var carsQuery = _unitOfWork.Repository<Car>()
                    .Include(x => x.Brand, x => x.Model, x => x.FuelType, x => x.BodyType, x => x.Color, x => x.CarImages)
                    .AsNoTracking();

                // Apply filters if provided
                if (filter != null)
                {
                    carsQuery = carsQuery.Where(filter);
                }

                // Get the total count of records before applying pagination
                var totalRecords = await carsQuery.CountAsync();

                var carDtos = await carsQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Return paginated results
                return new PaginatedResult<CarDto>
                {
                    Data = carDtos,
                    TotalRecordsCount = totalRecords,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = carDtos.Count
                };
            }
            catch (Exception ex)
            {
                // log the exception
                return null;
            }
        }

        public async Task<CarDto?> GetByIdAsync(int carId)
        {
            try
            {
                var carFromDb = await _unitOfWork.Repository<Car>()
                    .Include(x => x.Brand, x => x.Model, x => x.FuelType, x => x.BodyType, x => x.Color, x => x.CarImages)
                    .FirstOrDefaultAsync(c => c.Id == carId);

                if (carFromDb == null) return null;

                return _mapper.Map<CarDto>(carFromDb);
            }
            catch (Exception)
            {
                // log the exception
                return null;
            }
        }

        public async Task<Result<CarDto>> AddAsync(AddCarDto carDto)
        {
            if (carDto == null)
            {
                return Result<CarDto>.Failure("Invalid Car Object Be Null.");
            }

            try
            {

                var car = _mapper.Map<Car>(carDto);

                // Save car images if provided
                if (carDto.CarImages != null && carDto.CarImages.Any())
                {
                    foreach (var image in carDto.CarImages)
                    {
                        string? imageurl = await _imageService.SaveImageAsync(image, ImageCategory.Cars);

                        if (string.IsNullOrWhiteSpace(imageurl))
                        {
                            return Result<CarDto>.Failure("Failed to save car image.");
                        }

                        car.CarImages.Add(new CarImage
                        {
                            ImageUrl = imageurl,
                            IsMain = false
                        });
                    }

                    if (car.CarImages.Any())
                    {
                        car.CarImages.First().IsMain = true; // Set the first image as main
                        await _unitOfWork.Repository<Car>().AddAsync(car);

                        var saveCarResult = await _unitOfWork.SaveChangesAsync();
                        if (!saveCarResult.IsSuccess)
                        {
                            return Result<CarDto>.Failure(errorMessage: "Failed to save car .");
                        }

                        return Result<CarDto>.Success(_mapper.Map<CarDto>(car));
                    }



                }

                return Result<CarDto>.Failure(errorMessage: "Failed to save car .");

            }
            catch (Exception ex)
            {
                return Result<CarDto>.Failure($"Error creating car: {ex.Message}");
            }
        }

        public async Task<Result<CarDto>> UpdateAsync(UpdateCarDto updateCarDto, int carId)
        {
            if (updateCarDto == null)
            {
                return Result<CarDto>.Failure("Invalid Car Object Be Null.");
            }

            try
            {
                var car = await _unitOfWork.Repository<Car>()
                    .Table.Include(x=> x.CarImages)
                    .FirstOrDefaultAsync(c => c.Id == carId);

                if (car == null) return Result<CarDto>.Failure("Car not found.");

                _mapper.Map(updateCarDto, car);


                // handle car images
                if (updateCarDto.CarImages != null && updateCarDto.CarImages.Any())
                {
                    
                    if (car.CarImages.Any())
                    {
                        IEnumerable<CarImage> carImages = car.CarImages.ToList();

                        foreach (var carimage in carImages)
                        {
                            _imageService.DeleteImage(carimage.ImageUrl);
                        }

                        car.CarImages.Clear();
                    }


                    foreach (var image in updateCarDto.CarImages)
                    {
                        string? imageurl = await _imageService.SaveImageAsync(image, ImageCategory.Cars);

                        if (string.IsNullOrWhiteSpace(imageurl))
                        {
                            return Result<CarDto>.Failure("Failed to save car image.");
                        }
                        car.CarImages.Add(new CarImage
                        {
                            ImageUrl = imageurl,
                            IsMain = false
                        });
                    }
                    if (car.CarImages.Any())
                    {
                        car.CarImages.First().IsMain = true; // Set the first image as main
                    }
                }

                await _unitOfWork.Repository<Car>().UpdateAsync(car);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                var carDto = await GetByIdAsync(car.Id);

                return saveResult.IsSuccess
                    ? Result<CarDto>.Success(carDto)
                    : Result<CarDto>.Failure("Failed to update the car.");
            }
            catch (Exception ex)
            {
                return Result<CarDto>.Failure($"Error updating car: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CarImageDto>?> GetCarImagesAsync(int carId)
        {
            try
            {
                var carImages = await _unitOfWork.Repository<CarImage>()
                    .Table
                    .Where(x => x.CarId == carId)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<IEnumerable<CarImageDto>>(carImages);
            }
            catch (Exception ex)
            {
                // log the exception
                return null;
            }
        }

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
                    return Result.Failure("Failed to start transaction.");
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

                //var targetImage = carImages.FirstOrDefault(ci => ci.ImageUrl == imageUrl);
                //if (targetImage == null)
                //    return Result.Failure($"Image not found. ImageUrl: {imageUrl}, CarId: {carId}");

                foreach (var image in carImages)
                {
                    image.IsMain = false;
                    _unitOfWork.Repository<CarImage>().Update(image);
                }

                //targetImage.IsMain = true;
                //_unitOfWork.Repository<CarImage>().Update(targetImage);

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
                    return Result.Failure("Failed to start a transaction.");
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

        public async Task<CarFieldsDto> GetAllCarFieldsAsync()
        {
            try
            {
                CarFieldsDto carFields = new CarFieldsDto
                {
                    CarModels = await _carModelService.GetAllAsync(),
                    CarBodyTypes = await _carBodyTypeService.GetAllAsync(),
                    CarBrands = await _carBrandService.GetAllAsync(),
                    CarColors = await _carColorService.GetAllAsync(),
                    CarFuelTypes = await _carFuelTypeService.GetAllAsync()
                };

                return carFields;
            }
            catch (Exception)
            {
                // log the exception
                return null;
            }
        }
    }
}
