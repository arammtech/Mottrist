using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Cars.Interfaces;
using Feature.Car.DTOs;

namespace Mottrist.Service.UnitTests.Features.Cars
{
    public class CarServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ICarService _carService;

        public CarServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _carService = new CarService(_mockUnitOfWork.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnSuccess_WhenCarIsCreated()
        {
            // Arrange
            var carDto = new CarDto { BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };
            var carEntity = new Car { BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };

            _mockMapper.Setup(m => m.Map<Car>(carDto)).Returns(carEntity);
            _mockUnitOfWork.Setup(u => u.Repository<Car>().AddAsync(carEntity)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.AddAsync(carDto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockMapper.Verify(m => m.Map<Car>(carDto), Times.Once);
            _mockUnitOfWork.Verify(u => u.Repository<Car>().AddAsync(carEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // READ TEST CASE
        [Fact]
        public async Task GetCarByIdAsync_ShouldReturnCarDto_WhenCarExists()
        {
            // Arrange
            const int carId = 1;
            var carEntity = new Car { Id = carId, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };
            var carDto = new CarDto { Id = carId, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };

            _mockUnitOfWork.Setup(u => u.Repository<Car>().GetAsync(c => c.Id == carId)).ReturnsAsync(carEntity);
            _mockMapper.Setup(m => m.Map<CarDto>(carEntity)).Returns(carDto);

            // Act
            var result = await _carService.GetByIdAsync(carId);

            // Assert
            Assert.Equal(carId, result.Id);
            Assert.NotNull(result);
            _mockMapper.Verify(m => m.Map<CarDto>(carEntity), Times.Once);
        }

        // UPDATE TEST CASE
        [Fact]
        public async Task UpdateCarAsync_ShouldReturnSuccess_WhenCarIsUpdated()
        {
            // Arrange
            var carDto = new CarDto { Id = 1, BrandId = 2, Year = 2023, NumberOfSeats = 5, ModelId = 1, ColorId = 2, BodyTypeId = 3, FuelTypeId = 1 };
            var carEntity = new Car { Id = 1, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };

            _mockUnitOfWork.Setup(u => u.Repository<Car>().GetAsync(c => c.Id == carDto.Id)).ReturnsAsync(carEntity);
            _mockMapper.Setup(m => m.Map(carDto, carEntity)).Returns(carEntity);
            _mockUnitOfWork.Setup(u => u.Repository<Car>().Update(carEntity));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.UpdateAsync(carDto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockMapper.Verify(m => m.Map(carDto, carEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.Repository<Car>().Update(carEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // DELETE TEST CASE
        [Fact]
        public async Task DeleteCarAsync_ShouldReturnSuccess_WhenCarIsDeleted()
        {
            // Arrange
            const int carId = 1;
            var carEntity = new Car { Id = carId, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 };

            _mockUnitOfWork.Setup(u => u.Repository<Car>().GetAsync(c => c.Id == carId)).ReturnsAsync(carEntity);
            _mockUnitOfWork.Setup(u => u.Repository<Car>().Delete(carEntity));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.DeleteAsync(carId);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUnitOfWork.Verify(u => u.Repository<Car>().Delete(carEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // NEGATIVE TEST CASE - READ NON-EXISTENT
        [Fact]
        public async Task GetCarByIdAsync_ShouldReturnNull_WhenCarDoesNotExist()
        {
            // Arrange
            const int carId = 99;
            _mockUnitOfWork.Setup(u => u.Repository<Car>().GetAsync(c => c.Id == carId)).ReturnsAsync((Car)null);

            // Act
            var result = await _carService.GetByIdAsync(carId);

            // Assert
            Assert.Null(result);
        }

        // Test for AddCarImageAsync
        [Fact]
        public async Task AddCarImageAsync_ShouldReturnSuccess_WhenCarImageIsAdded()
        {
            // Arrange
            var carImageDto = new CarImageDto { ImageUrl = "image1.jpg", CarId = 1 };
            var carImage = new CarImage { ImageUrl = "image1.jpg", CarId = 1 };

            _mockMapper.Setup(m => m.Map<CarImage>(carImageDto)).Returns(carImage);
            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().AddAsync(carImage)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.AddCarImageAsync(carImageDto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockMapper.Verify(m => m.Map<CarImage>(carImageDto), Times.Once);
            _mockUnitOfWork.Verify(u => u.Repository<CarImage>().AddAsync(carImage), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // Test for RemoveCarImageAsync
        [Fact]
        public async Task RemoveCarImageAsync_ShouldReturnSuccess_WhenCarImageIsRemoved()
        {
            // Arrange
            const string imageUrl = "image1.jpg";
            const int carId = 1;
            var carImage = new CarImage { ImageUrl = imageUrl, CarId = carId };

            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().GetAsync(ci => ci.ImageUrl == imageUrl && ci.CarId == carId))
                           .ReturnsAsync(carImage);
            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().Delete(carImage));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.RemoveCarImageAsync(imageUrl, carId);

            // Assert
            Assert.True(result.IsSuccess);
        }

        // Test for SetMainImageAsync
        [Fact]
        public async Task SetMainImageAsync_ShouldReturnSuccess_WhenMainImageIsSet()
        {
            // Arrange
            const int carId = 1;
            const string imageUrl = "image1.jpg";
            var carImages = new List<CarImage>
            {
                new CarImage { ImageUrl = "image1.jpg", CarId = carId, IsMain = false },
                new CarImage { ImageUrl = "image2.jpg", CarId = carId, IsMain = true }
            };

            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().GetAllAsync(ci => ci.CarId == carId))
                           .ReturnsAsync(carImages);
            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().Update(It.IsAny<CarImage>()));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(Result.Success());

            // Act
            var result = await _carService.SetMainImageAsync(carId, imageUrl);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // Negative test for SetMainImageAsync
        [Fact]
        public async Task SetMainImageAsync_ShouldReturnFailure_WhenImageNotFound()
        {
            // Arrange
            const int carId = 1;
            const string imageUrl = "invalid.jpg";
            var carImages = new List<CarImage>
            {
                new CarImage { ImageUrl = "image1.jpg", CarId = carId, IsMain = false },
                new CarImage { ImageUrl = "image2.jpg", CarId = carId, IsMain = true }
            };

            _mockUnitOfWork.Setup(u => u.Repository<CarImage>().GetAllAsync(ci => ci.CarId == carId))
                           .ReturnsAsync(carImages);

            // Act
            var result = await _carService.SetMainImageAsync(carId, imageUrl);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Image not found", result.Errors.FirstOrDefault());
        }
    }
}
