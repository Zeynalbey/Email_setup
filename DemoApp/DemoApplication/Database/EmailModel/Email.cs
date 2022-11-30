using DemoApplication.Database.NotificationModel;

namespace DemoApplication.Database.EmailModel
{
    public class Email
    {
        public int Id { get; set; }
        public string TargetEmail { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
