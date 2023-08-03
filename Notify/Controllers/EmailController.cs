using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notify.Data;
using Notify.Models;

namespace Notify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly NotifyContext _context;

        public EmailsController(NotifyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmailNotification()
        {
            if (_context.Emails == null)
            {
                return NotFound();
            }
            return await _context.Emails.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Email>> PostEmailNotifaction(int UserId, string UserEmail)
        {
            if (UserEmail == null || UserId <= 0)
            {
                return Problem("Missing Data");
            }

            Email email = new Email { UserId=UserId,UserEmail = UserEmail};

            _context.Emails.Add(email);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmail", new { id = email.Id }, email);
        }
    }
}
