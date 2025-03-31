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
using ZeafloServer.Application.ViewModels.MapThemes;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MapThemeController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IMapThemeService _mapThemeService;

        public MapThemeController(
            INotificationHandler<DomainNotification> notifications,
            IMapThemeService mapThemeService
        ) : base(notifications)
        {
            _notifications = notifications;
            _mapThemeService = mapThemeService;
        }

        /// <summary>
        /// Retrieves a paginated list of map themes with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter map themes.</param>
        /// <param name="status">Filter by map theme status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of map themes or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all map themes", Description = "Return a list of map themes.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<MapThemeViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllMapThemesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<MapThemeViewModelSortProvider, MapThemeViewModel, MapTheme>] SortQuery? sortQuery = null
        )
        {
            return Response(await _mapThemeService.GetAllMapThemesAsync(query, status, searchTerm, sortQuery));
        }

        /// <summary>
        /// Creates a new map theme entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing map theme details.</param>
        /// <returns>Returns the UID of the created map theme or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create map theme", Description = "Create a new map theme and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreateMapThemeAsync([FromBody] CreateMapThemeRequest request)
        {
            return Response(await _mapThemeService.CreateMapThemeAsync(request));
        }
    }
}
