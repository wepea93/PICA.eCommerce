using eCommerce.Products.Reports.Core.Objects.DbTypes;

namespace eCommerce.Products.Reports.Core.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>?> GetProductsByIdAsync(IEnumerable<ProductEntity> productsEntities);
    }
}
