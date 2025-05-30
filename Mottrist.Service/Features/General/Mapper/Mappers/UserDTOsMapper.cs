﻿using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;
using static Mottrist.Service.Features.Users.UserGenerate;

namespace Mottrist.Service.Features.General.Mapper.Mappers
{
    public class UserDTOsMapper
    {

        public static async Task<UserDto> ToUserDto(ApplicationUser user, Task<IList<string>> roles)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public static ApplicationUser ToApplicationUser(UserDto userDto)
        {

            return new ApplicationUser
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
            };
        }
    }
}
