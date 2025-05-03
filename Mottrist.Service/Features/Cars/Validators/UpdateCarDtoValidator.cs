using FluentValidation;
using Microsoft.AspNetCore.Http;
using Feature.Car.DTOs;

namespace Feature.Car.Validators
{
    /// <summary>
    /// Validator for UpdateCarDto, ensuring proper validation of car details and image requirements.
    /// </summary>
    public class UpdateCarDtoValidator : AbstractValidator<UpdateCarDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCarDtoValidator"/> class.
        /// Configures validation rules for UpdateCarDto.
        /// </summary>
        public UpdateCarDtoValidator()
        {
            // Validate Year (must be a valid car year)
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
            RuleFor(car => car.ModelId).NotNull().GreaterThan(0).WithMessage("Model ID must be a valid positive number.");
            RuleFor(car => car.ColorId).NotNull().GreaterThan(0).WithMessage("Color ID must be a valid positive number.");
            RuleFor(car => car.BodyTypeId).NotNull().GreaterThan(0).WithMessage("Body Type ID must be a valid positive number.");
            RuleFor(car => car.FuelTypeId).NotNull().GreaterThan(0).WithMessage("Fuel Type ID must be a valid positive number.");

            // Validate boolean fields
            RuleFor(car => car.HasWiFi).NotNull().WithMessage("WiFi availability must be specified.");
            RuleFor(car => car.HasAirCondiations).NotNull().WithMessage("Air conditioning availability must be specified.");

            // Validate Car Images (Optional but if provided, must follow constraints)
            When(car => car.CarImages != null && car.CarImages.Count > 0, () =>
            {
                RuleFor(car => car.CarImages!.Count)
                    .InclusiveBetween(1, 10)
                    .WithMessage("The number of car images must be between 1 and 10.");

                RuleForEach(car => car.CarImages)
                    .Must(file => file.Length > 0 && file.Length <= 5 * 1024 * 1024) // Limit to 5MB per image
                    .WithMessage("Each car image must be between 1KB and 5MB.")
                    .Must(file => file.ContentType.StartsWith("image/"))
                    .WithMessage("Only image files are allowed.");
            });
        }
    }
}
