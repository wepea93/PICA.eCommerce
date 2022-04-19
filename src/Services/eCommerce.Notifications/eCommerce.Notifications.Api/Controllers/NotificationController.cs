using eCommerce.Commons.Objects.Requests.Notification;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Notifications.Core.Contracts.Services;
using eCommerce.Notifications.Core.Objects.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Notifications.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> Post([FromBody] NotificationRequest request)
        {
            await _notificationService.SentNotification(new NotificationDto(request.Subject, request.Email, request.Body, request.Name));
            return Ok(new ServiceResponse<bool>("Successful", true));
        }
    }
}
