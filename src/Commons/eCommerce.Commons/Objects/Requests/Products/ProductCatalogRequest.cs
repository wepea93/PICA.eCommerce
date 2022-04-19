using eCommerce.Commons.Validations.Products;

namespace eCommerce.Commons.Objects.Requests.Products
{
    public class ProductCatalogRequest
    {
        [ValidateProductCategory(ErrorMessage = "The field is required")]
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        
        [ValidateSearch(ErrorMessage = "The minimum length allowed is 4 characters")]
        public string? Search { get; set; }
        public int Page { get; set; }
        public int ItemsByPage { get; set; }
        
        [ValidateSort(ErrorMessage = "The field is required. Allowed values (MaxPrice, MinPrice, Relevance)")]
        public string Sort { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        [ValidateProductCondition(ErrorMessage = "The field is required. Allowed values (New, Used, Returned)")]
        public string Condition { get; set; }
    }
}
