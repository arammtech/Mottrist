using Mottrist.Service.Features.Cars.Validators.Attributes;

namespace Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs
{
    public class UpdateCarBrandDto
    {
        public int Id { get; set; }

        [UniqueCarBrandName]
        public string Name { get; set; } = null!;
    }
}
