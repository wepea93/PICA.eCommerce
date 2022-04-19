using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;

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
                                .Where(x => x.Products.Any(x=>x.ProductCategoryId == productCategoryId) && x.Status)
                                .Distinct()
                                .Select(x => new ProductProviderEntity
                                {
                                    Id = x.Id,
                                    Provider = x.Provider
                                })
                                .OrderBy(x => x.Provider);

            return providers;
        }

        public IEnumerable<ProductProviderEntity> GetProductProviders()
        {
            var providers = from provider in _dbcontext.ProductProviders
                            join product in _dbcontext.Products on provider.Id equals product.ProductProviderId
                            where provider.Status == true && product.Status == true
                            group provider by new { provider.Id, provider.Provider, product.ProductCategoryId } into grouping
                            orderby grouping.Key.Provider
                            select new ProductProviderEntity
                            {
                                Id = grouping.Key.Id,
                                Provider = grouping.Key.Provider,
                                ProductCategoryId = grouping.Key.ProductCategoryId
                            };

            return providers;
        }

    }
}
