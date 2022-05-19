using eCommerce.Blazor.Demo.Services;
using eCommerce.Commons.Objects.Requests.AuthorizerUser;
using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.AuthorizerUser;
using eCommerce.Commons.Objects.Responses.Products;
using eCommerce.Services.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eCommerce.Services.Services
{
    public class AuthorizerUserService : BaseHttpClient, IAuthorizerUserService
    {
        private const string Controller = "Authorizer";

        public AuthorizerUserService(HttpClient http) : base(http)
        {

        }

        public async Task<UserToken> Login(UserInfo request)
        {
            try
            {
                const string Metodo = "login";
                string uri = getUriBase(Controller, Metodo);
                return await PostAsync<UserToken>(uri, setJsonContent(request));
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserToken> CreateUser(UserInfo request)
        {
            try
            {
                const string Metodo = "crear";
                string uri = getUriBase(Controller, Metodo);
                return await PostAsync<UserToken>(uri, setJsonContent(request));
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserToken> Refresh(UserToken request)
        {
            try
            {
                const string Metodo = "refresh";
                string uri = getUriBase(Controller, Metodo);
                return await PostAsync<UserToken>(uri, setJsonContent(request));
            }
            catch
            {
                throw;
            }
        }
    }
}
