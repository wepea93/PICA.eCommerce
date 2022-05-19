
namespace eCommerce.Products.Availability.Core.Objects.DbTypes
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ProductEntity(long id, decimal price, int stok) 
        {
            Id = id;
            Price = price;
            Stock = stok;
        }

        public ProductEntity() { }
    }
}
