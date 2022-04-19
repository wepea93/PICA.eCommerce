using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;

namespace eCommerce.Services.Contracts
{
    public interface IProductService
    {
        public Task<ServiceResponse<ProductCatalogResponse>> GetProductCatalog(ProductCatalogRequest request);

        public Task<ServiceResponse<ProductResponse>> GetProduct(ProductRequest request);

        public Task<ServiceResponse<IEnumerable<ProductResponse>>> GetProducts(ProductsRequest request);

        public Task<ServiceResponse<IEnumerable<ProductCategoryResponse>>> GetProductCategories(ProductCategoryRequest request);

        public Task<ServiceResponse<IEnumerable<ProductProviderResponse>>> GetProductProviders(ProductProviderRequest request);
    }
}
