using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.User.DTOs;

namespace Mottrist.Service.Features.General.Mapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
           
            CreateMap<CarImage, CarImageDto>().ReverseMap();
           
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

        }
    }
}
