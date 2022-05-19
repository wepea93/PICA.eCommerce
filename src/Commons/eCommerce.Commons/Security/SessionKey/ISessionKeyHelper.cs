
namespace eCommerce.Commons.Security.SessionKey
{
    public interface ISessionKeyHelper
    {
        string GenerateSessionKeyToken(string userId, string sessionKey);
        string GetSessionKey(string sessionKeyToken);
        string GetUserId(string sessionKeyToken);
    }
}
