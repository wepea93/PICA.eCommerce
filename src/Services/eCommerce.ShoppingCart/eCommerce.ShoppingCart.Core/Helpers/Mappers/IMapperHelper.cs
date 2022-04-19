using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.ShoppingCart.Core.Objects.Dtos;

namespace eCommerce.ShoppingCart.Core.Helpers.Mappers
{
    public interface IMapperHelper
    {
        ShoppingCartResponse MappToShoppingCartResponse(ShoppingCartDto dto);
    }
}
