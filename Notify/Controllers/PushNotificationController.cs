using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notify.Data;
using Notify.Models;

namespace Notify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController : ControllerBase
    {
        private readonly NotifyContext _context;

        public PushNotificationController(NotifyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PushNotification>>> GetPushNotification()
        {
            if (_context.PushNotifications == null)
            {
                return NotFound();
            }
            return await _context.PushNotifications.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PushNotification>> PostPushNotifaction(int UserId, string UserName)
        {
            if (UserName == null && UserId == 0)
            {
                return Problem("Missing Data.");
            }

            PushNotification push = new PushNotification { UserId = UserId, UserName = UserName };

            _context.PushNotifications.Add(push);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmail", new { id = push.Id }, push);
        }
    }
}
