using FluentValidation;
using Microsoft.AspNetCore.Http;
using Feature.Car.DTOs;

namespace Feature.Car.Validators
{
    /// <summary>
    /// Validator for AddCarDto, ensuring proper validation of car details and image requirements.
    /// </summary>
    public class AddCarDtoValidator : AbstractValidator<AddCarDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCarDtoValidator"/> class.
        /// Configures validation rules for AddCarDto.
        /// </summary>
        public AddCarDtoValidator()
        {
            RuleFor(car => car.Year)
                .NotNull().WithMessage("Car year is required.")
                .InclusiveBetween(1900, DateTime.UtcNow.Year)
                .WithMessage($"Car year must be between 1900 and {DateTime.UtcNow.Year}.");

            // Validate NumberOfSeats
            RuleFor(car => car.NumberOfSeats)
                .NotNull().WithMessage("Number of seats is required.")
                .GreaterThan((byte)0).WithMessage("Number of seats must be greater than zero.");

            // Validate Brand, Model, Color, BodyType, FuelType
            RuleFor(car => car.BrandId).NotNull().GreaterThan(0).WithMessage("Brand ID must be a valid positive number.");
            RuleFor(car => car.Model).NotNull();
            RuleFor(car => car.ColorId).NotNull().GreaterThan(0).WithMessage("Color ID must be a valid positive number.");
            RuleFor(car => car.BodyTypeId).NotNull().GreaterThan(0).WithMessage("Body Type ID must be a valid positive number.");
            RuleFor(car => car.FuelTypeId).NotNull().GreaterThan(0).WithMessage("Fuel Type ID must be a valid positive number.");

            // Validate boolean fields
            RuleFor(car => car.HasWiFi).NotNull().WithMessage("WiFi availability must be specified.");
            RuleFor(car => car.HasAirCondiations).NotNull().WithMessage("Air conditioning availability must be specified.");

            RuleFor(car => car.CarImages)
                .NotNull().WithMessage("At least one car image is required.")
                .Must(images => images is not null && images.Count is >= 1 and <= 10)
                .WithMessage("You must upload at least one car image and no more than ten.");

            // Validate each image format and size
            RuleForEach(car => car.CarImages)
                .Must(file => file.Length > 0 && file.Length <= 5 * 1024 * 1024) // Limit to 5MB per image
                .WithMessage("Each car image must be between 1KB and 5MB.");
        }
    }
}
