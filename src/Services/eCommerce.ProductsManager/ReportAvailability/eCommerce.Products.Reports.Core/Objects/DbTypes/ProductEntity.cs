using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Reports.Core.Objects.DbTypes
{
    public class ProductEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }

        public ProductEntity(long id, string name, int stock)
        {
            Id = id;
            Name = name;
        }

        public ProductEntity() { }
    }
}
