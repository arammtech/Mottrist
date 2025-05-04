using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Users.Validators
{

    /// <summary>
    /// Validator for the <see cref="AddUserDto"/> class.
    /// Ensures user data meets required constraints before processing.
    /// </summary>
    public class AddUserDtoValidator : AbstractValidator<AddUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AddUserDtoValidator()
        {

            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First name is required.")
           .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");


            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Phone number must be in E.164 format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.WhatsappNumber)
                .NotEmpty().WithMessage("WhatsApp number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("WhatsApp number must be in E.164 format.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Roles list must not be null.")
                .Must(roles => roles.Count > 0).WithMessage("At least one role must be assigned.");

        }
    }
}
