using eCommerce.Blazor.Demo.Common.Validations;

namespace eCommerce.Blazor.Demo.Common.Request
{
    public class ProductRequest
    {
        [ValidateProductCode(ErrorMessage = "The field is required")]
        public long ProductCode { get; set; }
    }
}
