using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Customers.Core.Objects.Request
{
    public class CustomerUpRequest
    {

        public string Id { get; set; } = null!;
        public string? IdentTypeId { get; set; } = null!;
        public string? Identification { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? SecondLastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone1 { get; set; } = null!;
        public string? Phone2 { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public int AutenticationTypeId { get; set; }
        public string? Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
