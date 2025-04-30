

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
            CreateMap<Car, CarDto>();
            CreateMap<Car, AddCarDto>();
            CreateMap<Car, UpdateCarDto>();
            CreateMap<CarImage, CarImageDto>();
        }
    }
}
