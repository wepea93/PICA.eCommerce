
namespace eCommerce.Commons.Objects.Responses.Products
{
    public class ProductReviewResponse
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
