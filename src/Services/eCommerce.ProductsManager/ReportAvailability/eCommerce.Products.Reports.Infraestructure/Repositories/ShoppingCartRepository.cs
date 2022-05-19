using eCommerce.Commons.ExtensionMethods.DataTableExt;
using eCommerce.Products.Reports.Core.Contracts.Repositories;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using eCommerce.Products.Reports.Infraestructure.Contexts.DbCore;

namespace eCommerce.Products.Reports.Infraestructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DbCoreContext _dbcontext;

        public ShoppingCartRepository(DbCoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<CustomerByProductEntity>?> GetCustomersByProductsIdAsync(IEnumerable<ProductEntity> productEntities)
        {
            var dataTable = productEntities.CopyToDataTable();
            return await _dbcontext.SpGetUsersByProductsIdAsync(dataTable);
        }
    }
}
