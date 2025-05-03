using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Traveller.Validators
{
    /// <summary>
    /// Validator for adding a new traveler.
    /// Ensures required fields are provided and meet validation criteria.
    /// </summary>
    public class AddTravelerDtoValidator : AbstractValidator<AddTravelerDto>
    {
        /// <summary>
        /// Initializes validation rules for AddTravelerDto.
        /// </summary>
        public AddTravelerDtoValidator()
        {

            // NationalityId must be between 1 and 5
            RuleFor(x => x.NationalityId)
                .InclusiveBetween(1, 5)
                .WithMessage("Nationality ID is required and must be between 1 and 5.");

            // First name: required, minimum 2 and maximum 50 characters
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            // Last name: required, minimum 2 and maximum 50 characters
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            // Email: required, valid email format
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format. Expected format: example@domain.com.");

            // PhoneNumber: optional but must match phone regex if provided
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid phone number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            // WhatsAppNumber: optional but must match phone regex if provided
            RuleFor(x => x.WhatsAppNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid WhatsApp number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrEmpty(x.WhatsAppNumber));

            // Password: required, at least 8 characters, one uppercase, one lowercase, one digit, and one special character.
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
        }
    }
}
