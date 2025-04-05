using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;

namespace Mottrist.Service.Features.Cars.Mappers
{
    class CarFieldsProfile : Profile
    {
        public CarFieldsProfile()
        {
            CreateMap<Car, CarModelDto>().ReverseMap();
            CreateMap<Car, CarBodyTypeDto>().ReverseMap();
            CreateMap<AddCarDto, CarBrandDto>().ReverseMap();
            CreateMap<Car, CarFuelTypeDto>().ReverseMap();
            CreateMap<CarImage, CarColorDto>().ReverseMap();
        }
    }
}
