using DemoApplication.Database.EmailModel;

namespace DemoApplication.Database.NotificationModel
{
    public class Notification
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Tittle { get; set; }
        public string Message { get; set; }
        public int TargetEmailId { get; set; }
        public Email TargetEmail { get; set; }
    }
}
