
namespace eCommerce.Notifications.Core.Objects.DbTypes
{
    public class NotificationEntity
    {
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }

        public NotificationEntity(string subject, string email, string body, string name)
        {
            Subject = subject;
            Email = email;
            Body = body;
            Name = name;
        }

        public NotificationEntity() { }
    }
}
