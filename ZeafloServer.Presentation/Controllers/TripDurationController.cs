using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TripDurationController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly ITripDurationService _tripDurationService;

        public TripDurationController(
            INotificationHandler<DomainNotification> notifications,
            ITripDurationService tripDurationService
        ) : base(notifications)
        {
            _notifications = notifications;
            _tripDurationService = tripDurationService;
        }

        /// <summary>
        /// Retrieves a paginated list of trip durations with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter trip durations.</param>
        /// <param name="status">Filter by city status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of trip durations or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all trip durations", Description = "Return a list of trip durations.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<TripDurationViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllTripDurationsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<TripDurationViewModelSortProvider, TripDurationViewModel, TripDuration>] SortQuery? sortQuery = null
        )
        {
            return Response(await _tripDurationService.GetAllTripDurationsAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Creates a trip duration entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing trip duration details.</param>
        /// <returns>Returns the UID of the created trip duration or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create trip duration", Description = "Create a new trip duration and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreateTripDurationAsync([FromBody] CreateTripDurationRequest request)
        {
            return Response(await _tripDurationService.CreateTripDurationAsync(request));
        }
    }
}
