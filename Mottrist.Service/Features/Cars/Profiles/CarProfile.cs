

using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs;

namespace Mottrist.Service.Features.Cars.Profiles
{
    class CarProfile : Profile
    {
        public CarProfile() 
        {

            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.BodyType))
                .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelType))
                .ForMember(dest => dest.CarImageUrls, opt => opt.MapFrom(src => src.CarImages));

            CreateMap<CarImage, CarImageDto>();

            CreateMap<AddCarDto, Car>()
                .ForMember(dest => dest.CarImages, opt => opt.Ignore());

            CreateMap<UpdateCarDto, Car>()
                .ForMember(dest => dest.CarImages, opt => opt.Ignore());

            CreateMap<UpdateCarDto, AddCarDto>();

        }
    }
}
