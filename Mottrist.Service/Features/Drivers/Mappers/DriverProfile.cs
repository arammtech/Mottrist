using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Drivers.Mappers
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, AddDriverDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, AddDriverDto>()
                .ForPath(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore()); // Ignore Id during reverse mapping

            CreateMap<Car, AddDriverDto>()
    .ReverseMap();

            CreateMap<CarDto, AddDriverDto>()
                .ReverseMap();

            CreateMap<Driver, UpdateDriverDto>()
            .ReverseMap();

            CreateMap<ApplicationUser, UpdateDriverDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore()); // Ignore Id during reverse mapping

            CreateMap<CarDto, UpdateDriverDto>()
                .ReverseMap();

            CreateMap<DriverDto, Driver>()
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.Country.Name, opt => opt.MapFrom(src => src.Nationality))
                .ReverseMap();

        }
    }
}
