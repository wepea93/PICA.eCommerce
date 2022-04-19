using eCommerce.Notifications.Core.Objects.Dtos;

namespace eCommerce.Notifications.Core.Contracts.Services
{
    public interface INotificationService
    {
        Task SentNotification(NotificationDto notificationDto);
    }
}
