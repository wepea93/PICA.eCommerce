
namespace eCommerce.Commons.Objects.Messaging
{
    public class ProductMsg
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public int Units { get; set; }

        public ProductMsg(long id, decimal price, decimal newPrice, int units) 
        {
            Id = id;
            Price = price;
            NewPrice = newPrice;
            Units = units;
        }

        public ProductMsg(long id)
        {
            Id = id;
        }

        public ProductMsg() { }
    }
}
