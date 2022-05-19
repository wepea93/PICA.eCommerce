using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class CustomerAddress
    {
        public long Id { get; set; }
        public string CustomertId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customert { get; set; } = null!;
    }
}
