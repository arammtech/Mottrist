using FluentValidation;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Validators
{
    public class AddTravelerDtoValidator : AbstractValidator<AddTravelerDto>
    {
        public AddTravelerDtoValidator()
        {
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

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Invalid phone number format. Expected format: +12345678901 (10-15 digits).")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.PasswordHash)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.")
                .When(x => !string.IsNullOrEmpty(x.PasswordHash));

            RuleFor(x => x.ProfileImageUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid image URL format. Expected format: https://example.com/image.jpg")
                .When(x => !string.IsNullOrEmpty(x.ProfileImageUrl));
        }
    }
}
