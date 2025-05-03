using FluentValidation;
using Microsoft.AspNetCore.Http;
using Mottrist.Service.Features.Destinations.DTOs;

namespace Mottrist.Service.Features.Destinations.Validators
{
    /// <summary>
    /// Validator for UpdateDestinationDto, ensuring correct input during destination updates.
    /// </summary>
    public class UpdateDestinationDtoValidator : AbstractValidator<UpdateDestinationDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDestinationDtoValidator"/> class.
        /// Configures validation rules for UpdateDestinationDto.
        /// </summary>
        public UpdateDestinationDtoValidator()
        {
            // Validate Id
            RuleFor(destination => destination.Id)
                .GreaterThan(0).WithMessage("Destination ID must be a valid positive number.");

            // Validate Name
            RuleFor(destination => destination.Name)
                .NotEmpty().WithMessage("Destination name is required.")
                .MaximumLength(100).WithMessage("Destination name cannot exceed 100 characters.");

            // Validate CityId
            RuleFor(destination => destination.CityId)
                .GreaterThan(0).WithMessage("City ID must be a valid positive number.");

            // Validate Type
            RuleFor(destination => destination.Type)
                .NotEmpty().WithMessage("Destination type is required.")
                .MaximumLength(50).WithMessage("Destination type cannot exceed 50 characters.");

            // Validate Image (if provided, must be a valid file and an image)
            When(destination => destination.Image != null, () =>
            {
                RuleFor(destination => destination.Image!)
                    .Must(file => file.Length > 0 && file.Length <= 5 * 1024 * 1024) // Max 5MB size
                    .WithMessage("Image file must be between 1KB and 5MB.");
            });

            // Optional Description Validation
            RuleFor(destination => destination.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
