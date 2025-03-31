using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeafloServer.Application.ViewModels.Apps;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppController : ApiController
    {
        public AppController(INotificationHandler<DomainNotification> notifications) : base(notifications)
        {

        }

        [HttpGet]
        public ActionResult GetInformation()
        {
            AppViewModel app = new AppViewModel
            {
                App = new AppInformation
                {
                    Name = "Zeaflo",
                    Description = ""
                },
                User = new UserInformation
                {
                    Name = "",
                    Avatar = "",
                    Email = ""
                }
            };

            return Ok(app);
        }

/*        [HttpGet("{lang}")]
        public async Task<IActionResult> GetTranslation(string lang)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "i18n", $"{lang}.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { Message = "Language file not found!" });
            }

            var fileContents = await System.IO.File.ReadAllTextAsync(filePath);
            return Content(fileContents, "application/json");
        }*/
    }
}
