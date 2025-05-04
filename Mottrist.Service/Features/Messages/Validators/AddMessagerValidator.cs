using FluentValidation;
using Mottrist.Service.Features.Messages.DTOs;

namespace Mottrist.Service.Features.Messages.Validators
{
    /// <summary>
    /// Validator for the <see cref="AddMessageDto"/> class.
    /// Ensures that required fields are properly set.
    /// </summary>
    public class AddMessagerValidator : AbstractValidator<AddMessageDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMessagerValidator"/> class.
        /// Defines validation rules for adding a message.
        /// </summary>
        public AddMessagerValidator()
        {
            /// <summary>
            /// Validates that <see cref="AddMessageDto.MessageBody"/> is not empty or null and less than 500 char.
            /// </summary>
            RuleFor(x => x.MessageBody)
                .NotEmpty().WithMessage("Message body cannot be empty.")
                .NotNull().WithMessage("Message body is required.")
                .MaximumLength(500).WithMessage("Message body cannot exceed 500 characters.");
        }
    }
}