using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public bool Status { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
