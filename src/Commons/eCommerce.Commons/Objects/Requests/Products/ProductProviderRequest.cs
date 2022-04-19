using eCommerce.Commons.Validations.Products;

namespace eCommerce.Commons.Objects.Requests.Products
{
    public class ProductProviderRequest
    {
        [ValidateProductCategory(ErrorMessage = "The field is required")]
        public int ProductCategoryId { get; set; }
    }
}
