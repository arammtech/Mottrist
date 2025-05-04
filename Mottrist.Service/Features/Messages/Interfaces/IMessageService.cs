using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Messages.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Messages.Interfaces
{
    /// <summary>
    /// Provides message-related operations, including retrieval, addition, updating, and deletion of messages.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Retrieves all messages based on an optional filter.
        /// </summary>
        /// <param name="filter">
        /// An optional filter expression to apply when retrieving messages.
        /// </param>
        /// <returns>
        /// A result containing a collection of messages that match the filter criteria.
        /// </returns>
        Task<DataResult<MessageDto>?> GetAllAsync(Expression<Func<Message, bool>>? filter = null);

        /// <summary>
        /// Retrieves a paginated list of messages based on an optional filter.
        /// </summary>
        /// <param name="page">
        /// The page number to retrieve.
        /// </param>
        /// <param name="pageSize">
        /// The number of messages per page (default is 10).
        /// </param>
        /// <param name="filter">
        /// An optional filter expression to apply when retrieving messages.
        /// </param>
        /// <returns>
        /// A result containing a paginated list of messages.
        /// </returns>
        Task<PaginatedResult<MessageDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Message, bool>>? filter = null);

        /// <summary>
        /// Retrieves a message by its unique identifier.
        /// </summary>
        /// <param name="Id">
        /// The unique ID of the message to retrieve.
        /// </param>
        /// <returns>
        /// The message data transfer object if found, otherwise null.
        /// </returns>
        Task<MessageDto?> GetByIdAsync(int Id);

        /// <summary>
        /// Adds a new message.
        /// </summary>
        /// <param name="userId">
        /// The ID of the user creating the message.
        /// </param>
        /// <param name="destinationDto">
        /// The data transfer object containing message details.
        /// </param>
        /// <returns>
        /// A result indicating whether the message was successfully added.
        /// </returns>
        Task<Result<MessageDto>> AddAsync(int userId, AddMessageDto destinationDto);

        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="destinationDto">
        /// The data transfer object containing updated message details.
        /// </param>
        /// <returns>
        /// A result indicating whether the update was successful.
        /// </returns>
        Task<Result<MessageDto>> UpdateAsync(UpdateMessageDto destinationDto);

        /// <summary>
        /// Deletes a message by its unique identifier.
        /// </summary>
        /// <param name="Id">
        /// The unique ID of the message to delete.
        /// </param>
        /// <returns>
        /// A result indicating whether the deletion was successful.
        /// </returns>
        Task<Result> DeleteAsync(int Id);
    }
}
