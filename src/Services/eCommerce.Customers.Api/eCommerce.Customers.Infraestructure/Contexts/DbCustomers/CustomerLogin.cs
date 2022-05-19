using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class CustomerLogin
    {
        public long Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? Ip { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
