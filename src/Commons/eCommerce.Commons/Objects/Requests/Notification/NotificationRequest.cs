
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.Notification
{
    public  class NotificationRequest
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
