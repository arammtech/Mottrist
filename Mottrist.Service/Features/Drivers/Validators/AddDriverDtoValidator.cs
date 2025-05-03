using FluentValidation;
using Mottrist.Service.Features.Drivers.DTOs;
using Feature.Car.DTOs;
using System.Text.RegularExpressions;

namespace Mottrist.Service.Features.Drivers.Validators
{
    /// <summary>
    /// Validator for AddDriverDto, ensuring correct input when adding a new driver.
    /// Car validation is applied only when HasCar is true.
    /// </summary>
    public class AddDriverDtoValidator : AbstractValidator<AddDriverDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddDriverDtoValidator"/> class.
        /// Configures validation rules for the AddDriverDto object.
        /// </summary>
        public AddDriverDtoValidator()
        {
            // Validate FirstName
            RuleFor(driver => driver.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            // Validate LastName
            RuleFor(driver => driver.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format. Expected format: example@domain.com.");

            // Validate Password
            RuleFor(driver => driver.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            // Validate NationalityId
            RuleFor(driver => driver.NationalityId)
                .GreaterThan(0).WithMessage("Nationality ID must be greater than zero.");

            // Validate Years of Experience
            RuleFor(driver => driver.YearsOfExperience)
                .GreaterThanOrEqualTo((byte)0)
                .WithMessage("Years of experience must be zero or a positive number.");

            // Validate Price Per Hour (must be positive if provided)            // Validate Price Per Hour (must be positive if provided)
            RuleFor(driver => driver.PricePerHour)
                .GreaterThanOrEqualTo(0).WithMessage("Price per hour must be greater or equals than zero.");


            // Validate phone numbers using E.164 format
            RuleFor(driver => driver.PhoneNumber)
                .Matches(new Regex(@"^\+?[1-9]\d{1,14}$"))
                .When(driver => !string.IsNullOrWhiteSpace(driver.PhoneNumber))
                .WithMessage("Invalid phone number format.");

            RuleFor(driver => driver.WhatsAppNumber)
                .Matches(new Regex(@"^\+?[1-9]\d{1,14}$"))
                .When(driver => !string.IsNullOrWhiteSpace(driver.WhatsAppNumber))
                .WithMessage("Invalid WhatsApp number format.");

            // Ensure AvailableFrom is before AvailableTo if both exist
            When(driver => driver.AvailableFrom.HasValue && driver.AvailableTo.HasValue, () =>
            {
                RuleFor(driver => driver.AvailableFrom)
                    .LessThanOrEqualTo(driver => driver.AvailableTo)
                    .WithMessage("AvailableFrom date must be before or equal to AvailableTo date.");
            });

            // Validate image uploads
            RuleFor(driver => driver.LicenseImage)
                .NotNull().WithMessage("License image is required.");

            RuleFor(driver => driver.PassportImage)
                .NotNull().WithMessage("Passport image is required.");

            // Conditional car validation (only if HasCar is true)
            When(driver => driver.HasCar, () =>
            {
                RuleFor(driver => driver.Car)
                    .NotNull().WithMessage("Car details must be provided when HasCar is true.");

                RuleFor(driver => driver.Car!.Year)
                    .NotNull().WithMessage("Car year is required.")
                    .InclusiveBetween(1900, DateTime.UtcNow.Year)
                    .WithMessage($"Car year must be between 1900 and {DateTime.UtcNow.Year}.");

                RuleFor(driver => driver.Car!.NumberOfSeats)
                    .NotNull().WithMessage("Number of seats is required.")
                    .GreaterThan((byte)0).WithMessage("Number of seats must be greater than zero.");

                RuleFor(driver => driver.Car!.BrandId)
                    .NotNull().WithMessage("Brand ID is required.")
                    .GreaterThan(0).WithMessage("Brand ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.Model)
                    .NotNull().WithMessage("Model is required.");

                RuleFor(driver => driver.Car!.ColorId)
                    .NotNull().WithMessage("Color ID is required.")
                    .GreaterThan(0).WithMessage("Color ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.BodyTypeId)
                    .NotNull().WithMessage("Body Type ID is required.")
                    .GreaterThan(0).WithMessage("Body Type ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.FuelTypeId)
                    .NotNull().WithMessage("Fuel Type ID is required.")
                    .GreaterThan(0).WithMessage("Fuel Type ID must be a valid positive number.");

                    RuleFor(driver => driver.Car!.CarImages)
                          .NotNull().WithMessage("Car images are required between 0 to 10");

                When(driver => driver.Car!.CarImages != null, () =>
                {
                    RuleFor(driver => driver.Car!.CarImages!.Count)
                        .ExclusiveBetween(0, 10)
                        .WithMessage("Car images are required. between 0 to 10");
                });

                RuleFor(driver => driver.Car!.HasWiFi)
                    .NotNull().WithMessage("WiFi availability must be specified.");

                RuleFor(driver => driver.Car!.HasAirCondiations)
                    .NotNull().WithMessage("Air conditioning availability must be specified.");
            });
        }
    }
}
