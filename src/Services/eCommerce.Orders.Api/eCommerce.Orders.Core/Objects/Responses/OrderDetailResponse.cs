using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.Responses
{
    public class OrderDetailResponse
    {
        public string ProductID { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal PriceEach { get; set; }
        public int OrderLine { get; set; }

        public OrderDetailResponse(string productID, int quantityOrdered, decimal priceEach, int orderLine)
        {
            ProductID = productID;
            QuantityOrdered = quantityOrdered;
            PriceEach = priceEach;
            OrderLine = orderLine;
        }
        public OrderDetailResponse() { }
    }
}
