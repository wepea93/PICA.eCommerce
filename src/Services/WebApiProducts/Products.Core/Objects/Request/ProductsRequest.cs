using System.ComponentModel.DataAnnotations;


namespace Products.Core.Objects.Request
{
    public  class ProductsRequest
    {
        [Required]
        public IEnumerable<long> ProductsCode { get; set; }
    }
}
