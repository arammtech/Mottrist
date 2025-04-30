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
                .ForMember(dest => dest.SpokenLanguages, opt => opt.MapFrom(src => src.DriverLanguages.Select(dl => dl.Language)));
           
            CreateMap<AddDriverDto, Driver>();
            
            CreateMap<UpdateDriverDto, Driver>();
        }
    }
}
