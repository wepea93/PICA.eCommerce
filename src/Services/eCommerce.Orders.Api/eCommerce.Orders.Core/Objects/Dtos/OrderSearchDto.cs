using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.Dtos
{
    public class OrderSearchDto
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateRequiered { get; set; }
        public DateTime? DateShipped { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public OrderSearchDto(string orderID, DateTime orderDate, DateTime dateRequiered, string comment, string customer, DateTime dateShipped, string status)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            DateRequiered = dateRequiered;
            Comment = comment;
            Customer = customer;
            DateShipped = dateShipped;
            Status = status;
        }

        public OrderSearchDto() { }
    }
}
