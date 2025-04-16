using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _web;
        private readonly IUser _user;

        public UserController(
            INotificationHandler<DomainNotification> notifications,
            IUserService userService,
            IWebHostEnvironment web,
            IUser user
        ) : base(notifications)
        {
            _notifications = notifications;
            _userService = userService;
            _web = web;
            _user = user;
        }

        /// <summary>
        /// Retrieves a paginated list of users with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter users.</param>
        /// <param name="status">Filter by user status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of users or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all users", Description = "Return a list of users.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<UserViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<UserViewModelSortProvider, UserViewModel, User>] SortQuery? sortQuery = null
        )
        {
            return Response(await _userService.GetAllUsersAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="register">Register request data</param>
        /// <returns>Returns uid or error message</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register a user", Description = "Create a new user and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel register)
        {
            return Response(await _userService.RegisterAsync(register, Path.Combine(_web.WebRootPath, "images", "zeaflo-logo.png")));
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="login">Login request data</param>
        /// <returns>Returns authentication token or error message</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Login user", Description = "Authenticates a user and returns a JWT token.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<object?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel login)
        {
            return Response(await _userService.LoginAsync(login));
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="request">Update request data</param>
        /// <returns>Returns user view model or error message</returns>
        [HttpPut]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Update a user", Description = "Update a user and returns view model.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UserViewModel?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            return Response(await _userService.UpdateUserAsync(request));
        }

        /// <summary>
        /// Change user avatar
        /// </summary>
        /// <param name="request">Change avatar request data</param>
        /// <returns>Returns avatar string view model or error message</returns>
        [Route("change-avatar")]
        [HttpPut]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Change user avatar", Description = "Change user avatar and returns view model.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> ChangeUserAvatarAsync([FromBody] ChangeUserAvatarRequest request)
        {
            return Response(await _userService.ChangeUserAvatarAsync(request));
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Change password request data</param>
        /// <returns>Returns boolean or error message</returns>
        [Route("change-password")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Change password", Description = "Change password of user and returns boolean.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<bool>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            return Response(await _userService.ChangePasswordAsync(request));
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <returns>Returns user profile or error message</returns>
        [Route("profile")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get profile", Description = "Returns user's profile or null.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UserViewModel?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetProfileAsync()
        {
            return Response(await _userService.GetProfileAsync(_user.GetUserId()));
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="request">Refresh token request data</param>
        /// <returns>Returns token or error message</returns>
        [Route("refresh")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Refresh token", Description = "Refresh token and returns new token.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<object?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request )
        {
            return Response(await _userService.RefreshTokenAsync(request));
        }

        /// <summary>
        /// Retrieve QR
        /// </summary>
        /// <returns>Returns qr url or error message</returns>
        [Route("retrieve")]
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve QR", Description = "Retrieve QR and returns QR url.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> RetrieveQRAsync()
        {
            return Response(await _userService.RetrieveQrAsync(_user.GetUserId()));
        }
    }
}
