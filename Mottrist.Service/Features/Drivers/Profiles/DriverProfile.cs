using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;

namespace Mottrist.Service.Features.Drivers.Profiles
{
    public class DriverProfile : Profile
    {

        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.CitiesWorkedOn, opt => opt.MapFrom(src => src.DriverCities.Where(dc => dc.WorkStatus == WorkStatus.WorkedOn).Select(dc => dc.City)))
                .ForMember(dest => dest.CitiesCoverNow, opt => opt.MapFrom(src => src.DriverCities.Where(dc => dc.WorkStatus == WorkStatus.CoverNow).Select(dc => dc.City)))
                .ForMember(dest => dest.CountriesWorkedOn, opt => opt.MapFrom(src => src.DriverCountries.Where(dc => dc.WorkStatus == WorkStatus.WorkedOn).Select(dc => dc.Country)))
                .ForMember(dest => dest.CountriesCoverNow, opt => opt.MapFrom(src => src.DriverCountries.Where(dc => dc.WorkStatus == WorkStatus.CoverNow).Select(dc => dc.Country)))
                .ForMember(dest => dest.SpokenLanguages, opt => opt.MapFrom(src => src.DriverLanguages.Select(dl => dl.Language)))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.DriverInteractions.Count(di => di.IsLiked == true)))
                .ForMember(dest => dest.Dislikes, opt => opt.MapFrom(src => src.DriverInteractions.Count(di => di.IsLiked != true)))
                .ForMember(dest => dest.NumberOfViews, opt => opt.MapFrom(src => src.DriverInteractions.Where(di => di.DriverId == src.Id).Sum(di => di.ViewsCount)))
                .ForMember(dest => dest.Natioanlity, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));




            CreateMap<AddDriverDto, Driver>();
            CreateMap<AddDriverDto, AddCarDto>();
            CreateMap<AddDriverDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<UpdateDriverDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateDriverDto, Driver>();
            CreateMap<UpdateDriverDto, UpdateCarDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Car.Id));

        }
    }
}
