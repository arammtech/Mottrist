using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Messages.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Messages.Interfaces
{
    public interface IMessageService
    {
        Task<DataResult<MessageDto>?> GetAllAsync(Expression<Func<Message, bool>>? filter = null);
        Task<PaginatedResult<MessageDto>?> GetAllWithPaginationAsync(
            int page,
            int pageSize = 10,
            Expression<Func<Message, bool>>? filter = null);
        Task<MessageDto?> GetByIdAsync(int Id);
        Task<Result<MessageDto>> AddAsync(int userId,AddMessageDto destinationDto);
        Task<Result<MessageDto>> UpdateAsync(UpdateMessageDto destinationDto);
        Task<Result> DeleteAsync(int Id);
    }
}
