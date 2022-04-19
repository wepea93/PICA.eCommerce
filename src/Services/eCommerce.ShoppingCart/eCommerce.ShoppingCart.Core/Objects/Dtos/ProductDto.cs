
namespace eCommerce.ShoppingCart.Core.Objects.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; }

        public ProductDto(long id, string name, string image, bool status, int stock) 
        {
            Id = id;
            Name = name;
            Image = image;
            Available = status && stock > 0 ? true : false;
        }
    }
}
