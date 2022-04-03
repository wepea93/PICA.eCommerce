
namespace Products.Core.Objects.Responses
{
    public class ProductProviderResponse
    {
        public int Id { get; set; }
        public string Provider { get; set; }

        public ProductProviderResponse(int id, string provider)
        {
            Id = id;
            Provider = provider;
        }

        public ProductProviderResponse() { }
    }
}
