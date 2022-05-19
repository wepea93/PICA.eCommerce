
namespace eCommerce.Products.Reports.Core.Objects.Dtos
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Stock { get; set; }

        public ProductDto(long productId)
        {
            ProductId = productId;
        }

        public ProductDto() { }
    }
}
