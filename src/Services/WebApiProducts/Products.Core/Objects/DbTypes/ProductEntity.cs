using System.ComponentModel.DataAnnotations;

namespace Products.Core.Objects.DbTypes
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ProductCategoryEntity ProductCategoy { get; set; } = null!;
        public virtual ProductProviderEntity ProductProvider { get; set; } = null!;
        public virtual IEnumerable<ProductDetailEntity> ProductDetails { get; set; }
        public virtual IEnumerable<ProductSpecificationEntity> ProductSpecifications { get; set; }
        public virtual IEnumerable<ProductReviewEntity> ProductReviews { get; set; }

        public ProductEntity() { }
    }
}
