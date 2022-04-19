
namespace Products.Core.Objects.DbTypes
{
    public class ProductCategoryEntity
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string Image { get; set; } = null!;
        public bool Status { get; set; }
        public IEnumerable<ProductProviderEntity> ProductProviderEntities { get; set; }
    }
}
