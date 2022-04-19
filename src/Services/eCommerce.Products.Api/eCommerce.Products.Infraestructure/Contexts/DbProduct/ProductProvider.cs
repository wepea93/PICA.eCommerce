using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class ProductProvider
    {
        public ProductProvider()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Provider { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
