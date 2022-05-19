using eCommerce.Products.Reports.Core.Objects.DbTypes;

namespace eCommerce.Products.Reports.Core.Contracts.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<CustomerByProductEntity>?> GetCustomersByProductsIdAsync(IEnumerable<ProductEntity> productEntities);
    }
}
