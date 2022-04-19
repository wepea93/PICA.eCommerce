using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;
using eCommerce.ShoppingCart.Core.Config;
using eCommerce.Commons.Objects.Responses;

namespace WebApiProducts.Config
{
    public class AuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        public AuthorizationPolicy Policy { get; }

        public AuthorizationFilter()
        {
            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                // Allow Anonymous skips all authorization
                if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                {
                    return;
                }

                var token = context.HttpContext.Request.Headers["Authorization"];
                var isAuthorized = await IsAutorized(token);

                if (!isAuthorized)
                {
                    context.Result = GetErrorResponse();
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = GetErrorResponse();
            }
        }

        private async Task<bool> IsAutorized(string token)
        {
            var apiCode = AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString();
            var apiAutorizedUrl = AppConfiguration.Configuration["AppConfiguration:ApiAutorizedUrl"].ToString();
            var apiAutorizedMethod = AppConfiguration.Configuration["AppConfiguration:ApiAutorizedMethod"].ToString();

            var client = new RestClient(apiAutorizedUrl);
            var requestService = new RestRequest(apiAutorizedMethod, Method.Post);

            var authObject = new Authenticate(token, apiCode);

            requestService.AddJsonBody(authObject);

            var response = await client.ExecutePostAsync(requestService);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private JsonResult GetErrorResponse()
        {
            var UnauthorizedResponse = new ServiceResponse<string>("Unauthorized", null);
            return new JsonResult(UnauthorizedResponse)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        private class Authenticate
        {
            public string AccessToken { get; set; }
            public string AppCode { get; set; }

            public Authenticate(string accessToken, string appCode)
            {
                AccessToken = accessToken;
                AppCode = appCode;
            }
        }
    }
}
