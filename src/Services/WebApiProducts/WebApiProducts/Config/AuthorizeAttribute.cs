using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Products.Core.Config;
using Products.Core.Objects.Responses;
using RestSharp;

namespace WebApiProducts.Config
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

                if (allowAnonymous) return;

                var token = context.HttpContext.Request.Headers["Authorization"];

                var isAuthorized = IsAutorized(token);

                var UnauthorizedResponse = new ServiceResponse<string>("Unauthorized", null);

                if (!isAuthorized)
                {
                    context.Result = new JsonResult(UnauthorizedResponse)
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                    return;
                }
                return;
            }
            catch (Exception)
            {
                var UnauthorizedResponse = new ServiceResponse<string>("Unauthorized", null);
                context.Result = new JsonResult(UnauthorizedResponse)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }

        private static bool IsAutorized(string token)
        {
            var apiCode = AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString();
            var apiAutorizedUrl = AppConfiguration.Configuration["AppConfiguration:ApiAutorizedUrl"].ToString();
            var apiAutorizedMethod = AppConfiguration.Configuration["AppConfiguration:ApiAutorizedMethod"].ToString();

            var client = new RestClient(apiAutorizedUrl);
            var requestService = new RestRequest(apiAutorizedMethod, Method.Post);

            requestService.AddParameter("accessToken", token);
            requestService.AddParameter("user", apiCode);

            var response = client.ExecuteAsync(requestService);
            return response.Result.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
