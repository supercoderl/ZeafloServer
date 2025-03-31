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
using ZeafloServer.Application.ViewModels.Notifications;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly INotificationService _notificationService;

        public NotificationController(
            INotificationHandler<DomainNotification> notifications,
            INotificationService notificationService
        ) : base(notifications)
        {
            _notifications = notifications;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Retrieves a paginated list of notifications with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter notifications.</param>
        /// <param name="status">Filter by notification status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of notifications or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all notifications", Description = "Return a list of notifications.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<NotificationViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllNotificationsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<NotificationViewModelSortProvider, NotificationViewModel, Notification>] SortQuery? sortQuery = null
        )
        {
            return Response(await _notificationService.GetAllNotificationsAsync(query, status, searchTerm, sortQuery));
        }
    }
}
