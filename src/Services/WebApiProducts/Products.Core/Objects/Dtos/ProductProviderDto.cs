
namespace Products.Core.Objects.Dtos
{
    public class ProductProviderDto
    {
        public int Id { get; set; }
        public string Provider { get; set; } = null!;

        public ProductProviderDto(int id, string provider)
        {
            Id = id;
            Provider = provider;
        }

        public ProductProviderDto() { }
    }
}
