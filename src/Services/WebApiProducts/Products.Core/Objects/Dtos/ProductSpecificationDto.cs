
namespace Products.Core.Objects.Dtos
{
    public class ProductSpecificationDto
    {
        public string Description { get; set; } = null!;
        public string Value { get; set; } = null!;

        public ProductSpecificationDto(string description, string value ) 
        {
            Description = description;
            Value = value;
        }

        public ProductSpecificationDto() { }
    }
}
