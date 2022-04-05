
namespace Products.Core.Objects.Responses
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public ProductCategoryResponse(int id, string category)
        {
            Id = id;
            Category = category;
        }

        public ProductCategoryResponse() { }
    }
}
