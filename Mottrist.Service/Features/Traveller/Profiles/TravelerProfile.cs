﻿using AutoMapper;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Profiles
{
    /// <summary>
    /// AutoMapper profile for Traveler mappings.
    /// </summary>
    public class TravelerProfile : Profile
    {
        public TravelerProfile()
        {
            // Mapping Traveler to TravelerDto
            CreateMap<Traveler, TravelerDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            // Mapping DTOs to Traveler entity
            CreateMap<AddTravelerDto, Traveler>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PreferredLanguage, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<UpdateTravelerDto, Traveler>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PreferredLanguage, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());

            // Mapping DTOs to ApplicationUser entity
            CreateMap<AddTravelerDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<UpdateTravelerDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<TravelerDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(src => src.Id, opt => opt.Ignore());

        }
    }
}