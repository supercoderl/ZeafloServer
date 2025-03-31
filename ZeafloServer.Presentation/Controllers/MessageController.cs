using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.ViewModels.Users;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IMessageService _messageService;

        public MessageController(
            INotificationHandler<DomainNotification> notifications,
            IMessageService messageService
        ) : base(notifications)
        {
            _notifications = notifications;
            _messageService = messageService;
        }

        /// <summary>
        /// Retrieves a paginated list of messages with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter messages.</param>
        /// <param name="status">Filter by message status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of messages or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all messages", Description = "Return a list of messages.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<MessageViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllMessagesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<MessageViewModelSortProvider, MessageViewModel, Message>] SortQuery? sortQuery = null
        )
        {
            return Response(await _messageService.GetAllMessagesAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Update unread messages
        /// </summary>
        /// <param name="request">Update unread messages request data</param>
        /// <returns>Returns boolean or error message</returns>
        [Route("update-unread-messages")]
        [HttpPut]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Update unread messages", Description = "Update unread messages and returns boolean.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<bool>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateUnreadMessagesAsync([FromBody] UpdateUnreadMessagesRequest request)
        {
            return Response(await _messageService.UpdateUnreadMessagesAsync(request));
        }
    }
}
