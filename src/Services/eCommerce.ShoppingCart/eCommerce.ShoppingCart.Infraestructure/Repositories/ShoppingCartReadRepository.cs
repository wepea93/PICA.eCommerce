using eCommerce.Commons.ExtensionMethods.DataTableExt;
using eCommerce.ShoppingCart.Core.Contracts.Repositories;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;
using eCommerce.ShoppingCart.Infraestructure.Contexts;
using eCommerce.ShoppingCart.Infraestructure.Contexts.DbProduct;

namespace eCommerce.ShoppingCart.Infraestructure.Repositories
{
    public class ShoppingCartReadRepository : IShoppingCartReadRepository
    {
        private readonly DbShoppingCartReadContext _dbcontext;
        private readonly DbProductContext _dbProductContext;

        public ShoppingCartReadRepository(DbShoppingCartReadContext dbcontext, DbProductContext dbProductContext)
        {
            _dbcontext = dbcontext;
            _dbProductContext = dbProductContext;
        }

        public async Task<IEnumerable<ShoppingCartEntity>?> GetShoppingCartByUser(string userId)
        {
            return await _dbcontext.SpGetShoppingCartByUserAsync(userId);
        }

        public async Task<IEnumerable<ProductEntity>?> GetShoppingCartProducts(IEnumerable<ProductEntity> productsEntities)
        {
            var tProducts = productsEntities.Select(x => new TProductEntity() { Id = x.Id });
            var dataTable = tProducts.CopyToDataTable();
            return await _dbProductContext.SpGetProductsAsync(dataTable);
        }
    }
}
