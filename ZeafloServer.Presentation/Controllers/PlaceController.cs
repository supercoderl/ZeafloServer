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
using ZeafloServer.Application.ViewModels.Places;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPlaceService _placeService;

        public PlaceController(
            INotificationHandler<DomainNotification> notifications,
            IPlaceService placeService
        ) : base(notifications)
        {
            _notifications = notifications;
            _placeService = placeService;
        }

        /// <summary>
        /// Retrieves a paginated list of places with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter places.</param>
        /// <param name="status">Filter by place status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of places or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all places", Description = "Return a list of places.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<PlaceViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllPlacesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<PlaceViewModelSortProvider, PlaceViewModel, Place>] SortQuery? sortQuery = null
        )
        {
            return Response(await _placeService.GetAllPlacesAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Creates a place entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing place details.</param>
        /// <returns>Returns the UID of the saved place or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create place", Description = "Create a place and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePlaceAsync([FromBody] CreatePlaceRequest request)
        {
            return Response(await _placeService.CreatePlaceAsync(request));
        }
    }
}
