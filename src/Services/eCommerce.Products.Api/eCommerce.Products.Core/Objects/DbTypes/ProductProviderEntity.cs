
namespace Products.Core.Objects.DbTypes
{
    public class ProductProviderEntity
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Provider { get; set; } = null!;
        public bool Status { get; set; }
    }
}
