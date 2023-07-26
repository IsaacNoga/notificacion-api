using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public NotificationLogsController(NotifyContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotificationLog>> PostNotificationLog(NotificationLog notificationLog)
        {
          if (_context.NotificationLog == null)
          {
              return Problem("Entity set 'NotifyContext.NotificationLog'  is null.");
          }
            _context.NotificationLog.Add(notificationLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationLog", new { id = notificationLog.id }, notificationLog);
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
