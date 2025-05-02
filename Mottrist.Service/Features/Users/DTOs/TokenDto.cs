namespace Mottrist.Service.Features.Users.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a token and its expiration time.
    /// Typically used for authentication or authorization responses.
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Gets or sets the authentication token string.
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the token.
        /// </summary>
        public DateTime expiredDateTime { get; set; }
    }
}