using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class ProductDetail
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
