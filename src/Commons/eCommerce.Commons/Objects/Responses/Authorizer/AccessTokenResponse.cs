
namespace eCommerce.Commons.Objects.Responses.Authorizer
{
    public class AccessTokenResponse
    {
        private DateTime _expires_at { get; set; }
        private int _expires_in { get; set; }

        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Scope { get; set; }
        public int Expires_in 
        {
            get { return _expires_in; }
            set 
            {
                _expires_in = value;
                _expires_at = DateTime.Now.AddSeconds(value);
            }
        }
        
        public DateTime Expires_at 
        { 
            get { return _expires_at; } 
        }

        public AccessTokenResponse(string accessToken) 
        {
            Access_token = accessToken;
        }

        public AccessTokenResponse() { }
    }
}
