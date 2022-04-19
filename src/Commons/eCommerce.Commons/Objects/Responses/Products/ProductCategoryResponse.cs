
namespace eCommerce.Commons.Objects.Responses.Products
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public IEnumerable<ProductProviderResponse> ProductProviders { get; set; }

        public ProductCategoryResponse(int id, string category, string image)
        {
            Id = id;
            Category = category;
            Image = image;
        }

        public ProductCategoryResponse() { }
    }
}
