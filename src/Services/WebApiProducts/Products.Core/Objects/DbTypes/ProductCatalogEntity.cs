
namespace Products.Core.Objects.DbTypes
{
    public class ProductCatalogEntity
    {
        public ICollection<ProductEntity> Products { get; set; }
        public int TotalProducts { get; set; }

        public ProductCatalogEntity(ICollection<ProductEntity> products, int totalProducts)
        {
            Products = products;
            TotalProducts = totalProducts;
        }
    }
}
