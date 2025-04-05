using AutoMapper;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Countries.DTOs;


namespace Mottrist.Service.Features.Countries.Mappers
{
    /// <summary>
    /// This class defines the mapping profile for converting between 
    /// the <see cref="Country"/> entity and the <see cref="GetCountryDto"/>.
    /// It inherits from AutoMapper's <see cref="Profile"/> class to configure the mappings.
    /// </summary>
    public class CountryProfile : Profile
    {
        /// <summary>
        /// Constructor for the <see cref="CountryProfile"/> class.
        /// It sets up the mappings between <see cref="Country"/> and <see cref="GetCountryDto"/>.
        /// </summary>
        public CountryProfile()
        {
            /// <summary>
            /// Creates a mapping configuration between the <see cref="Country"/> entity 
            /// and the <see cref="GetCountryDto"/> Data Transfer Object (DTO).
            /// The reverse mapping allows mapping from <see cref="GetCountryDto"/> back to <see cref="Country"/>.
            /// </summary>
            CreateMap<Country, GetCountryDto>()
                .ReverseMap(); 
        }
    }
}