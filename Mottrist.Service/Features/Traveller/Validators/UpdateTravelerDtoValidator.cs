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
