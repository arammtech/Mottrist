namespace Mottrist.Service.Features.Messages.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for messages.
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user who sent the message.
        /// </summary>
        public string FullName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email of the user who sent the message.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string MessageBody { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time the message was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}