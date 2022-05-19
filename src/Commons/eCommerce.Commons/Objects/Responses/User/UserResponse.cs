
namespace eCommerce.Commons.Objects.Responses.User
{
    public class UserResponse
    {
        public bool Result { get; set; }
        public string UserId { get; set; }
        public string SessionKey {get; set;}
        public string SessionExpiredAt { get; set;}

        public UserResponse(bool result, string sessionKey) 
        {
            Result = result;
            SessionKey = sessionKey;
        }

        public UserResponse(bool result, string userId, string sessionKey, string sessionExpiredAt)
        {
            Result = result;
            UserId = userId;
            SessionKey = sessionKey;
            SessionExpiredAt = sessionExpiredAt;
        }

        public UserResponse() { }
    }
}
