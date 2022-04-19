using eCommerce.Notifications.Core.Objects.DbTypes;

namespace eCommerce.Notifications.Core.Contracts.Repositories
{
    public interface INotificationRepository
    {
        Task SentNotificationFromGmail(NotificationEntity notificationentity);
    }
}
