using FluentValidation;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;

namespace Feature.Car.Validators
{
    public class UpdateCarBrandDtoValidator : AbstractValidator<UpdateCarBrandDto>
    {
        public UpdateCarBrandDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Car brand cannot be empty.")
                .NotNull().WithMessage("Car name is required.");
        }
    }
}
