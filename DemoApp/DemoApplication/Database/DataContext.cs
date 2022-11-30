using DemoApplication.Database.EmailModel;
using DemoApplication.Database.NotificationModel;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
