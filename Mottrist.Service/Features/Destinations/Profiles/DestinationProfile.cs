using AutoMapper;
using Mottrist.Domain.Entities;
using Mottrist.Service.Features.Destinations.DTOs;

namespace Mottrist.Service.Features.Destinations.Profiles
{
    public class DestinationProfile : Profile
    {
        public DestinationProfile()
        {
            CreateMap<Destination, DestinationDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country))
                .ReverseMap();

            CreateMap<AddDestinationDto, Destination>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<UpdateDestinationDto, Destination>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());
        }
    }
}
