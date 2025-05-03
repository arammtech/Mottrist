using FluentValidation;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Validators
{
    /// <summary>
    /// Validator for updating a traveler.
    /// Ensures that the provided data adheres to specific validation rules.
    /// </summary>
    public class UpdateTravelerDtoValidator : AbstractValidator<UpdateTravelerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTravelerDtoValidator"/> class.
        /// Defines validation rules for updating traveler data.
        /// </summary>
        public UpdateTravelerDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Traveler ID must be a positive number.");
            // Valid NationalityId between 1 to 5.
            RuleFor(x => x.NationalityId)
                .InclusiveBetween(1, 5)
                .WithMessage("Nationality ID is required and must be between 1 and 5.");

            // First name: required, with minimum and maximum length constraints.
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            // Last name: required, with minimum and maximum length constraints.
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            // Optional PhoneNumber: if provided, must match the given international phone number format.
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid phone number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

            // Optional WhatsAppNumber: if provided, must match the international phone format.
            RuleFor(x => x.WhatsAppNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid WhatsApp number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrWhiteSpace(x.WhatsAppNumber));

            // Additional rules can be added here for the ProfileImage or PreferredLanguageId if needed.
        }
    }
}
