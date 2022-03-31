using Products.Core.Objects.DbTypes;
using Products.Core.Utilities;

namespace Products.Core.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProductById(long productId);
        ProductCatalogEntity GetProducts(int categoryId, int providerId, string search, int page,
            int itemsByPage, UtilitiesHelper.ORDERBY sort, decimal minPrice, decimal maxPrice);
        IEnumerable<ProductEntity> GetProductsById(IEnumerable<long> productsId);
    }
}
