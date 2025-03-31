using FluentValidation;
using Mottrist.Service.Features.Drivers.DTOs;

namespace Mottrist.Service.Features.Drivers.Validators
{
    /// <summary>
    /// Validator for the DriverDto object to ensure input data is valid for driver-related operations.
    /// </summary>
    public class DriverDtoValidator : AbstractValidator<DriverDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DriverDtoValidator"/> class.
        /// Configures validation rules for the DriverDto object.
        /// </summary>
        public DriverDtoValidator()
        {
            // Validate Id
            RuleFor(driver => driver.Id)
                .GreaterThan(0).WithMessage("Driver ID must be greater than 0.");

            // Validate FirstName
            RuleFor(driver => driver.FirstName)
                .NotEmpty().WithMessage("First name is required.");
            // Validate LastName
            RuleFor(driver => driver.LastName)
                .NotEmpty().WithMessage("Last name is required.");
            // Validate Email
            RuleFor(driver => driver.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            //// Optional Fields
            //RuleFor(driver => driver.PhoneNumber)
            //    .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.")
            //    .Matches(@"^\+?[1-9]\d{1,14}$").When(driver => !string.IsNullOrEmpty(driver.PhoneNumber))
            //    .WithMessage("Invalid phone number format.");

            //RuleFor(driver => driver.WhatsAppNumber)
            //    .MaximumLength(15).WithMessage("WhatsApp number cannot exceed 15 characters.")
            //    .Matches(@"^\+?[1-9]\d{1,14}$").When(driver => !string.IsNullOrEmpty(driver.WhatsAppNumber))
            //    .WithMessage("Invalid WhatsApp number format.");

            // Validate Nationality
            RuleFor(driver => driver.Nationality)
                .NotEmpty().WithMessage("Nationality is required.")
                .MaximumLength(100).WithMessage("Nationality cannot exceed 100 characters.");

            // Validate LicenseImageUrl
            RuleFor(driver => driver.LicenseImageUrl)
                .NotEmpty().WithMessage("License image URL is required.");

            // Validate PassportImageUrl
            RuleFor(driver => driver.PassportImageUrl)
                .NotEmpty().WithMessage("Passport image URL is required.");

            // Validate HasCar and related car details
            RuleFor(driver => driver.HasCar)
                .Must(hasCar => hasCar == true || hasCar == false)
                .WithMessage("HasCar must be a valid boolean value.");

            When(driver => driver.HasCar, () =>
            {
                RuleFor(driver => driver.CarBrand)
                    .NotEmpty().WithMessage("Car brand is required when the driver has a car.")
                    .MaximumLength(100).WithMessage("Car brand cannot exceed 100 characters.");

                RuleFor(driver => driver.CarModel)
                    .NotEmpty().WithMessage("Car model is required when the driver has a car.")
                    .MaximumLength(100).WithMessage("Car model cannot exceed 100 characters.");

                RuleFor(driver => driver.CarYear)
                    .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Car year must be between 1900 and {DateTime.Now.Year}.");


                RuleFor(driver => driver.CarColor)
                    .NotEmpty().WithMessage("Car color is required when the driver has a car.")
                    .MaximumLength(50).WithMessage("Car color cannot exceed 50 characters.");

                RuleFor(driver => driver.CarBodyType)
                    .NotEmpty().WithMessage("Car body type is required when the driver has a car.")
                    .MaximumLength(50).WithMessage("Car body type cannot exceed 50 characters.");

                RuleFor(driver => driver.CarFuelType)
                    .NotEmpty().WithMessage("Car fuel type is required when the driver has a car.")
                    .MaximumLength(50).WithMessage("Car fuel type cannot exceed 50 characters.");

            });
        }
    }
}
