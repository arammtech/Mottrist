using FluentValidation;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Validators
{
    public class UpdateTravelerDtoValidator : AbstractValidator<UpdateTravelerDto>
    {
        public UpdateTravelerDtoValidator()
        {
            RuleFor(x => x.NationalityId)
              .InclusiveBetween(1, 5).WithMessage("Nationality ID is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.ProfileImageUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Invalid image URL format. Expected format: https://example.com/image.jpg")
            .When(x => !string.IsNullOrEmpty(x.ProfileImageUrl));
        }
    }
}
