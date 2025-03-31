using DeepSeek.ApiClient.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels.Deepseeks;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Presentation.Models;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AiController : ApiController
    {
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IDeepseekService _deepseekService;

        public AiController(
            INotificationHandler<DomainNotification> notifications,
            IDeepseekService deepseekService
        ) : base(notifications)
        {
            _notifications = notifications;
            _deepseekService = deepseekService;
        }

        /// <summary>
        /// Chat with DeepseekAI
        /// </summary>
        /// <param name="promt">Request content</param>
        /// <returns>Returns message object or error message</returns>
        [Route("deepseek")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Chat with deepseek", Description = "Get message response.")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DeepseekResponse?>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> GenerateResponseAsync([FromBody] string promt)
        {
            return Response(await _deepseekService.GenerateResponseAsync(promt));
        }
    }
}
