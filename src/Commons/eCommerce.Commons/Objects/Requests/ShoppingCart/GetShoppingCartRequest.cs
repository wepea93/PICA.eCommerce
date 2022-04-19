using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.ShoppingCart
{
    public class GetShoppingCartRequest
    {
        [Required]
        public string CustomerId { get; set; }
    }
}
