using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.ShoppingCart
{
    public class CreateShoppingCartRequest
    {
        [Required]
        public IEnumerable<CreateShoppingItemCartRequest> ShoppingCartItems { get; set; }
    }

    public class CreateShoppingItemCartRequest
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
