using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.User
{
    public class SessionRequest
    {
        [Required]
        public string SessionKey { get; set; }
    }
}
