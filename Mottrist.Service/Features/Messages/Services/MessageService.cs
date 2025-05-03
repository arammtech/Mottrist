using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Messages.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Messages.Interfaces;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Messages.Services
{
    /// <summary>
    /// Service for handling message-related operations.
    /// </summary>

    public class MessageService : BaseService, IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all messages with optional filtering.
        /// </summary>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>A list of messages or null if an error occurs.</returns>
        public async Task<DataResult<MessageDto>?> GetAllAsync(Expression<Func<Message, bool>>? filter = null)
        {
            try
            {
                var messagesQuery = _unitOfWork.Repository<Message>().Table;

                if (filter != null)
                    messagesQuery = messagesQuery.Where(filter);

                messagesQuery = messagesQuery
                    .Include(x => x.User);

                var messages = await _mapper.ProjectTo<MessageDto>(messagesQuery).ToListAsync();

                return new DataResult<MessageDto>
                {
                    Data = messages.Any() ? messages : Enumerable.Empty<MessageDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves messages with pagination.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="filter">Optional filter expression.</param>
        /// <returns>A paginated list of messages or null if an error occurs.</returns>
        public async Task<PaginatedResult<MessageDto>?> GetAllWithPaginationAsync(int page, int pageSize = 10, Expression<Func<Message, bool>>? filter = null)
        {
            try
            {
                var messagesQuery = _unitOfWork.Repository<Message>().Table;

                if (filter != null)
                    messagesQuery = messagesQuery.Where(filter);

                messagesQuery = messagesQuery
                    .Include(x => x.User);

                var messages = await _mapper.ProjectTo<MessageDto>(messagesQuery).ToListAsync();

                return new PaginatedResult<MessageDto>
                {
                    Data = messages,
                    TotalRecordsCount = 10,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = messages.Count
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a message by its ID.
        /// </summary>
        /// <param name="Id">The ID of the message.</param>
        /// <returns>The message DTO or null if not found.</returns>
        public async Task<MessageDto?> GetByIdAsync(int Id)
        {
            try
            {
                var message = await _unitOfWork.Repository<Message>()
                    .Table
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                return _mapper.Map<MessageDto>(message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new message to the database.
        /// </summary>
        /// <param name="addMessageDto">The message data to add.</param>
        /// <returns>The added message DTO wrapped in a result.</returns>
        public async Task<Result<MessageDto>> AddAsync(AddMessageDto addMessageDto)
        {
            try
            {
                Message message = _mapper.Map<Message>(addMessageDto);
                message.CreatedAt = DateTime.Now;

                await _unitOfWork.Repository<Message>().AddAsync(message);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (message.Id <= 0 || !saveResult.IsSuccess)
                    return Result<MessageDto>.Failure("Failed to save message.");

                return Result<MessageDto>.Success(await GetByIdAsync(message.Id));
            }
            catch (Exception ex)
            {
                return Result<MessageDto>.Failure($"Error creating message: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="updateMessageDto">The message data to update.</param>
        /// <returns>The updated message DTO wrapped in a result.</returns>
        public async Task<Result<MessageDto>> UpdateAsync(UpdateMessageDto updateMessageDto)
        {
            try
            {
                var message = await _unitOfWork.Repository<Message>().GetAsync(d => d.Id == updateMessageDto.Id);

                if (message == null)
                    return Result<MessageDto>.Failure("Message not found.");

                _mapper.Map(updateMessageDto, message);

                await _unitOfWork.Repository<Message>().UpdateAsync(message);
                var result = await _unitOfWork.SaveChangesAsync();

                return Result<MessageDto>.Success(await GetByIdAsync(message.Id));
            }
            catch (Exception ex)
            {
                return Result<MessageDto>.Failure($"Error updating message: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a message by its ID.
        /// </summary>
        /// <param name="Id">The ID of the message to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
        public async Task<Result> DeleteAsync(int Id)
        {
            if (Id < 1)
                return Result.Failure("Invalid message ID.");

            try
            {
                var message = await _unitOfWork.Repository<Message>().GetAsync(d => d.Id == Id);

                if (message == null)
                    return Result.Failure("Message not found.");

                await _unitOfWork.Repository<Message>().DeleteAsync(message);
                var result = await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error while deleting message: {ex.Message}");
            }
        }

    }
}
