using eCommerce.Orders.Core.Objects.Dtos;

namespace eCommerce.Orders.Core.Objects.Request
{
    public class OrderSearchRequest
    {
        public string? OrderID { get; set; }

        public string? Customer { get; set; }
    }
}
