using eCommerce.Blazor.Demo.LocalStorage;
using eCommerce.Services.Services;
using System.Net.Http.Headers;

namespace eCommerce.Blazor.Demo.Auth
{
    public class AuthHttpMessageHandler : DelegatingHandler
    {
        private TokenJwtLocalStorage _localStorage;
        public AuthHttpMessageHandler(TokenJwtLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.Contains("/Authorizer/"))
            {
                //TODO: obtiene el token anonino para loguear la api
                //request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            var token = await _localStorage.GetToken();
            if (token == null)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
