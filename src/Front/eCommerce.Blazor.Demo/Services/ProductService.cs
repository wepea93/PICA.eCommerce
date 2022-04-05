using eCommerce.Blazor.Demo.Common.Request;
using eCommerce.Blazor.Demo.Common.Responses;
using eCommerce.Blazor.Demo.Contracts;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace eCommerce.Blazor.Demo.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient http;

        private const string Controller = "Product";

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public ProductService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ServiceResponse> GetProductCatalog(ProductCatalogRequest request)
        {
            try
            {
                const string Metodo = "Catalog";
                string uri = setGetParametert(request, Metodo);
                var response = await http.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync());               
                return new ServiceResponse(
                        obj["message"].ToString().Trim('\'')
                      , obj["response"].ToString()
                     );

                //return JsonConvert.DeserializeObject<ServiceResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ServiceResponse("Error").SetResponse(ex);
            }

        }

        public async Task<ServiceResponse> GetProduct(ProductRequest request)
        {
            return new ServiceResponse("Error").SetResponse(new NotImplementedException("Metodo no implentado"));
        }

        public async Task<ServiceResponse> GetProducts(ProductsRequest request)
        {
            return new ServiceResponse("Error").SetResponse(new NotImplementedException("Metodo no implentado"));
        }

        private string getUriBase(string Metodo)
        {
            return $"{http.BaseAddress}/{Controller}/{Metodo}";
        }

        private string setGetParametert<T>(T request, string Metodo)
        {
            var json = JsonConvert.SerializeObject(request, jsonSettings);
            var query = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var uri = QueryHelpers.AddQueryString(this.getUriBase(Metodo), query);
            return uri;
        }
    }
}
