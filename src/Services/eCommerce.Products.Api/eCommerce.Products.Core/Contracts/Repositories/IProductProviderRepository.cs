using Products.Core.Objects.DbTypes;

namespace Products.Core.Contracts.Repositories
{
    public interface IProductProviderRepository
    {
        IEnumerable<ProductProviderEntity> GetProductProvidersByProductCategoryId(int productCategoryId);
        IEnumerable<ProductProviderEntity> GetProductProviders();
    }
}
