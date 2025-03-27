using Mottrist.Domain.Identity;
using Mottrist.Service.Features.User.DTOs;
using static Mottrist.Service.Features.User.UserGenerate;

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
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Password = user.PasswordHash,
                Role =  roles.GetAwaiter().GetResult().ToList(),
                IsLocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow
            };
        }

        public static ApplicationUser ToApplicationUser(UserDto userDto)
        {
            var defaultUserName = 
                string.IsNullOrWhiteSpace(userDto.UserName)
                ? GenerateDefaultUserNameFromEmailOrNames(userDto)
                : userDto.UserName;

            return new ApplicationUser
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = defaultUserName,
                PhoneNumber = userDto.Phone,
                PasswordHash = userDto.Password
            };
        }
    }
}
