
namespace Products.Core.Objects.Dtos
{
    public class ProductReviewDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductReviewDto(string userId, string userName, string review, int value, DateTime createdAt)
        {
            userId = userId;
            UserName = userName;
            Review = review;
            Value = value;
            CreatedAt = createdAt;
        }

        public ProductReviewDto() { }
    }
}
