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
using ZeafloServer.Application.ViewModels.Likes;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly ILikeService _likeService;

        public LikeController(
            INotificationHandler<DomainNotification> notifications,
            ILikeService likeService
        ) : base(notifications)
        {
            _notifications = notifications;
            _likeService = likeService;
        }

        /// <summary>
        /// Retrieves a paginated list of likes with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter likes.</param>
        /// <param name="status">Filter by like status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of likes or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all likes", Description = "Return a list of likes.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<LikeViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllLikesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<LikeViewModelSortProvider, LikeViewModel, Like>] SortQuery? sortQuery = null
        )
        {
            return Response(await _likeService.GetAllLikesAsync(query, status, searchTerm, sortQuery));
        }
    }
}
