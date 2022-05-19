using eCommerce.Blazor.Demo.Services;
using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using eCommerce.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace eCommerce.Services.Services
{
    public class ProductService : BaseHttpClient, IProductService
    {
        private const string Controller = "Product";

        public ProductService(HttpClient http, IConfiguration configuration) : base(http, configuration)
        {
            Audience = configuration["Auth0:Audiences:ProductsAud"].ToString();
            CreateMachinneAccessToken = true;
            CleanAccessToken();
        }

        public async Task<ServiceResponse<ProductCatalogResponse>> GetProductCatalog(ProductCatalogRequest request)
        {
            try
            {
                const string Metodo = "Catalog";
                string uri = SetUriQueryParameters(request,Controller, Metodo);
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
                string uri = SetUriQueryParameters(request, Controller, Metodo);
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
                string uri = SetUriQueryParameters(request, Controller, Metodo);
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
                string uri = SetUriQueryParameters(request, Controller, Metodo);
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
                string uri = SetUriQueryParameters(request, Controller, Metodo);
                return await GetAsync<ServiceResponse<IEnumerable<ProductProviderResponse>>>(uri);
            }
            catch
            {
                throw;
            }
        }
    }
}
