namespace Mottrist.Service.Features.Messages.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for updating a message.
    /// </summary>
    public class UpdateMessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the updated content of the message.
        /// </summary>
        public string MessageBody { get; set; } = null!;
    }
}