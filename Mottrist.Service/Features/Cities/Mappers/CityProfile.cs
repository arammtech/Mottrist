using AutoMapper;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cities.Dtos;

namespace Mottrist.Service.Features.Cities.Mappers
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
    
}
