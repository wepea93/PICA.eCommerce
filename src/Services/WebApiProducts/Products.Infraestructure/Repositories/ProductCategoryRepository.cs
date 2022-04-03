using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;
using Products.Infraestructure.Contexts.DbProduct;

namespace Products.Infraestructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DbProductsContext _dbcontext;

        public ProductCategoryRepository(DbProductsContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<ProductCategoryEntity> GetProductCategories()
        {
            var categories =  _dbcontext.ProductCategories.Where(x=>x.Status)
                .Select(x => new ProductCategoryEntity
                    {
                        Id = x.Id,
                        Category = x.Category
                    })
                .OrderBy(x=>x.Category);

            return categories;
        }
    }
}
