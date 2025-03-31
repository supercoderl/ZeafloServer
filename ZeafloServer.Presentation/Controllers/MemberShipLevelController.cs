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
using ZeafloServer.Application.ViewModels.MemberShipLevels;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberShipLevelController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IMemberShipLevelService _memberShipLevelService;

        public MemberShipLevelController(
            INotificationHandler<DomainNotification> notifications,
            IMemberShipLevelService memberShipLevelService
        ) : base(notifications)
        {
            _notifications = notifications;
            _memberShipLevelService = memberShipLevelService;
        }

        /// <summary>
        /// Retrieves a paginated list of member ship levels with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter member ship levels.</param>
        /// <param name="status">Filter by member ship level status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of member ship levels or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all member ship levels", Description = "Return a list of member ship levels.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<MemberShipLevelViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllMemberShipLevelsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<MemberShipLevelViewModelSortProvider, MemberShipLevelViewModel, MemberShipLevel>] SortQuery? sortQuery = null
        )
        {
            return Response(await _memberShipLevelService.GetAllMemberShipLevelsAsync(query, status, searchTerm, sortQuery));
        }
    }
}
