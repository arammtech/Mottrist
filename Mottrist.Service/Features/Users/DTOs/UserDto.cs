namespace Mottrist.Service.Features.Users.DTOs
{
    /// <summary>
    /// Data Transfer Object representing user details.
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
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's phone number.
        /// This field is optional.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the user's WhatsApp number.
        /// </summary>
        public string WhatsappNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of roles assigned to the user.
        /// </summary>
        public List<string> Roles { get; set; } = null!;
    }
}