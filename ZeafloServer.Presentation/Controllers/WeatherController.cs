using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Application.ViewModels.Weathers;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IWeatherService _weatherService;

        public WeatherController(
            INotificationHandler<DomainNotification> notifications,
            IWeatherService weatherService
        ) : base(notifications)
        {
            _notifications = notifications;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Get weather
        /// </summary>
        /// <param name="lat">Latitude of the location</param>
        /// <param name="lon">Longitude of the location</param>
        /// <returns>Returns weather information or error message</returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get weather", Description = "Returns weather information or null.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<WeatherViewModel?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GetWeatherAsync(
            [FromQuery] double lat,
            [FromQuery] double lon
        )
        {
            return Response(await _weatherService.GetWeatherAsync(lat, lon));
        }
    }
}
