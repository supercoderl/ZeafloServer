using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Application.Interfaces;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly ICityService _cityService;

        public CityController(
            INotificationHandler<DomainNotification> notifications,
            ICityService cityService
        ) : base(notifications)
        {
            _notifications = notifications;
            _cityService = cityService;
        }

        /// <summary>
        /// Retrieves a paginated list of cities with optional filtering and sorting.
        /// </summary>
        /// <param name="query">Pagination details, including page size and page index.</param>
        /// <param name="searchTerm">Optional search keyword to filter cities.</param>
        /// <param name="status">Filter by city status (Deleted, Not Deleted, or All).</param>
        /// <param name="sortQuery">Optional sorting criteria for the results.</param>
        /// <returns>Returns a paginated list of cities or an error message if the request is invalid.</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all cities", Description = "Return a list of cities.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<CityViewModel>>))]        
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetAllCitiesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] ActionStatus status = ActionStatus.NotDeleted,
            [FromQuery][SortableFieldsAttribute<CityViewModelSortProvider, CityViewModel, City>] SortQuery? sortQuery = null
        )
        {
            return Response(await _cityService.GetAllCitiesAsync(query, status, searchTerm, sortQuery));
        }
    }
}
