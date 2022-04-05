using eCommerce.Blazor.Demo.Common.Request;
using eCommerce.Blazor.Demo.Common.Responses;

namespace eCommerce.Blazor.Demo.Contracts
{
    public interface IProductService
    {
        public Task<ServiceResponse> GetProductCatalog(ProductCatalogRequest request);

        public Task<ServiceResponse> GetProduct(ProductRequest request);

        public Task<ServiceResponse> GetProducts(ProductsRequest request);
    }
}
