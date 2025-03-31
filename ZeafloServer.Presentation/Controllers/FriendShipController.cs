using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.ViewModels.Users;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendShipController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IFriendShipService _friendShipService;

        public FriendShipController(
            INotificationHandler<DomainNotification> notifications,
            IFriendShipService friendShipService
        ) : base(notifications)
        {
            _notifications = notifications;
            _friendShipService = friendShipService;
        }

        /// <summary>
        /// Retrieves a paginated list of friend ships with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter friend ships.</param>
        /// <param name="status">Filter by friend ship status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of friend ships or an error message if the request is invalid.</returns>
        [Route("friend")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all friend ships", Description = "Return a list of friend ships.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<FriendShipViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllFriendShipsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string actionType = "",
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery] Guid? userId = null
        )
        {
            return Response(await _friendShipService.GetAllFriendShipsAsync(query, status, actionType, searchTerm, userId));
        }

        /// <summary>
        /// Retrieves a paginated list of contacts with optional filtering.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter contacts.</param>
        /// <param name="status">Filter by contact status (Deleted, Not Deleted, or All).</param>
        /// <param name="userId">Filtering by an user's id</param>
        /// <returns>Returns a paginated list of contacts or an error message if the request is invalid.</returns>
        [Route("contact")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all contacts", Description = "Return a list of contacts.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<ContactInfo>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllContactsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] Guid userId,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted       
        )
        {
            return Response(await _friendShipService.GetContactsAsync(query, status, userId, searchTerm));
        }

        /// <summary>
        /// Add friend
        /// </summary>
        /// <param name="request">Add friend request data</param>
        /// <returns>Returns uuid or error message</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Add friend", Description = "Send a add request and returns uuid.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> AddFriendAsync([FromBody] AddFriendRequest request)
        {
            return Response(await _friendShipService.AddFriend(request));
        }

        /// <summary>
        /// Cancel request
        /// </summary>
        /// <param name="request">Cancel request data</param>
        /// <returns>Returns boolean or error message</returns>
        [Route("cancel")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Cancel request", Description = "Send a cancel request and returns boolean.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<bool>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CancelRequestAsync([FromBody] CancelRequest request)
        {
            return Response(await _friendShipService.CancelRequest(request));
        }

        /// <summary>
        /// Accept request
        /// </summary>
        /// <param name="request">Accept request data</param>
        /// <returns>Returns boolean or error message</returns>
        [Route("accept")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Accept request", Description = "Send a accept request and returns boolean.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<bool>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> AcceptRequestAsync([FromBody] AcceptRequest request)
        {
            return Response(await _friendShipService.AcceptRequest(request));
        }
    }
}
