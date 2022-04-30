using System.ComponentModel.DataAnnotations;

namespace eCommerce.AuthorizerUser.Api.Models
{
    public class UserInfo
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
