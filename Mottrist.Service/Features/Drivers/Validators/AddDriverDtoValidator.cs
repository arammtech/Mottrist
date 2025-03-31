using FluentValidation;
using Mottrist.Service.Features.Drivers.DTOs;

namespace Mottrist.Service.Features.Drivers.Validators
{
    /// <summary>
    /// Validator for the AddDriverDto object to ensure input data is valid.
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
            RuleFor(driver => driver.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validate Password
            RuleFor(driver => driver.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            // Validate NationalityId
            RuleFor(driver => driver.NationalityId)
                .GreaterThan(0).WithMessage("Nationality ID must be greater than 0.");

            // Validate LicenseImageUrl
            RuleFor(driver => driver.LicenseImageUrl)
                .NotEmpty().WithMessage("License image URL is required.");

            // Validate PassportImageUrl
            RuleFor(driver => driver.PassportImageUrl)
                .NotEmpty().WithMessage("Passport image URL is required.");

            //// Optional Fields
            //RuleFor(driver => driver.PhoneNumber)
            //    .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.")
            //    .Matches(@"^\+?[1-9]\d{1,14}$").When(driver => !string.IsNullOrEmpty(driver.PhoneNumber))
            //    .WithMessage("Invalid phone number format.");

            //RuleFor(driver => driver.WhatsAppNumber)
            //    .MaximumLength(15).WithMessage("WhatsApp number cannot exceed 15 characters.")
            //    .Matches(@"^\+?[1-9]\d{1,14}$").When(driver => !string.IsNullOrEmpty(driver.WhatsAppNumber))
            //    .WithMessage("Invalid WhatsApp number format.");

            // Validate HasCar and related car details
            RuleFor(driver => driver.HasCar)
                .Must(hasCar => hasCar == true || hasCar == false)
                .WithMessage("HasCar must be a valid boolean value.");

            When(driver => driver.HasCar, () =>
            {
                RuleFor(driver => driver.BrandId)
                    .NotNull().WithMessage("Brand ID is required when the driver has a car.");

                RuleFor(driver => driver.ModelId)
                    .NotNull().WithMessage("Model ID is required when the driver has a car.");

                //RuleFor(driver => driver.Year)
                //    .NotNull().WithMessage("Car year is required when the driver has a car.")
                //    .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Car year must be between 1900 and {DateTime.Now.Year}.");

                RuleFor(driver => driver.NumberOfSeats)
                    .NotNull().WithMessage("Number of seats is required when the driver has a car.");

                RuleFor(driver => driver.MainCarImageIndex)
                    .NotEmpty().WithMessage("Car image URL is required when the driver has a car.");
            });
        }
    }
}
