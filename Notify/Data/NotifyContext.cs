using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Models;

namespace Notify.Data
{
    public class NotifyContext : DbContext
    {
        public NotifyContext (DbContextOptions<NotifyContext> options)
            : base(options)
        {
        }

        public DbSet<Notify.Models.NotificationLog> NotificationLog { get; set; } = default!;
    }
}
