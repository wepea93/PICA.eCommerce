using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;

namespace Products.Infraestructure.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly DbProductsContext _dbcontext;

        public ProductReviewRepository(DbProductsContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async void CreateProductReviewAsync(ProductReviewEntity productReviewEntity)
        {
            var review = new ProductReview
            {
                ProductId = productReviewEntity.ProductId,
                UserId = productReviewEntity.UserId,
                UserName = productReviewEntity.UserName,
                Review = productReviewEntity.Review,
                Value = productReviewEntity.Value,
                Status = true,
                CreatedAt = DateTime.Now
            };
            await _dbcontext.ProductReviews.AddAsync(review);
        }
    }
}
