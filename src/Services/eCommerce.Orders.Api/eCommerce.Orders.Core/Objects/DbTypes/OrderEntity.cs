using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.DbTypes
{
    public class OrderEntity
    {
        [Key]
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateRequiered { get; set; }
        public DateTime? DateShipped { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public OrderEntity(string orderID, DateTime orderDate, DateTime dateRequiered, string comment, string customer,string status)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            DateRequiered = dateRequiered;
            Comment = comment;
            Customer = customer;
            Status = status;
            
        }
        public OrderEntity() { }
    }
}
