using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.ViewModels.UserLevels;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.ViewModels.Users;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserLevelController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IUserLevelService _userLevelService;
        private readonly IUser _user;

        public UserLevelController(
            INotificationHandler<DomainNotification> notifications,
            IUserLevelService userLevelService,
            IUser user
        ) : base(notifications)
        {
            _notifications = notifications;
            _userLevelService = userLevelService;
            _user = user;
        }

        /// <summary>
        /// Retrieves a user level with user id.
        /// </summary>
        /// <returns>Returns a user level or an error message if the request is invalid.</returns>
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(Summary = "Get user level", Description = "Return a user level")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UserLevelViewModel?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetUserLevelAsync()
        {
            return Response(await _userLevelService.GetUserLevelAsync(_user.GetUserId()));
        }

        /// <summary>
        /// Add point
        /// </summary>
        /// <param name="request">Add point request data</param>
        /// <returns>Returns uid or error message</returns>
        [Route("add-point")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add Point", Description = "Add point of user and returns uid.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> AddPointAsync([FromBody] AddPointRequest request)
        {
            return Response(await _userLevelService.AddPointAsync(request));
        }
    }
}
