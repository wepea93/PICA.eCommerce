
namespace eCommerce.ShoppingCart.Core.Objects.DbTypes
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int Stock { get; set; }
    }
}
