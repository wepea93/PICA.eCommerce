using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Products.Reports.Core.Objects.Dtos
{
    public class ReportDto
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ProductName { get; set; }
        public decimal ProductOldPrice { get; set; }
        public decimal ProductNewPrice { get; set; }
    }
}
