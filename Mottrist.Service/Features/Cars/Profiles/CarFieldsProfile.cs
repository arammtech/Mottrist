using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs;

namespace Mottrist.Service.Features.Cars.Profiles
{
    class CarFieldsProfile : Profile
    {
        public CarFieldsProfile()
        {
            CreateMap<BodyType, CarBodyTypeDto>().ReverseMap();

            #region Brand
            CreateMap<Brand, CarBrandDto>().ReverseMap();
            CreateMap<AddCarBrandDto, Brand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateCarBrandDto, Brand>();

            #endregion

            CreateMap<FuelType, CarFuelTypeDto>().ReverseMap();
            CreateMap<Color, CarColorDto>().ReverseMap();
        }
    }
}
