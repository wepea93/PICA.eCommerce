using eCommerce.ShoppingCart.Core.Objects.Dtos;

namespace eCommerce.ShoppingCart.Core.Contracts.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartDto>> GetShoppingCartByUserId(string userId);
        Task<bool> CreateShoppingCart(IEnumerable<ShoppingCartDto> shoppingCart);
        Task<bool> UpdateShoppingCart(ShoppingCartDto shoppingCart);
        Task<bool> DeleteShoppingCartItem(ShoppingCartDto shoppingCart);
        Task<bool> DeleteShoppingCartByUser(string userId);
    }
}
