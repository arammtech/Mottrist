using Microsoft.AspNetCore.Mvc;
using Mottrist.API.Response;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Messages.DTOs;
using Mottrist.Service.Features.Messages.Interfaces;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// API controller for managing messages.
    /// </summary>
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="messageService">Service for handling message operations.</param>
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Retrieves a message by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the message.</param>
        /// <returns>Message data if found; otherwise, an error message.</returns>
        /// <response code="200">Message retrieved successfully.</response>
        /// <response code="400">Invalid message ID.</response>
        /// <response code="404">Message not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("{id:int}", Name = "GetMessageByIdAsync")]
        [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
                return BadRequestResponse("InvalidId", "The provided message ID is invalid.");

            try
            {
                var message = await _messageService.GetByIdAsync(id);

                if (message is null)
                    return NotFoundResponse("MessageNotFound", "No message found with the provided ID.");

                return SuccessResponse(message, "Message retrieved successfully.");
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", httpEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all messages.
        /// </summary>
        /// <returns>List of messages.</returns>
        /// <response code="200">Messages retrieved successfully.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("all", Name = "GetAllMessagesAsync")]
        [ProducesResponseType(typeof(DataResult<MessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var dataResult = await _messageService.GetAllAsync();

                return dataResult != null
                    ? SuccessResponse(dataResult, "Messages retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "NoDataFound", "No data found.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Adds a new message.
        /// </summary>
        /// <param name="addMessageDto">The message data.</param>
        /// <returns>Created message information.</returns>
        /// <response code="201">Message created successfully.</response>
        /// <response code="400">Validation error in message data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("paged",Name = "GetAllMessagesWithPaginationAsync")]
        [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMessagesWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                    return BadRequestResponse("PaginationError", "Both page and pageSize must be greater than 0.");

                var result = await _messageService.GetAllWithPaginationAsync(page, pageSize);

                return result != null
                    ? SuccessResponse(result, "Paginated messages retrieved successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error:");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Adds a new message.
        /// </summary>
        /// <param name="addMessageDto">The message data.</param>
        /// <returns>Created message information.</returns>
        /// <response code="201">Message created successfully.</response>
        /// <response code="400">Validation error in message data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost(Name = "AddNewMessageAsync")]
        [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] AddMessageDto addMessageDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return BadRequestResponse("ValidationError", "Invalid data provided.", errors.ToArray());
            }

            try
            {
                var result = await _messageService.AddAsync(addMessageDto);

                return result.IsSuccess
                    ? CreatedResponse("GetMessageByIdAsync", new { id = result.Data?.Id }, result.Data, "Message created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create message.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="id">The ID of the message to update.</param>
        /// <param name="updateMessageDto">The updated message data.</param>
        /// <returns>Updated message information.</returns>
        /// <response code="200">Message updated successfully.</response>
        /// <response code="400">Invalid message ID or validation errors.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("{id:int}", Name = "UpdateMessageDetailsAsync")]
        [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateMessageDto updateMessageDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequestResponse("ValidationError", "Invalid data provided.", errors.ToArray());
            }

            if (id < 1)
                return BadRequestResponse("InvalidId", $"The parameter '{nameof(id)}' must be a positive integer.");

            try
            {
                var result = await _messageService.UpdateAsync(updateMessageDto);

                return result.IsSuccess
                    ? SuccessResponse(result.Data, "Message details updated successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "UpdateError", "An error occurred during update.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Deletes a message by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the message to delete.</param>
        /// <returns>A success message if deleted; otherwise, an error message.</returns>
        /// <response code="200">Message deleted successfully.</response>
        /// <response code="400">Invalid message ID.</response>
        /// <response code="404">Message not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpDelete("{id:int}", Name = "DeleteMessageAsync")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
                return BadRequestResponse("InvalidId", "The message ID provided is invalid.");

            try
            {
                var result = await _messageService.DeleteAsync(id);

                return result.IsSuccess
                    ? SuccessResponse("Message deleted successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "DeletionError", "Failed to delete the message.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "HttpRequestException", ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "UnexpectedError", $"Unexpected error: {ex.Message}");
            }
        }
    }
}
