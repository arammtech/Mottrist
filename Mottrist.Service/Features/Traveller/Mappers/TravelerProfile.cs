using AutoMapper;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Mappers
{
    public class TravelerProfile : Profile
    {
        public TravelerProfile()
        {
            CreateMap<GetTravelerDto, Traveler>()
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.Country.Id, opt => opt.MapFrom(src => src.NationailtyId))
                .ForPath(dest => dest.Country.Name, opt => opt.MapFrom(src => src.CountryName))
            .ReverseMap();

            CreateMap<AddUpdateTravelerDto, Traveler>().ReverseMap();

            CreateMap<AddUpdateTravelerDto, ApplicationUser>().ReverseMap();
            CreateMap<GetTravelerDto, ApplicationUser>().ReverseMap();

            CreateMap<PaginationTravelerDto, Traveler>().ReverseMap();
        }
    }
}
