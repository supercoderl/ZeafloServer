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
using ZeafloServer.Application.ViewModels.Comments;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly ICommentService _commentService;

        public CommentController(
            INotificationHandler<DomainNotification> notifications,
            ICommentService commentService
        ) : base(notifications)
        {
            _notifications = notifications;
            _commentService = commentService;
        }

        /// <summary>
        /// Retrieves a paginated list of comments with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter comments.</param>
        /// <param name="status">Filter by comment status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of comments or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all comments", Description = "Return a list of comments.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<CommentViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllCommentsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<CommentViewModelSortProvider, CommentViewModel, Comment>] SortQuery? sortQuery = null
        )
        {
            return Response(await _commentService.GetAllCommentsAsync(query, status, searchTerm, sortQuery));
        }
    }
}
