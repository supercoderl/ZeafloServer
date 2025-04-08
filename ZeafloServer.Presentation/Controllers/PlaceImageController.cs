using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;
using ZeafloServer.Presentation.Swagger;
using ZeafloServer.Application.ViewModels.PlaceImages;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceImageController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IPlaceImageService _placeImageService;

        public PlaceImageController(
            INotificationHandler<DomainNotification> notifications,
            IPlaceImageService placeImageService
        ) : base(notifications)
        {
            _notifications = notifications;
            _placeImageService = placeImageService;
        }

        /// <summary>
        /// Creates a place image entry and returns its unique identifier.
        /// </summary>
        /// <param name="request">The request payload containing place image details.</param>
        /// <returns>Returns the UID of the created place image or an error message if the request is invalid.</returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create place image", Description = "Create a place image and returns UID.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> CreatePlaceImageAsync([FromBody] CreatePlaceImageRequest request)
        {
            return Response(await _placeImageService.CreatePlaceImageAsync(request));
        }
    }
}
