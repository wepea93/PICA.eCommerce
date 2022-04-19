
namespace eCommerce.Notifications.Core.Objects.Dtos
{
    public class NotificationDto
    {
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public NotificationDto(string subject, string email, string body, string name)
        {
            Subject = subject;
            Email = email;
            Body = body;
            Name = name;
        }

        public NotificationDto() { }
    }
}
