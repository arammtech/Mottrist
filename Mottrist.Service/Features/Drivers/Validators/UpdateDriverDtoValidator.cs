using FluentValidation;
using Mottrist.Service.Features.Drivers.DTOs;
using System.Text.RegularExpressions;

namespace Mottrist.Service.Features.Drivers.Validators
{
    /// <summary>
    /// Validator for the UpdateDriverDto object to ensure valid driver update input data.
    /// </summary>
    public class UpdateDriverDtoValidator : AbstractValidator<UpdateDriverDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDriverDtoValidator"/> class.
        /// Configures validation rules for the UpdateDriverDto object.
        /// </summary>
        public UpdateDriverDtoValidator()
        {
            // Ensure Id is valid
            RuleFor(driver => driver.Id)
                .GreaterThan(0).WithMessage("Driver ID must be a positive number.");

            // Validate FirstName
            RuleFor(driver => driver.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            // Validate LastName
            RuleFor(driver => driver.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            // Validate NationalityId
            RuleFor(driver => driver.NationalityId)
                .GreaterThan(0).WithMessage("Nationality ID must be greater than zero.");

            // Validate PhoneNumber (optional, but if provided, must be a valid number)
            RuleFor(driver => driver.PhoneNumber)
                .Matches(new Regex(@"^\+?[1-9]\d{1,14}$"))
                .When(driver => !string.IsNullOrWhiteSpace(driver.PhoneNumber))
                .WithMessage("Invalid phone number format.");

            // Validate WhatsAppNumber (same rules as PhoneNumber)
            RuleFor(driver => driver.WhatsAppNumber)
                .Matches(new Regex(@"^\+?[1-9]\d{1,14}$"))
                .When(driver => !string.IsNullOrWhiteSpace(driver.WhatsAppNumber))
                .WithMessage("Invalid WhatsApp number format.");

            // Validate Years of Experience
            RuleFor(driver => driver.YearsOfExperience)
                .GreaterThanOrEqualTo((byte)0)
                .WithMessage("Years of experience must be zero or a positive number.");

            // Validate Price Per Hour (must be positive if provided)
            RuleFor(driver => driver.PricePerHour)
                .GreaterThanOrEqualTo(0).WithMessage("Price per hour must be greater or equals than zero.");

            // Conditional validation for AvailableFrom and AvailableTo
            When(driver => driver.AvailableFrom.HasValue && driver.AvailableTo.HasValue, () =>
            {
                RuleFor(driver => driver.AvailableFrom)
                    .LessThanOrEqualTo(driver => driver.AvailableTo)
                    .WithMessage("AvailableFrom date must be before or equal to AvailableTo date.");
            });

            // Ensure ProfileImage is not empty if provided
            RuleFor(driver => driver.ProfileImage)
                .NotNull().When(driver => driver.ProfileImage != null)
                .WithMessage("Profile image file must be valid.");


            // Conditional car validation
            When(driver => driver.Car != null, () =>
            {
                RuleFor(driver => driver.Car!.Year)
                    .InclusiveBetween(1900, DateTime.UtcNow.Year)
                    .WithMessage($"Car year must be between 1900 and {DateTime.UtcNow.Year}.");

                RuleFor(driver => driver.Car!.NumberOfSeats)
                    .GreaterThan((byte)0).WithMessage("Number of seats must be greater than zero.");

                RuleFor(driver => driver.Car!.BrandId)
                    .GreaterThan(0).WithMessage("Brand ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.ModelId)
                    .GreaterThan(0).WithMessage("Model ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.ColorId)
                    .GreaterThan(0).WithMessage("Color ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.BodyTypeId)
                    .GreaterThan(0).WithMessage("Body Type ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.FuelTypeId)
                    .GreaterThan(0).WithMessage("Fuel Type ID must be a valid positive number.");

                RuleFor(driver => driver.Car!.HasWiFi)
                    .NotNull().WithMessage("WiFi availability must be specified.");

                RuleFor(driver => driver.Car!.HasAirCondiations)
                    .NotNull().WithMessage("Air conditioning availability must be specified.");

                When(driver => driver.Car!.CarImages != null, () =>
                {
                    RuleFor(driver => driver.Car!.CarImages!.Count)
                        .ExclusiveBetween(0, 10)
                        .WithMessage("Car images are required. between 0 to 10");
                });
            });

            // Ensure CitiesWorkedOn and CountriesWorkedOn contain valid IDs
            RuleFor(driver => driver.CitiesWorkedOn)
                .Must(cities => cities.All(id => id > 0))
                .When(driver => driver.CitiesWorkedOn.Any())
                .WithMessage("All city IDs in CitiesWorkedOn must be greater than zero.");

            RuleFor(driver => driver.CountriesWorkedOn)
                .Must(countries => countries.All(id => id > 0))
                .When(driver => driver.CountriesWorkedOn.Any())
                .WithMessage("All country IDs in CountriesWorkedOn must be greater than zero.");

            // Ensure CitiesCoverNow and CountriesCoverNow contain valid IDs
            RuleFor(driver => driver.CitiesCoverNow)
                .Must(cities => cities.All(id => id > 0))
                .When(driver => driver.CitiesCoverNow.Any())
                .WithMessage("All city IDs in CitiesCoverNow must be greater than zero.");

            RuleFor(driver => driver.CountriesCoverNow)
                .Must(countries => countries.All(id => id > 0))
                .When(driver => driver.CountriesCoverNow.Any())
                .WithMessage("All country IDs in CountriesCoverNow must be greater than zero.");

            // Ensure LanguagesSpoken contain valid IDs
            RuleFor(driver => driver.LanguagesSpoken)
                .Must(languages => languages.All(id => id > 0))
                .When(driver => driver.LanguagesSpoken.Any())
                .WithMessage("All language IDs in LanguagesSpoken must be greater than zero.");
        }
    }
}
