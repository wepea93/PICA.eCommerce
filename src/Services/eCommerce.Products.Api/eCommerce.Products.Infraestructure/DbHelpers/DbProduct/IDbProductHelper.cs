using eCommerce.Commons.Utilities;
using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Products.Core.Objects.DbTypes;
using System.Linq.Expressions;


namespace Products.Infraestructure.DbHelpers
{
    public interface IDbProductHelper
    {
        Expression<Func<Product, bool>> FiltersExpression(string search, int productCategoryId, int providerId, decimal minPrice, decimal maxPrice);
        Func<IQueryable<Product>, IOrderedQueryable<Product>> OrderBy(ProductUtilities.ORDERBY orderBy);

        ProductEntity ConvertToProductEntity(Product bdProdcut);
        IEnumerable<ProductEntity> ConvertToProductEntity(IEnumerable<Product> bdProdcuts);
    }
}
