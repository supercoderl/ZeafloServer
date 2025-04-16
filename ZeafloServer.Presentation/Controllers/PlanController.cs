using MediatR;
using Microsoft.AspNetCore.Authorization;
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
using ZeafloServer.Application.ViewModels.Plans;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.ViewModels.Posts;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PlanController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPlanService _planService;

        public PlanController(
            INotificationHandler<DomainNotification> notifications,
            IPlanService planService
        ) : base(notifications)
        {
            _notifications = notifications;
            _planService = planService;
        }

        /// <summary>
        /// Retrieves a paginated list of plans with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter plans.</param>
        /// <param name="status">Filter by plan status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of plans or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all plans", Description = "Return a list of plans.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<PlanViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllPlansAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<PlanViewModelSortProvider, PlanViewModel, Plan>] SortQuery? sortQuery = null
        )
        {
            return Response(await _planService.GetAllPlansAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Creates a new plan entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing plan details.</param>
        /// <returns>Returns the UID of the created plan or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create plan", Description = "Create a new plan and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePlanAsync([FromBody] CreatePlanRequest request)
        {
            return Response(await _planService.CreatePlanAsync(request));
        }
    }
}
