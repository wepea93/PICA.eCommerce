using Microsoft.AspNetCore.Identity;

namespace eCommerce.AuthorizerUser.Api.Models
{
    public class IdentityUserCustom : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
