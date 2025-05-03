using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;

namespace Mottrist.Service.Features.Cars.Profiles
{
    class CarFieldsProfile : Profile
    {
        public CarFieldsProfile()
        {
            CreateMap<BodyType, CarBodyTypeDto>().ReverseMap();
            CreateMap<Brand, CarBrandDto>().ReverseMap();
            CreateMap<FuelType, CarFuelTypeDto>().ReverseMap();
            CreateMap<Color, CarColorDto>().ReverseMap();
        }
    }
}
