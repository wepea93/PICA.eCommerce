using eCommerce.Products.Availability.Core.Objects.DbTypes;

namespace eCommerce.Products.Availability.Core.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProduct(long productId);
        Task<bool> UpdateProduct(ProductEntity productEntity);
        Task<bool> RemoveUnitsToProductStock(IEnumerable<ProductEntity> productEntity);
    }
}
