using Products.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace Products.Core.Objects.Request
{
    public class ProductRequest
    {
        [ValidateProductCode( ErrorMessage = "The field is required")]
        public long ProductCode { get; set; }
    }
}
