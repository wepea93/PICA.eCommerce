
namespace Products.Core.Objects.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string Image { get; set; } = null!;
        public IEnumerable<ProductProviderDto> ProductProviders { get; set; }

        public ProductCategoryDto(int id, string category, string image) 
        {
            Id = id;
            Category = category;
            Image = image;
        }

        public ProductCategoryDto() { }
    }
}
