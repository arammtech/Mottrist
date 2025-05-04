using System.ComponentModel.DataAnnotations;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;

namespace Mottrist.Service.Features.Cars.Validators.Attributes
{
    public class UniqueCarBrandName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Ensure value exists
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult("Car brand name is required.");

            // Retrieve ICarBrandService from DI
            var carBrandService = (ICarBrandService)validationContext.GetService(typeof(ICarBrandService));
            if (carBrandService == null)
                throw new InvalidOperationException("Dependency injection failed for CarBrandService.");

            // Check if brand name already exists
            var brandExists = carBrandService.GetByNameAsync(value.ToString()).Result;

            return brandExists != null
                ? new ValidationResult("Car brand name must be unique.")
                : ValidationResult.Success;
        }
    }
}