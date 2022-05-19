using eCommerce.Commons.Objects.Requests.Authorizer;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Utilities;
using eCommerce.Commons.Objects.Responses.Authorizer;
using eCommerce.Services.Exceptions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace eCommerce.Blazor.Demo.Services
{
    public class BaseHttpClient
    {
        protected IConfiguration Configuration;
        protected readonly HttpClient Http;
        protected bool CreateMachinneAccessToken { get; set; } = false;
        private AccessTokenResponse? AccessToken { get; set; }
        protected string Audience { get; set; }

        public BaseHttpClient(HttpClient http, IConfiguration configuration)
        {
            this.Http = http;
            Configuration = configuration;
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string request, HttpContent? content = null)
        {
            var msg = new HttpRequestMessage(method, request);
            msg.Content = content;
            return await Http.SendAsync(msg);
        }

        private async Task<string> GetAsync(string request)
        {
            try
            {
                await ValidateAccess();
                var response = await SendAsync(HttpMethod.Get, request);

                if (response.IsSuccessStatusCode)///2++
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var a2 = await response.Content.ReadAsStringAsync();
                    var r = MapBadRequest(GetResponse<ServiceResponse<Dictionary<string, string[]>>>(await response.Content.ReadAsStringAsync()));
                    throw new ServiceException(r, 400);
                }
                else//5xx or otro
                {
                    var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());

                    throw new ServiceException(r, 1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> PostAsync(string request, HttpContent? content)
        {
            try
            {
                await ValidateAccess();
                var response = await SendAsync(HttpMethod.Post, request, content);
                if (response.IsSuccessStatusCode)///2++
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    var r = MapBadRequest(GetResponse<ServiceResponse<Dictionary<string, string[]>>>(resp));

                    throw new ServiceException(r, 400);
                }
                else//5xx or otro
                {
                    var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());

                    throw new ServiceException(r, 1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> PutAsync(string request, HttpContent? content)
        {
            try
            {
                await ValidateAccess();
                var response = await SendAsync(HttpMethod.Put, request, content);
                if (response.IsSuccessStatusCode)///2++
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var r = MapBadRequest(GetResponse<ServiceResponse<Dictionary<string, string[]>>>(await response.Content.ReadAsStringAsync()));

                    throw new ServiceException(r, 400);
                }
                else//5xx or otro
                {
                    var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());

                    throw new ServiceException(r, 1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> DeleteAsync(string request)
        {
            try
            {
                await ValidateAccess();
                var response = await SendAsync(HttpMethod.Delete, request);
                if (response.IsSuccessStatusCode)///2++
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var r = MapBadRequest(GetResponse<ServiceResponse<Dictionary<string, string[]>>>(await response.Content.ReadAsStringAsync()));

                    throw new ServiceException(r, 400);
                }
                else//5xx or otro
                {
                    var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());

                    throw new ServiceException(r, 1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async Task<T> GetAsync<T>(string request) => GetResponse<T>(await GetAsync(request));

        protected async Task<T> PostAsync<T>(string request, HttpContent? content) => GetResponse<T>(await PostAsync(request, content));

        protected async Task<T> PutAsync<T>(string request, HttpContent? content) => GetResponse<T>(await PutAsync(request, content));

        protected async Task<T> DeleteAsync<T>(string request) => GetResponse<T>(await DeleteAsync(request));

        protected T GetResponse<T>(string response)
        {
            var valor = JsonSerializer.Deserialize<T>(response, JsonUtilities.jsonSettings);
            return valor;
        }

        protected ServiceResponseError MapBadRequest(ServiceResponse<Dictionary<string, string[]>> response)
        {
            string Error = string.Empty;
            foreach (var item in response.Response.Values)
            {
                var data = $"{item},{string.Join(',', item?.ToArray())}";
                Debug.WriteLine(data);
                Error += $"{data.Trim(',')}|";
            }

            return new ServiceResponseError(response.Message, Error);
        }

        protected string GetUri(string Controller, string Metodo = "", string queryParameter = "")
        {
            var urlBase = $"{Http.BaseAddress.OriginalString}/{Controller}/{Metodo}".Trim('/');
            return urlBase += queryParameter;
        }

        protected string SetUriQueryParameters<T>(T request, string Controller, string Metodo)
        {
            var json = JsonSerializer.Serialize(request, JsonUtilities.jsonSettings);
            var query = JsonSerializer.Deserialize<Dictionary<string, object>>(json, JsonUtilities.jsonSettings);
            var qb = new QueryBuilder();
            foreach (var kvp in query)
            {
                qb.Add(kvp.Key, kvp.Value.ToString());
            }
            return GetUri(Controller, Metodo, qb.ToQueryString().Value);
        }

        protected StringContent SetJsonContent<T>(T request)
        {
            var content = JsonSerializer.Serialize(request, JsonUtilities.jsonSettings);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        #region Machinne Authentication (AccessToken)
        public Task SetUserAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                AccessToken = new AccessTokenResponse(accessToken);
            }
            return Task.CompletedTask;
        }
        private async Task GenerateMachineAccessToken()
        {
            try
            {
                if (AccessToken == null || (AccessToken != null && AccessToken.Expires_at <= DateTime.Now))
                {
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri(Configuration["Auth0:Authority"].ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string clientId = Configuration["Auth0:Machine:ClientId"].ToString();
                    string clientSecrect = Configuration["Auth0:Machine:ClientSecrect"].ToString();
                    AccessTokenRequest accessTokenReq = new(clientId, clientSecrect, Audience);

                    var response = await client.PostAsJsonAsync(Configuration["Auth0:Machine:Method"].ToString(), accessTokenReq);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = GetResponse<AccessTokenResponse>(await response.Content.ReadAsStringAsync());
                        AccessToken = result;
                    }
                    else
                    {
                        var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());
                        throw new ServiceException(r, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task ValidateAccess()
        {
            if (CreateMachinneAccessToken)
            {
                if (AccessToken == null || (AccessToken != null && string.IsNullOrEmpty(AccessToken.Access_token)))
                    await GenerateMachineAccessToken();

                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Access_token);
            }
            else
            {
                AccessToken = null;
            }
        }
        protected Task CleanAccessToken()
        {
            AccessToken = null;
            return Task.CompletedTask;
        }
        #endregion
    }
}
