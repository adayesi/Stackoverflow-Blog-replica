using DecaBlog.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace DecaBlog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notifications;

        public NotificationsController(INotificationsService notifications)
        {
            _notifications = notifications;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserNotifications([FromRoute] string userId, [FromQuery] int pageNumber, [FromQuery] int perPage)
        => Ok( await _notifications.GetUserNofitifations(userId, pageNumber, perPage));
    }
}
