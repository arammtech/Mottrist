using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;

namespace Mottrist.Service.Features.Users.Interface
{
    /// <summary>
    /// Interface for user service, responsible for authentication, registration, and user management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="userDto">User DTO containing authentication details.</param>
        /// <returns>JWT token with expiration details.</returns>
        Task<TokenDto> LoginAsync(UserDto userDto);

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        Task LogoutAsync();

        /// <summary>
        /// Adds a new user and assigns roles.
        /// </summary>
        /// <param name="addUserDto">DTO containing user details.</param>
        /// <returns>Result  upon success.</returns>
        Task<Result> AddUserAsync(AddUserDto addUserDto);

        /// <summary>
        /// Retrieves user details by email.
        /// </summary>
        /// <param name="email">User's email address.</param>
        /// <returns>User DTO containing user details.</returns>
        Task<UserDto> GetUserByEmail(string email);


        Task<Result> SendEmailAsync(string Email);
        Task<Result> ConfirmEmailAsync(string userId, string token);

    }
}