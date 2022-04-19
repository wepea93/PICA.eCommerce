using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.ShoppingCart
{
    public class UpdateShoppingCartRequest
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
