using System.ComponentModel.DataAnnotations;


namespace eCommerce.Commons.Objects.Requests.Products
{
    public  class ProductsRequest
    {
        [Required]
        public IEnumerable<long> ProductsCode { get; set; }
    }
}
