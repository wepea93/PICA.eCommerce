using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Customers.Core.Objects.DbTypes
{
    public class CustomerUpdateEntity
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

        public CustomerUpdateEntity(string id, string? identTypeId, string? identification, string? firstName, string? secondName, string? lastName, string? secondLastName, string? email, string? phone1, string? phone2, string? userName, string? password, int autenticationTypeId, string? status, DateTime? createdAt)
        {
            Id = id;
            IdentTypeId = identTypeId;
            Identification = identification;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
            Email = email;
            Phone1 = phone1;
            Phone2 = phone2;
            UserName = userName;
            Password = password;
            AutenticationTypeId = autenticationTypeId;
            Status = status;
            CreatedAt = createdAt;

        }
    }
}
