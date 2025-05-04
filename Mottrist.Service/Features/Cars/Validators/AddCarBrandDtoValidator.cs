using FluentValidation;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;

namespace Feature.Car.Validators
{
    public class AddCarBrandDtoValidator : AbstractValidator<AddCarBrandDto>
    {
        public AddCarBrandDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Car brand cannot be empty.")
                .NotNull().WithMessage("Car name is required.");
        }
    }
}
