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
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<SMS> SMSs { get; set; }
        public DbSet<PushNotification> PushNotifications { get; set; }
    }
}
