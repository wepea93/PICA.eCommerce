using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;
using Products.Infraestructure.Contexts.DbProduct;

namespace Products.Infraestructure.Repositories
{
    public class ProductProviderRepository : IProductProviderRepository
    {
        private readonly DbProductsContext _dbcontext;

        public ProductProviderRepository(DbProductsContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<ProductProviderEntity> GetProductProvidersByProductCategoryId(int productCategoryId)
        {
            var providers = _dbcontext.ProductProviders
                                .Where(x => x.Products.Any(x=>x.ProductCategoyId == productCategoryId) && x.Status)
                                .Distinct()
                                .Select(x => new ProductProviderEntity
                                {
                                    Id = x.Id,
                                    Provider = x.Provider
                                })
                                .OrderBy(x => x.Provider);

            return providers;
        }
    }
}
