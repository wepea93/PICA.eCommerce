namespace eCommerce.Commons.Objects.Responses.AuthorizerUser
{
    public class UserToken
    {
        public string? Token { get; set; }
        public DateTime? ExpirationToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpirationRefreshToken { get; set; }
    }
}
