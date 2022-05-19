
namespace eCommerce.Commons.Objects.Requests.Authorizer
{
    public class AccessTokenRequest
    {
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Audience { get; set; }
        public string Grant_type { get; set; } 

        public AccessTokenRequest(string clientId, string clientSecret, string audience) 
        {
            Client_id = clientId;
            Client_secret = clientSecret;
            Audience = audience;
            Grant_type = "client_credentials";
        }
    }
}
