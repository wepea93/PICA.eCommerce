using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class Product
    {
        public Product()
        {
            BestProductByReviews = new HashSet<BestProductByReview>();
            ProductDetails = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImage>();
            ProductReviews = new HashSet<ProductReview>();
            ProductSpecifications = new HashSet<ProductSpecification>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductProviderId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? Score { get; set; }
        public int ProductConditionId { get; set; }
        public int? Stock { get; set; }

        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ProductCondition ProductCondition { get; set; } = null!;
        public virtual ProductProvider ProductProvider { get; set; } = null!;
        public virtual ICollection<BestProductByReview> BestProductByReviews { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
    }
}
