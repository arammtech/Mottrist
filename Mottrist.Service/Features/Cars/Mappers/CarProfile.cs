

using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs;

namespace Mottrist.Service.Features.Cars.Mappers
{
    class CarProfile : Profile
    {
        public CarProfile() 
        {
            CreateMap<Car, AddCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<CarImage, CarImageDto>().ReverseMap();

        }
    }
}
