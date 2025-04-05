using AutoMapper;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Languages.DTOs;

namespace Mottrist.Service.Features.Languages.Mappers
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile() 
        {
            CreateMap<Language, LanguageDto>().ReverseMap();

        }
    }
}
