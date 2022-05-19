using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class TokenType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
    }
}
