using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.User
{
    public class UserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
