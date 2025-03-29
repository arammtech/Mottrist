namespace Mottrist.Service.Features.Users.DTOs
{
    /// <summary>
    /// Data Transfer Object for representing user information.
    /// Used for transferring user-related data between layers.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's phone number.
        /// Optional field.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// Used for authentication purposes.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
