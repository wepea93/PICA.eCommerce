using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class ProductCondition
    {
        public ProductCondition()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Condition { get; set; } = null!;
        public bool Status { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
