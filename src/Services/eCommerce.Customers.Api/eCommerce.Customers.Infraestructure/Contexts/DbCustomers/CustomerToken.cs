using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class CustomerToken
    {
        public long Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int TokenType { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
