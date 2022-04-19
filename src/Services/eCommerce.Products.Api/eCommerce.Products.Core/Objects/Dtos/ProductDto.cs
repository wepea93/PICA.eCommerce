
namespace Products.Core.Objects.Dtos
{
    public class ProductDto
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductCategoryDto Category { get; set; }
        public ProductProviderDto Provider { get; set; }
        public IEnumerable<ProductReviewDto> Reviews { get; set; }
        public IEnumerable<string> OtherImages { get; set; }
        public IEnumerable<string> Detail { get; set; }
        public IEnumerable<ProductSpecificationDto> Specifications { get; set; }
        public decimal Score { get; set; }

        public ProductDto(long code, string name, decimal price, int stock)
        {
            Code = code;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public ProductDto(long code, string name, decimal price, int stock, IEnumerable<ProductReviewDto> reviews)
        {
            Code = code;
            Name = name;
            Price = price;
            Stock = stock;
            Reviews = reviews;
        }

        public ProductDto() { }
    }
}
