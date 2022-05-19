
namespace eCommerce.Commons.Objects.Messaging
{
    public class OrderMsg
    {
        public string UserId { get; set; }
        public IEnumerable<ProductMsg> Products { get; set; }
    }
}
