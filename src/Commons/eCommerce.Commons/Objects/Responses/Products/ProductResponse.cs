
namespace eCommerce.Commons.Objects.Responses.Products
{
    public  class ProductResponse
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<string> OtherImages { get; set; }
        public IEnumerable<string> Description { get; set; }
        public decimal Price { get; set; }
        public string Score { get; set; }
        public int Stock { get; set; }
        public ProductCategoryResponse Category { get; set; }
        public ProductProviderResponse Provider { get; set; }
        public IEnumerable<ProductSpecificationResponse> Specifications { get; set; }
        public IEnumerable<ProductReviewResponse> Reviews { get; set; }

        public ProductResponse() 
        {
        
        }
    }
}
