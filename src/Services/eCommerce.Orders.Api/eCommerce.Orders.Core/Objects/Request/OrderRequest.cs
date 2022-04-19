using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Objects.Request
{

    public class OrderRequest
    {
        [Required]
        public string OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DateRequiered { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        public List<OrderDetailRequest> OrderDetail { get; set; }
    }

    public class OrderDetailRequest
    {
        [Required]
        public string ProductID { get; set; }
        [Required]
        public int QuantityOrdered { get; set; }
        [Required]
        public decimal PriceEach { get; set; }
        [Required]
        public int OrderLine { get; set; }
    }
}
