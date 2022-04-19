using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.Dtos
{
    public class OrderDetailDto
    {
        public string ProductID { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal PriceEach { get; set; }
        public int OrderLine { get; set; }

        public OrderDetailDto(string productID, int quantityOrdered, decimal priceEach, int orderLine)
        {
            ProductID = productID;
            QuantityOrdered = quantityOrdered;
            PriceEach = priceEach;
            OrderLine = orderLine;
        }
        public OrderDetailDto() { }
    }
}
