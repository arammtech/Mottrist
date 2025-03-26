using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Service.Features.General.Mapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();

            CreateMap<CarImage, CarImageDto>().ReverseMap();
        }
    }
}
