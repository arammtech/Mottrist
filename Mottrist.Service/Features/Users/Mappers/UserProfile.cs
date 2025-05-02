using AutoMapper;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;

namespace Mottrist.Service.Features.Users.Mappers
{
    /// <summary>
    /// AutoMapper profile for mapping User-related entities and DTOs.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// Defines mappings between <see cref="ApplicationUser"/> and DTOs.
        /// </summary>
        public UserProfile()
        {
            /// <summary>
            /// Maps <see cref="ApplicationUser"/> to <see cref="UserDto"/> and vice versa.
            /// </summary>
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            /// <summary>
            /// Maps <see cref="ApplicationUser"/> to <see cref="AddUserDto"/>.
            /// Converts PasswordHash in <see cref="ApplicationUser"/> to Password in <see cref="AddUserDto"/>.
            /// Enables reverse mapping from <see cref="AddUserDto"/> to <see cref="ApplicationUser"/>.
            /// </summary>
            CreateMap<ApplicationUser, AddUserDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();
        }
    }
}