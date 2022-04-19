
namespace eCommerce.Commons.Objects.Responses.Authorizer
{
    public class TokenResponse
    {
        public string ApiUser { get; set; }
        public string AccessToken { get; set; }
        public string ExpiresAt { get; set; }

        public TokenResponse(string apiUser, string accessToken, string expiresAt)
        {
            ApiUser = apiUser;
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
        }
    }
}
