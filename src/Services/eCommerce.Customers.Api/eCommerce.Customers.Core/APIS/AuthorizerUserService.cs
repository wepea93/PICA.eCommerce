using eCommerce.Commons.Objects.Requests.User;
using eCommerce.Commons.Objects.Responses.User;
using eCommerce.Customers.Core.APIS.Contracts;
using eCommerce.Customers.Core.Config;
using eCommerce.Customers.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace eCommerce.Customers.Core.APIS
{
    public class AuthorizerUserService : IAuthorizerUserService
    {
        private readonly ILogger<AuthorizerUserService> _logger;
        private readonly IAuthenticateService _athenticateService;
        private string _urlBase { get; set; } = AppConfiguration.Configuration["AppConfiguration:Auth0:Url"].ToString();

        public AuthorizerUserService(ILogger<AuthorizerUserService> logger, IAuthenticateService athenticateService)
        {
            _logger = logger;
            _athenticateService = athenticateService;
        }

        public async Task<UserResponse?> CreateUser(string userName, string password) 
        {
            var apiAutorizedMethod = AppConfiguration.Configuration["AppConfiguration:Auth0:Url"].ToString();

            var client = new RestClient(_urlBase);
            var requestService = new RestRequest(apiAutorizedMethod, Method.Post);

            string audience = AppConfiguration.Configuration["AppConfiguration:Authentication:Audience"].ToString();
            var accessToken = await _athenticateService.GetBearerTokenAsync(audience);

            var bearerToken = "Bearer "  + accessToken ;
            bearerToken = bearerToken.Replace('|', '"');
            requestService.AddHeader("Authorization", bearerToken);

            var request = new UserRequest()
            {
                 Email = userName,
                 Password = password
            };

            requestService.AddJsonBody(request);

            var response = await client.ExecutePostAsync(requestService);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<UserResponse>(response.Content);

            var message = "Internal server error: {@error}";
            var error = new 
            { 
                errorMessage = "Error al crear el usaurio", 
                request = JsonConvert.SerializeObject(request), 
                response = JsonConvert.SerializeObject(response.Content)
            };
            _logger.LogError(message, error);

            throw new CreateUserException();
        }   
    }
}
