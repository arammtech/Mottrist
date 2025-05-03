using FluentValidation;
using Mottrist.Service.Features.Users.DTOs;

namespace Mottrist.Service.Features.Users.Validators
{
    /// <summary>
    /// Validator for the <see cref="UserLoginDto"/> class.
    /// Ensures login credentials are valid before processing authentication.
    /// </summary>
    class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
