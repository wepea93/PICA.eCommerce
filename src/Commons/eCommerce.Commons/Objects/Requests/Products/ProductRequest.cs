using eCommerce.Commons.Validations.Products;

namespace eCommerce.Commons.Objects.Requests.Products
{
    public class ProductRequest
    {
        [ValidateProductCode( ErrorMessage = "The field is required")]
        public long ProductCode { get; set; }
    }
}
