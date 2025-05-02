using FluentValidation;
using Mottrist.Service.Features.Drivers.DTOs;

namespace Mottrist.Service.Features.Drivers.Validators
{
    /// <summary>
    /// Validator for the UpdateDriverDto object to ensure input data is valid during driver updates.
    /// </summary>
    public class UpdateDriverDtoValidator : AbstractValidator<UpdateDriverDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDriverDtoValidator"/> class.
        /// Configures validation rules for the UpdateDriverDto object.
        /// </summary>
        public UpdateDriverDtoValidator()
        {
            // Validate Id
            // Validate FirstName
            RuleFor(driver => driver.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            // Validate LastName
            RuleFor(driver => driver.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(driver => driver.NationalityId)
                .GreaterThan(0).WithMessage("Nationality ID must be greater than 0.");
        }
    }
}
