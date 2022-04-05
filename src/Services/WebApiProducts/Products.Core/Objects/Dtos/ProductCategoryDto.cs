
namespace Products.Core.Objects.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;

        public ProductCategoryDto(int id, string category) 
        {
            Id = id;
            Category = category;
        }

        public ProductCategoryDto() { }
    }
}
