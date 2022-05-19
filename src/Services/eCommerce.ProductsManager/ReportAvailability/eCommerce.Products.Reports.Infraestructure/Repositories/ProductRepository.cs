using eCommerce.Products.Reports.Core.Contracts.Repositories;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using eCommerce.Products.Reports.Infraestructure.Contexts.DbProducts;
using eCommerce.Commons.ExtensionMethods.DataTableExt;

namespace eCommerce.Products.Reports.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbProductContext _dbcontext;

        public ProductRepository(DbProductContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<ProductEntity>?> GetProductsByIdAsync(IEnumerable<ProductEntity> productsEntities)
        {
            var dataTable = productsEntities.CopyToDataTable();
            return await _dbcontext.SpGetProductsByIdAsync(dataTable);
        }
    }
}
