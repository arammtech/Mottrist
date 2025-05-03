using FluentValidation;
using Mottrist.Service.Features.Destinations.DTOs;
using Microsoft.AspNetCore.Http;

namespace Mottrist.Service.Features.Destinations.Validators
{
    /// <summary>
    /// Validator for AddDestinationDto, ensuring correct input when adding a new destination.
    /// </summary>
    public class AddDestinationDtoValidator : AbstractValidator<AddDestinationDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddDestinationDtoValidator"/> class.
        /// Configures validation rules for AddDestinationDto.
        /// </summary>
        public AddDestinationDtoValidator()
        {
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

            // Validate Image (must be a valid file and an image)
            RuleFor(destination => destination.Image)
                .NotNull().WithMessage("An image is required for the destination.")
                .Must(file => file.Length > 0 && file.Length <= 5 * 1024 * 1024) // Max 5MB size
                .WithMessage("Image file must be between 1KB and 5MB.");
            // Optional Description Validation
            RuleFor(destination => destination.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
