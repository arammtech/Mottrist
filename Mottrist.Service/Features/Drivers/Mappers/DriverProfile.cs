using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;

namespace Mottrist.Service.Features.Drivers.Mappers
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, AddDriverDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, AddDriverDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore()) // Ignore Id during reverse mapping
                .ForMember(src => src.PasswordHash, opt => opt.MapFrom(dest => dest.Password));


            CreateMap<Car, AddDriverDto>()
                .ReverseMap();

            CreateMap<AddCarDto, AddDriverDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore());

            CreateMap<Driver, UpdateDriverDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, UpdateDriverDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore()); // Ignore Id during reverse mapping

            CreateMap<AddCarDto, UpdateDriverDto>()
                .ReverseMap();

            CreateMap<Driver,DriverDto>()
                .ForPath(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForPath(dest => dest.Nationality, opt => opt.MapFrom(src => src.Country.Name))
                .ReverseMap();

        }
    }
}
