using Products.Core.Objects.DbTypes;

namespace Products.Core.Contracts.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategoryEntity> GetProductCategories();
        IEnumerable<ProductCategoryEntity> GetProductCategoriesWithProviders();
    }
}
