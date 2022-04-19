using eCommerce.Blazor.Demo.Services;
using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using eCommerce.Services.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eCommerce.Services.Services
{
    public class ProductService : BaseHttpClient, IProductService
    {
        private const string Controller = "Product";

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public ProductService(HttpClient http) : base(http)
        {

        }

        public async Task<ServiceResponse<ProductCatalogResponse>> GetProductCatalog(ProductCatalogRequest request)
        {
            try
            {
                const string Metodo = "Catalog";
                string uri = setGetParametert(request, Metodo);
                return await GetAsync<ServiceResponse<ProductCatalogResponse>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<ProductResponse>> GetProduct(ProductRequest request)
        {
            try
            {
                const string Metodo = "Detail";
                string uri = setGetParametert(request, Metodo);
                return await GetAsync<ServiceResponse<ProductResponse>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductResponse>>> GetProducts(ProductsRequest request)
        {
            try
            {
                const string Metodo = "List";
                string uri = setGetParametert(request, Metodo);
                return await GetAsync<ServiceResponse<IEnumerable<ProductResponse>>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductCategoryResponse>>> GetProductCategories(ProductCategoryRequest request)
        {
            try
            {
                const string Metodo = "Categories";
                string uri = setGetParametert(request, Metodo);
                return await GetAsync<ServiceResponse<IEnumerable<ProductCategoryResponse>>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductProviderResponse>>> GetProductProviders(ProductProviderRequest request)
        {
            try
            {
                const string Metodo = "Providers";
                string uri = setGetParametert(request, Metodo);
                return await GetAsync<ServiceResponse<IEnumerable<ProductProviderResponse>>>(uri);
            }
            catch
            {
                throw;
            }
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
