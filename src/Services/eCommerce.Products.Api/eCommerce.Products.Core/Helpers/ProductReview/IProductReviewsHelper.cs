
using Products.Core.Objects.DbTypes;

namespace Products.Core.Helpers.ProductReview
{
    public  interface IProductReviewsHelper
    {
        decimal CalculateScore(IList<ProductReviewEntity> reviews);
    }
}
