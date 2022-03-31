
namespace Products.Core.Objects.Dtos
{
    public class ProductCatalogDto
    {
        public bool ProductsFound { get; set; }
        public ICollection<ProductDto> Products { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int Count { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages { get; set; }

        public ProductCatalogDto(ICollection<ProductDto> products, int page, string sort, int totalProducts, int itemsByPage) 
        {
            ProductsFound = products != null && products.Any();
            Products = products;
            Count = products != null ? products.Count : 0;
            Page = page;
            Sort = sort;
            TotalProducts = totalProducts;
            TotalPages = CalculatePagesNumber(itemsByPage, totalProducts);
        }

        private int CalculatePagesNumber(int itemsByPage, int totalProducts) 
        {
            return (int)Math.Ceiling((double)totalProducts / itemsByPage);
        }
    }
}
