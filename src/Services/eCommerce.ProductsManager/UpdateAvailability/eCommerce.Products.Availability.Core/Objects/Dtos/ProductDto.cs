
namespace eCommerce.Products.Availability.Core.Objects.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ProductDto(long id, decimal price, int stok)
        {
            Id = id;
            Price = price;
            Stock = stok;
        }

        public ProductDto() { }
    }
}
