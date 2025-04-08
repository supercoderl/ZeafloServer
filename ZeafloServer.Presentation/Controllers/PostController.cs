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
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.ViewModels.PostMedias;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPostService _postService;

        public PostController(
            INotificationHandler<DomainNotification> notifications,
            IPostService postService
        ) : base(notifications)
        {
            _notifications = notifications;
            _postService = postService;
        }

        /// <summary>
        /// Retrieves a paginated list of posts with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter posts.</param>
        /// <param name="status">Filter by post status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of posts or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all posts", Description = "Return a list of posts.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<PostViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllPostsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery] string scope = "others",
            [FromQuery] Guid? userId = null,
            [FromQuery][SortableFieldsAttribute<PostViewModelSortProvider, PostViewModel, Post>] SortQuery? sortQuery = null
        )
        {
            return Response(await _postService.GetAllPostsAsync(query, status, scope, searchTerm, userId, sortQuery));
        }

        /// <summary>
        /// Creates a new post entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing post details.</param>
        /// <returns>Returns the UID of the created post or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create post", Description = "Create a new post and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request)
        {
            return Response(await _postService.CreatePostAsync(request));
        }

        /// <summary>
        /// Reacts a post entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing post details.</param>
        /// <returns>Returns the UID of the reacted post or an error message if the request is invalid.</returns>
        [Route("react")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "React post", Description = "React a post and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> ReactPostAsync([FromBody] ReactPostRequest request)
        {
            return Response(await _postService.ReactPostAsync(request));
        }

        /// <summary>
        /// Saves a post entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing post details.</param>
        /// <returns>Returns the UID of the saved post or an error message if the request is invalid.</returns>
        [Route("save")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Save post", Description = "Save a post and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> SavePostAsync([FromBody] SavePostRequest request)
        {
            return Response(await _postService.SavePostAsync(request));
        }
    }
}
