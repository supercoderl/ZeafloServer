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
using ZeafloServer.Application.ViewModels.PostMedias;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PostMediaController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPostMediaService _postMediaService;

        public PostMediaController(
            INotificationHandler<DomainNotification> notifications,
            IPostMediaService postMediaService
        ) : base(notifications)
        {
            _notifications = notifications;
            _postMediaService = postMediaService;
        }

        /// <summary>
        /// Creates a new post media entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing media details.</param>
        /// <returns>Returns the UID of the created post media or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create post media", Description = "Create a new post media and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePostMediaAsync([FromBody] CreatePostMediaRequest request)
        {
            return Response(await _postMediaService.CreatePostMediaAsync(request));
        }
    }
}
