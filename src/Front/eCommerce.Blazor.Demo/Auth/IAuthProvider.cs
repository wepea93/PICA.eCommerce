using eCommerce.Commons.Objects.Responses.AuthorizerUser;

namespace eCommerce.Blazor.Demo.Auth
{
    public interface IAuthProvider
    {
        public Task Login(UserToken token);
        public Task Logout();
    }
}
