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
            CreateMap<Driver, AddUpdateDriverDto>().ReverseMap();
            CreateMap<ApplicationUser, AddUpdateDriverDto>().ReverseMap();
            CreateMap<Car, AddUpdateDriverDto>().ReverseMap();
        }
    }
}
