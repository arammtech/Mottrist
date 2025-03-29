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
        }
    }
}
