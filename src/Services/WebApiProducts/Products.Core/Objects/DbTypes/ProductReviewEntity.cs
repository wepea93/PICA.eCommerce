﻿
namespace Products.Core.Objects.DbTypes
{
    public  class ProductReviewEntity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
