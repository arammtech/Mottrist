using AutoMapper;
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
            // Mapping Driver entity to AddUpdateDriverDto
            CreateMap<Driver, AddUpdateDriverDto>()
                .ForMember(destinationMember => destinationMember.Id, opt => opt.MapFrom(sourceMember => sourceMember.Id))
                .ForMember(destinationMember => destinationMember.WhatsAppNumber, opt => opt.MapFrom(sourceMember => sourceMember.WhatsAppNumber))
                .ForMember(destinationMember => destinationMember.NationailtyId, opt => opt.MapFrom(sourceMember => sourceMember.NationailtyId))
                .ForMember(destinationMember => destinationMember.LicenseImageUrl, opt => opt.MapFrom(sourceMember => sourceMember.LicenseImageUrl))
                .ForMember(destinationMember => destinationMember.YearsOfExperience, opt => opt.MapFrom(sourceMember => sourceMember.YearsOfExperience))
                .ForMember(destinationMember => destinationMember.Bio, opt => opt.MapFrom(sourceMember => sourceMember.Bio))
                .ForMember(destinationMember => destinationMember.PassportImageUrl, opt => opt.MapFrom(sourceMember => sourceMember.PassportImageUrl))
                .ReverseMap();


            CreateMap<ApplicationUser, AddUpdateDriverDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email,  opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .ReverseMap();


            // Mapping Car entity to AddUpdateDriverDto
            CreateMap<Car, AddUpdateDriverDto>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.NumberOfSeats, opt => opt.MapFrom(src => src.NumberOfSeats))
                .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.ModelId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.ColorId))
                .ForMember(dest => dest.BodyTypeId, opt => opt.MapFrom(src => src.BodyTypeId))
                .ForMember(dest => dest.FuelTypeId, opt => opt.MapFrom(src => src.FuelTypeId))
                .ReverseMap();
        }
    }
}
