using eCommerce.Commons.Objects.Responses;
using eCommerce.Services.Exceptions;
using Newtonsoft.Json;
using System.Diagnostics;

namespace eCommerce.Blazor.Demo.Services
{
    public class BaseHttpClient
    {
        protected readonly HttpClient http;
        public BaseHttpClient(HttpClient http)
        {
            this.http = http;
        }


        protected async Task<string> GetAsync(string request)
        {
            try
            {
                var response = await http.GetAsync(request);
                if (response.IsSuccessStatusCode)///2++
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var r = MapBadRequest(GetResponse<ServiceResponse<Dictionary<string, string[]>>>(await response.Content.ReadAsStringAsync()));

                    throw new ServiceException(r,400);
                }
                else//5xx or otro
                {
                    var r = GetResponse<ServiceResponseError>(await response.Content.ReadAsStringAsync());

                    throw new ServiceException(r,1);
                }
            }
            catch
            {
                throw;
            }
        }
        protected async Task<T> GetAsync<T>(string request)
        {
            return GetResponse<T>(await GetAsync(request));
        }

        protected T GetResponse<T>(string response)
        {
            Debug.WriteLine(response);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected ServiceResponseError MapBadRequest(ServiceResponse<Dictionary<string, string[]>> response)
        {
            string Error = string.Empty;
            foreach (var item in response.Response.Values)
            {
                string data = $"{item},{string.Join(',', item?.ToArray())}";
                Debug.WriteLine(data);
                Error += $"{data.Trim(',')}|";
            }

            return new ServiceResponseError(response.Message, Error);
        }
    }
}
