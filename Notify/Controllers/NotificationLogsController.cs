using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notify.Data;
using Notify.Models;

namespace Notify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationLogsController : ControllerBase
    {
        private readonly NotifyContext _context;
        private readonly EmailsController _emailsController;
        private readonly SMSsController _smssController;
        private readonly PushNotificationController _pushNotificationController;

        public NotificationLogsController(NotifyContext context, EmailsController emailsController, SMSsController smssController, PushNotificationController pushNotificationController)
        {
            _context = context;
            _emailsController = emailsController;
            _smssController = smssController;
            _pushNotificationController = pushNotificationController;
        }


        private List<Users>GetUsersByCategoryId(int IdCategory)
        {
            List<int> userIds = _context.UserCategories
                .Where(userCat => userCat.idCategory ==  IdCategory)
                .Select(userCat=> userCat.idUser)
                .ToList();

            List<Users> users = _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToList();

            return users;
        }

        // GET: api/NotificationLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationLog>>> GetNotificationLog()
        {
          if (_context.NotificationLog == null)
          {
              return NotFound();
          }
            return await _context.NotificationLog.ToListAsync();
        }

        // GET: api/NotificationLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationLog>> GetNotificationLog(int id)
        {
          if (_context.NotificationLog == null)
          {
              return NotFound();
          }
            var notificationLog = await _context.NotificationLog.FindAsync(id);

            if (notificationLog == null)
            {
                return NotFound();
            }

            return notificationLog;
        }

        // PUT: api/NotificationLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationLog(int id, NotificationLog notificationLog)
        {
            if (id != notificationLog.id)
            {
                return BadRequest();
            }

            _context.Entry(notificationLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NotificationLogs
        [HttpPost]
        public async Task<ActionResult<NotificationLog>> PostNotificationLog(int CategoryId, string message)
        {
          if (CategoryId == 0)
          {
              return Problem("Category not sent");
          }

            List<Users> userList = GetUsersByCategoryId(CategoryId);

            NotificationLog notificationLog;

            foreach(Users user in userList)
            {
                 notificationLog = new NotificationLog {
                    userId = user.Id,
                    categoryId = CategoryId,
                    message = message,
                   
                };

                foreach(var subscription in  user.Subscriptions)
                {
                    if (subscription.Equals("Email"))
                    {
                        await _emailsController.PostEmailNotifaction(user.Id,user.Email);
                        notificationLog.notificationType = "Email";
                    }
                    else if(subscription.Equals("SMS"))
                    {
                        await _smssController.PostSMSNotifaction(user.Id, user.PhoneNumber);
                        notificationLog.notificationType = "SMS";
                    }else if(subscription.Equals("Push Notification"))
                    {
                        await _pushNotificationController.PostPushNotifaction(user.Id, user.Name);
                        notificationLog.notificationType = "Push Notification";
                    }

                    _context.NotificationLog.Add(notificationLog);
                }

            }

            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationLog", new { id = notificationLog.Id }, notificationLog);
        }

        // DELETE: api/NotificationLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationLog(int id)
        {
            if (_context.NotificationLog == null)
            {
                return NotFound();
            }
            var notificationLog = await _context.NotificationLog.FindAsync(id);
            if (notificationLog == null)
            {
                return NotFound();
            }

            _context.NotificationLog.Remove(notificationLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationLogExists(int id)
        {
            return (_context.NotificationLog?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
