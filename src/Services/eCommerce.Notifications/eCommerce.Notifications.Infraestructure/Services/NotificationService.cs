using eCommerce.Notifications.Core.Contracts.Repositories;
using eCommerce.Notifications.Core.Contracts.Services;
using eCommerce.Notifications.Core.Objects.DbTypes;
using eCommerce.Notifications.Core.Objects.Dtos;

namespace eCommerce.Notifications.Infraestructure.Services
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SentNotification(NotificationDto notificationDto)
        {
            await _notificationRepository.SentNotificationFromGmail(new NotificationEntity(notificationDto.Subject, notificationDto.Email, notificationDto.Body, notificationDto.Name));
        }
    }
}
