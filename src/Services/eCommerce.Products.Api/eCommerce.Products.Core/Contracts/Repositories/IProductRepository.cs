using eCommerce.Commons.Utilities;
using Products.Core.Objects.DbTypes;

namespace Products.Core.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProductById(long productId);
        ProductCatalogEntity GetProducts(int categoryId, int providerId, string search, int page,
            int itemsByPage, ProductUtilities.ORDERBY sort, decimal minPrice, decimal maxPrice, IEnumerable<ProductUtilities.CONDITIONCODE> condition);
        IEnumerable<ProductEntity> GetProductsById(IEnumerable<long> productsId);
    }
}
