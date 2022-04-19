using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.DbTypes
{
    public class OrderDetailEntity
    {
        [Key]
        public int OrderDetailID { get; set; }
        public string ProductID { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal PriceEach { get; set; }
        public int OrderLine { get; set; }

        public OrderDetailEntity(string pruductID, int quantityOrdered, decimal priceEach, int orderLine)
        {
            ProductID = pruductID;
            QuantityOrdered = quantityOrdered;
            PriceEach = priceEach;
            OrderLine = orderLine;
        }
        public OrderDetailEntity() { }
    }
}
