namespace Mottrist.Service.Features.Users.DTOs
{
    /// <summary>
    /// Data Transfer Object for user login requests.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}