﻿
namespace Products.Core.Objects.DbTypes
{
    public  class ProductReviewEntity
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }


        public ProductReviewEntity(long productId, string userId, string userName, string review, int value, DateTime createdAt)
        {
            ProductId = productId;
            UserId = userId;
            UserName = userName;
            Review = review;
            Value = value;
            CreatedAt = createdAt;
        }

        public ProductReviewEntity(long productId, string userId, string userName, string review, int value)
        {
            ProductId = productId;
            UserId = userId;
            UserName = userName;
            Review = review;
            Value = value;
        }

        public ProductReviewEntity() { }
    }
}
