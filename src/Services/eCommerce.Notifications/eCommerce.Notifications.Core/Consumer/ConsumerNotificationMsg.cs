using eCommerce.Commons.Objects.Messaging;
using eCommerce.Notifications.Core.Config;
using eCommerce.Notifications.Core.Contracts.Services;
using eCommerce.Notifications.Core.Objects.Dtos;
using eCommerce.PublisherSubscriber.Messaging;
using eCommerce.PublisherSubscriber.Object;
using Newtonsoft.Json;

namespace eCommerce.Notifications.Core.Consumers
{
    public class ConsumerNotificationMsg: ConsumerMessage<NotificationMsg>
    {
        private readonly INotificationService _notificationService;

        public ConsumerNotificationMsg(INotificationService notificationService) 
            : base() 
        {
            _notificationService = notificationService;
            InitAsPublishedMessage(AppConfiguration.Configuration["AppConfiguration:EmailNotificationQueueName"].ToString());
        }
        
        public override async Task ProcessMessage(Message<NotificationMsg> messsage)
        {
            var strMessage = JsonConvert.SerializeObject(messsage);
            var notification = JsonConvert.DeserializeObject<Message<NotificationMsg>>(strMessage);

            if(notification != null)
                await _notificationService.SentNotification(new NotificationDto(notification.BusinessObject.Subject, 
                    notification.BusinessObject.CustomerEmail, notification.BusinessObject.Body, notification.BusinessObject.CustomerName));
        }
    }
}
