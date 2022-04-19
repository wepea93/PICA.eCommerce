using eCommerce.ShoppingCart.Core.Objects.DbTypes;

namespace eCommerce.ShoppingCart.Core.Contracts.Repositories
{
    public interface IShoppingCartReadRepository
    {
        Task<IEnumerable<ShoppingCartEntity>?> GetShoppingCartByUser(string userId);
        Task<IEnumerable<ProductEntity>?> GetShoppingCartProducts(IEnumerable<ProductEntity> productsEntities);
    }
}
