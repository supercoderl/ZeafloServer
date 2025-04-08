using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoPostController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPhotoPostService _photoPostService;

        public PhotoPostController(
            INotificationHandler<DomainNotification> notifications,
            IPhotoPostService photoPostService
        ) : base(notifications)
        {
            _notifications = notifications;
            _photoPostService = photoPostService;
        }

        /// <summary>
        /// Retrieves a paginated list of photo posts with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter posts.</param>
        /// <param name="status">Filter by post status (Deleted, Not Deleted, or All).</param>
        /// <param name="scope">Determines the scope of photo posts to retrieve: "mine" (my posts), "user" (specific user's posts), or "others" (photo posts from other users).</param>
        /// <param name="userId">User ID to filter photo posts by, used when scope is "mine" or "user".</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of photo posts or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all photo posts", Description = "Return a list of photo posts.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<PhotoPostViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllPhotoPostsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery] string scope = "others",
            [FromQuery] Guid? userId = null,
            [FromQuery][SortableFieldsAttribute<PhotoPostViewModelSortProvider, PhotoPostViewModel, PhotoPost>] SortQuery? sortQuery = null
        )
        {
            return Response(await _photoPostService.GetAllPhotoPostsAsync(query, status, scope, searchTerm, userId, sortQuery));
        }

        /// <summary>
        /// Creates a new photo post entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing photo post details.</param>
        /// <returns>Returns the UID of the created photo post or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create photo post", Description = "Create a new photo post and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePhotoPostRequest request)
        {
            return Response(await _photoPostService.CreatePhotoPostAsync(request));
        }
    }
}
