using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cities.Dtos;
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



            CreateMap<AddDriverDto, Driver>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Car, opt => opt.Ignore())
                .ForMember(dest => dest.CarId, opt => opt.Ignore())
                .ForMember(dest => dest.LicenseImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.PassportImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.DriverCities, opt => opt.MapFrom(x =>
                    x.CitiesCoverNow.Select(cityId => new DriverCity { CityId = cityId, WorkStatus = WorkStatus.CoverNow })
                    .Concat(x.CitiesWorkedOn.Select(cityId => new DriverCity { CityId = cityId, WorkStatus = WorkStatus.WorkedOn }))
                ))
                .ForMember(dest => dest.DriverCountries, opt => opt.MapFrom(x =>
                    x.CountriesCoverNow.Select(countryId => new DriverCountry { CountryId = countryId, WorkStatus = WorkStatus.CoverNow })
                    .Concat(x.CountriesWorkedOn.Select(countryId => new DriverCountry { CountryId = countryId, WorkStatus = WorkStatus.WorkedOn }))
                ))
                .ForMember(dest => dest.DriverLanguages, opt => opt.MapFrom(x => x.LanguagesSpoken.Select(langId => new DriverLanguage { LanguageId = langId })));


            CreateMap<AddCarDto, Car>();

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
