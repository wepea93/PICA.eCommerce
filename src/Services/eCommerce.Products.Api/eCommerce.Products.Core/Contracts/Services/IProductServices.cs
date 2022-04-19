using Products.Core.Objects.Dtos;

namespace Products.Core.Contracts.Services
{
    public interface IProductServices
    {
        Task<ProductDto> GetProduct(long productCode);
        ProductCatalogDto GetProductCatalog(ProductSearchDto productSearch);
        IEnumerable<ProductDto> GetProducts(IEnumerable<long> productsId);
        IEnumerable<ProductCategoryDto>? GetProductCategories(bool providerRequired);
        IEnumerable<ProductProviderDto>? GetProductProviders(int productCategoryId);
        Task CreateProductReview(ProductReviewDto productReview);
    }
}
