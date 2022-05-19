using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class CustomerPasswordHistory
    {
        public long Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
