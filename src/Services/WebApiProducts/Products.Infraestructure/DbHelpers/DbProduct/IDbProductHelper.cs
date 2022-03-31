using Products.Core.Objects.DbTypes;
using Products.Core.Utilities;
using Products.Infraestructure.Contexts.DbProduct;
using System.Linq.Expressions;


namespace Products.Infraestructure.DbHelpers
{
    public interface IDbProductHelper
    {
        Expression<Func<Product, bool>> FiltersExpression(string search, int productCategoryId, int providerId, decimal minPrice, decimal maxPrice);
        Func<IQueryable<Product>, IOrderedQueryable<Product>> OrderBy(UtilitiesHelper.ORDERBY orderBy);

        ProductEntity ConvertToProductEntity(Product bdProdcut);
        IEnumerable<ProductEntity> ConvertToProductEntity(IEnumerable<Product> bdProdcuts);
    }
}
