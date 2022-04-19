using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.Dtos
{
    public class OrderDto
    {


        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateRequiered { get; set; }
        public string Comment { get; set; }
        public string Customer { get; set; }
        public List<OrderDetailDto> OrderDetail { get; set; }
        public OrderDto(string orderID, DateTime orderDate, DateTime dateRequiered, string comment, string customer, List<OrderDetailDto> orderDetail)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            DateRequiered = dateRequiered;
            Comment = comment;
            Customer = customer;
            OrderDetail = orderDetail;
        }

        public OrderDto() { }
    }
}
