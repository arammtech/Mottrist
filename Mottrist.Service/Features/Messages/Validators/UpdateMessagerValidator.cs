using FluentValidation;
using Mottrist.Service.Features.Messages.DTOs;

namespace Mottrist.Service.Features.Messages.Validators
{
    /// <summary>
    /// Validator for the <see cref="UpdateMessageDto"/> class.
    /// Ensures that required fields are properly set for updating a message.
    /// </summary>
    public class UpdateMessagerValidator : AbstractValidator<UpdateMessageDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMessagerValidator"/> class.
        /// Defines validation rules for updating a message.
        /// </summary>
        public UpdateMessagerValidator()
        {
            /// <summary>
            /// Validates that <see cref="UpdateMessageDto.Id"/> is not null.
            /// </summary>
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.");

            /// <summary>
            /// Validates that <see cref="UpdateMessageDto.MessageBody"/> is not empty or null.
            /// </summary>
            RuleFor(x => x.MessageBody)
                .NotEmpty().WithMessage("Message body cannot be empty.")
                .NotNull().WithMessage("Message body is required.");
        }
    }
}