namespace Mottrist.Service.Features.Messages.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for adding a new message.
    /// </summary>
    public class AddMessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user who is sending the message.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string MessageBody { get; set; } = null!;
    }
}