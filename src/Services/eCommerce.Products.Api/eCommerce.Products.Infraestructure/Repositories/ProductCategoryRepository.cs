using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;

namespace Products.Infraestructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DbProductsContext _dbcontext;
        private readonly IProductProviderRepository _productProviderRepository;

        public ProductCategoryRepository(DbProductsContext dbcontext, IProductProviderRepository productProviderRepository)
        {
            _dbcontext = dbcontext;
            _productProviderRepository = productProviderRepository;
        }

        public IEnumerable<ProductCategoryEntity> GetProductCategories()
        {
            var categories =  _dbcontext.ProductCategories.Where(x=>x.Status)
                .Select(x => new ProductCategoryEntity
                    {
                        Id = x.Id,
                        Category = x.Category,
                        Image = x.Image,
                    })
                .OrderBy(x=>x.Category);

            return categories;
        }

        public IEnumerable<ProductCategoryEntity> GetProductCategoriesWithProviders()
        {
            var providers = _productProviderRepository.GetProductProviders();
            var categories = _dbcontext.ProductCategories.Where(x => x.Status)
                .Select(x => new ProductCategoryEntity
                {
                    Id = x.Id,
                    Category = x.Category,
                    Image = x.Image,
                    ProductProviderEntities = providers.Where(y=>y.ProductCategoryId == x.Id).OrderBy(x=>x.Provider).ToList()
                })
                .OrderBy(x => x.Category);

            return categories;
        }
    }
}
