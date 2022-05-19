
using eCommerce.Commons.Validations.Products;

namespace eCommerce.Commons.Objects.Requests.Products
{
    public class UpdateProductRequest
    {
        [ValidateProductCode(ErrorMessage = "The field is required")]
        public long ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
