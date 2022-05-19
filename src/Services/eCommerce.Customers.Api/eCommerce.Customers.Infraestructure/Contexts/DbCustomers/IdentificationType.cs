using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class IdentificationType
    {
        public IdentificationType()
        {
            Customers = new HashSet<Customer>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
