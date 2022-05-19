using System;
using System.Collections.Generic;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            CustomerLogins = new HashSet<CustomerLogin>();
            CustomerPasswordHistories = new HashSet<CustomerPasswordHistory>();
            CustomerTokens = new HashSet<CustomerToken>();
        }

        public string Id { get; set; } = null!;
        public string IdentTypeId { get; set; } = null!;
        public string Identification { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string LastName { get; set; } = null!;
        public string? SecondLastName { get; set; }
        public string Email { get; set; } = null!;
        public string Phone1 { get; set; } = null!;
        public string? Phone2 { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int AutenticationTypeId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual IdentificationType IdentType { get; set; } = null!;
        public virtual CustomerStatus StatusNavigation { get; set; } = null!;
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<CustomerLogin> CustomerLogins { get; set; }
        public virtual ICollection<CustomerPasswordHistory> CustomerPasswordHistories { get; set; }
        public virtual ICollection<CustomerToken> CustomerTokens { get; set; }
    }
}
