
using Products.Core.Objects.DbTypes;

namespace Products.Core.Contracts.Repositories
{
    public interface IProductReviewRepository
    {
        void CreateProductReviewAsync(ProductReviewEntity productReviewEntity);
    }
}
