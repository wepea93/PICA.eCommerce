
namespace eCommerce.ShoppingCart.Core.Objects.Dtos
{
    public class ShoppingCartDto
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public long ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ShoppingCartDto(string customerId, long productId, decimal initialPrice, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            InitialPrice = initialPrice;
            Quantity = quantity;
        }

        public ShoppingCartDto(string customerId, long productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }

        public ShoppingCartDto(string customerId, long productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }

        public ShoppingCartDto(long id, string customerId, long productId, decimal initialPrice, int quantity)
        {
            Id = id;
            CustomerId = customerId;
            ProductId = productId;
            InitialPrice = initialPrice;
            Quantity = quantity;
        }

        public ShoppingCartDto(long id, string customerId, ProductDto product, decimal initialPrice, int quantity)
        {
            Id = id;
            CustomerId = customerId;
            ProductId = product.Id;
            Product = product;
            InitialPrice = initialPrice;
            Quantity = quantity;
        }
    }
}
