using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Reports.Core.Objects.DbTypes
{
    public class CustomerEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CustomerEntity(string id, string name, string email) 
        {
            Id= id;
            Name= name;
            Email= email;
        }

        public CustomerEntity() { }
    }
}
