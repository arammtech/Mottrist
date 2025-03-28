using FluentValidation;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Validators
{
    public class AddTravelerDtoValidator : AbstractValidator<AddTravelerDto>
    {
        public AddTravelerDtoValidator()
        {
            RuleFor(x => x.NationalityId).GreaterThan(0).WithMessage("Nationality is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
                                     .MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
                                    .MinimumLength(2).MaximumLength(50);

            // Email Validation (Required + Valid Format)
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // PhoneNumber Validation (Optional, but must be in correct format if provided)
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$") 
                .WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
