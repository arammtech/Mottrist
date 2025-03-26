using AutoMapper;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.General.Mapper.Profiles
{
    public class TravelerProfile : Profile
    {
        public TravelerProfile()
        {
            CreateMap<AddUpdateTravelerDto, Traveler>().ReverseMap();
            CreateMap<AddUpdateTravelerDto, ApplicationUser>().ReverseMap();
        }
    }
}
