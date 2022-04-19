using eCommerce.Commons.Objects.Responses.Products;

namespace eCommerce.Commons.Objects.Responses.ShoppingCart
{
    public class ShoppingCartResponse
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public ProductResponse Product { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal Price { get; set; }
        public string SigDiff { get; set; }
        public decimal PercentDiff { get; set; }
        public int Quantity { get; set; }
        public bool Available { get; set; }

    }
}
