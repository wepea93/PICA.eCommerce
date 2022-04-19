using System.ComponentModel.DataAnnotations;

namespace eCommerce.ShoppingCart.Core.Objects.DbTypes
{
    public class ShoppingCartEntity
    {
        [Key]
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public long ProductId { get; set; }
        public decimal InitialPrice { get; set; }
        public int Quantity { get; set; }

        public ShoppingCartEntity(string customerId, long productId, decimal initialPrice, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            InitialPrice = initialPrice;
            Quantity = quantity;
        }

        public ShoppingCartEntity(string customerId, long productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }

        public ShoppingCartEntity() { }
    }
}
