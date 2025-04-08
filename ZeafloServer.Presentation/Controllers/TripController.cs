using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TripController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly ITripService _tripService;

        public TripController(
            INotificationHandler<DomainNotification> notifications,
            ITripService tripService
        ) : base(notifications)
        {
            _notifications = notifications;
            _tripService = tripService;
        }

        /// <summary>
        /// Generates a travel itinerary based on the provided duration, start date, and end date.
        /// The itinerary will be customized for the given province and trip duration.
        /// </summary>
        /// <param name="city">Request city</param>
        /// <param name="duration">Request duration</param>
        /// <returns>A list of itinerary details, including activities and locations for each day of the trip</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Generate a trip hint base on city and duration", Description = "Return an trip object.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PageResult<TripDurationViewModel>>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GenerateItinerary(
            [FromQuery] Guid cityId,
            [FromQuery] Guid tripDurationId
        )
        {
            return Response(await _tripService.GenerateTripHintAsync(cityId, tripDurationId));
        }
    }
}
