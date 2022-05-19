
using eCommerce.Commons.Objects.Requests.Authorizer;
using eCommerce.Commons.Objects.Responses.Authorizer;
using eCommerce.Customers.Core.APIS.Contracts;
using eCommerce.Customers.Core.Config;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace eCommerce.Customers.Core.APIS
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger<AuthenticateService> _logger;
        private string _urlBase { get; set; } = AppConfiguration.Configuration["AppConfiguration:Auth0:Url"].ToString();
        private string _clientId { get; set; } = AppConfiguration.Configuration["AppConfiguration:Auth0:ClientId"].ToString();
        private string _clientSecret { get; set; } = AppConfiguration.Configuration["AppConfiguration:Auth0:ClientSecret"].ToString();

        public AuthenticateService(ILogger<AuthenticateService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetBearerTokenAsync(string audience) 
        {
            var authMethod = AppConfiguration.Configuration["AppConfiguration:Auth0:AuthMethod"].ToString();

            var client = new RestClient(_urlBase);
            var requestService = new RestRequest(authMethod, Method.Post);
            var request = new AccessTokenRequest(_clientId, _clientSecret, audience);

            
         //   request.sessionKey = "asd@asd.com";

         //   var a = JsonConvert.SerializeObject(request);
         //   a = a.Replace("sessionKey", "https://mydomain.com/some-key");
         //  requestService.AddHeader("Content-type", "application/json");
            requestService.AddJsonBody(request);

           //  requestService.AddJsonBody(a, "application/json; charset=utf-8");

            var response = await client.ExecutePostAsync(requestService);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content);
                return result.Access_token;
            }
            return string.Empty;
        }
    }
}
