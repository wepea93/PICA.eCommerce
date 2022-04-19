using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.ShoppingCart
{
    public class DeleteShoppingCartRequest
    {
        [Required]
        public string CustomerId { get; set; }
        public long ProductId { get; set; }
    }
}
