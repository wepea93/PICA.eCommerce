using Products.Core.Validations;

namespace Products.Core.Objects.Request
{
    public class ProductProviderRequest
    {
        [ValidateProductCategory(ErrorMessage = "The field is required")]
        public int ProductCategoryId { get; set; }
    }
}
