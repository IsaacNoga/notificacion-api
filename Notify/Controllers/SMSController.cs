using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notify.Data;
using Notify.Models;

namespace Notify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSsController : ControllerBase
    {
        private readonly NotifyContext _context;

        public SMSsController(NotifyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SMS>>> GetSMSNotification()
        {
            if (_context.SMSs == null)
            {
                return NotFound();
            }
            return await _context.SMSs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SMS>> PostSMSNotifaction(int UserId, string UserPhone)
        {

            if (UserId == 0 || UserPhone == null)
            {
                return Problem("Missing Data.");
            }

            SMS sms = new SMS
            {
                UserId = UserId,
                UserPhone = UserPhone
            };

            _context.SMSs.Add(sms);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSMS", new { id = sms.Id }, sms);
        }
    }
}
