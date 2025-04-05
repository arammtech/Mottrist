using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Validators
{
    /// <summary>
    /// Validator for adding a new traveler.
    /// Ensures required fields are provided and meet validation criteria.
    /// </summary>
    public class AddTravelerDtoValidator : AbstractValidator<AddTravelerDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;


        /// <summary>
        /// Initializes validation rules for AddTravelerDto.
        /// </summary>
        public AddTravelerDtoValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.NationalityId)
                .InclusiveBetween(1, 5).WithMessage("Nationality ID is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email is required.")
                  .EmailAddress().WithMessage("Invalid email format. Expected format: example@domain.com");
                    //.MustAsync(EmailNotTaken).WithMessage("Email is already taken.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid phone number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.")

        }

        private async Task<bool> EmailNotTaken(string email, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;  
        }
    }
}
