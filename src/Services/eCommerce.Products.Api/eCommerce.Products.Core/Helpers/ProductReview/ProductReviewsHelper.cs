using Products.Core.Objects.DbTypes;

namespace Products.Core.Helpers.ProductReview
{
    public  class ProductReviewsHelper : IProductReviewsHelper
    {
        public decimal CalculateScore(IList<ProductReviewEntity> reviews)
        {
            var score = 0;
            var total = reviews.Count();

            if (total == 0) return 0; 

            var s5 = reviews.Count(x => x.Value == 5);
            var s4 = reviews.Count(x => x.Value == 4);
            var s3 = reviews.Count(x => x.Value == 3);
            var s2 = reviews.Count(x => x.Value == 2);
            var s1 = reviews.Count(x => x.Value == 1);

            var s5P = (s5 * 100) / total;
            var s4P = (s4 * 100) / total;
            var s3P = (s3 * 100) / total;
            var s2P = (s2 * 100) / total;
            var s1P = (s1 * 100) / total;

            var divisor = s5P + s4P + s3P + s2P + s1P;

            if (divisor == 0) return 0;

            score = (s5P * 5 + s4P * 4 + s3P * 3 + s2P * 2 + s1P * 1) / (s5P + s4P + s3P + s2P + s1P);
            return score;
        }
    }
}
