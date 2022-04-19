
namespace eCommerce.Commons.Objects.Responses.Products
{
    public  class ProductCatalogResponse
    {
        public bool ProductsFound { get; set; }
        public IEnumerable<ProductResponse> Products { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int Count { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages { get; set; }

        public ProductCatalogResponse(bool productsFound, int count, IEnumerable<ProductResponse> products, int page, string sort, int totalProducts, int totalPages)
        {
            ProductsFound = productsFound;
            Products = products;
            Count = count;
            Page = page;
            Sort = sort;
            TotalProducts = totalProducts;
            TotalPages = totalPages;
        }
    }
}
