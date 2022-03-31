using System;
using System.Collections.Generic;

namespace Products.Infraestructure.Contexts.DbProduct
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
            ProductReviews = new HashSet<ProductReview>();
            ProductSpecifications = new HashSet<ProductSpecification>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductCategoyId { get; set; }
        public int ProductProviderId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ProductCategory ProductCategoy { get; set; } = null!;
        public virtual ProductProvider ProductProvider { get; set; } = null!;
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
    }
}
