using eCommerce.Notifications.Core.Config;
using eCommerce.Notifications.Core.Contracts.Repositories;
using eCommerce.Notifications.Core.Objects.DbTypes;
using System.Net;
using System.Net.Mail;

namespace eCommerce.Notifications.Infraestructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public Task SentNotificationFromGmail(NotificationEntity notificationentity)
        {
            var fromPassword = AppConfiguration.Configuration["AppConfiguration:Gmail:Password"].ToString();
            var fromAddress = new MailAddress(AppConfiguration.Configuration["AppConfiguration:Gmail:Email"].ToString(), "Ecommerce");
            var toAddress = new MailAddress(notificationentity.Email, notificationentity.Name);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = notificationentity.Subject,
                Body = notificationentity.Body
            })
            {
                 smtp.Send(message);
            }
            return Task.CompletedTask;
        }
    }
}
