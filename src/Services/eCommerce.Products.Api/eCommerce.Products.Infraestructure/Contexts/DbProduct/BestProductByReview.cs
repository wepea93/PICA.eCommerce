using System;
using System.Collections.Generic;

namespace eCommerce.Products.Infraestructure.Contexts.DbProduct
{
    public partial class BestProductByReview
    {
        public int Id { get; set; }
        public long? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
