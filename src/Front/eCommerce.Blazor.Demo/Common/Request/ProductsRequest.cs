using System.ComponentModel.DataAnnotations;


namespace eCommerce.Blazor.Demo.Common.Request
{
    public class ProductsRequest
    {
        [Required]
        public IEnumerable<long> ProductsCode { get; set; }
    }
}
