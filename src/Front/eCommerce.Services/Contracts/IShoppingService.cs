using eCommerce.Commons.Objects.Requests.ShoppingCart;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.ShoppingCart;

namespace eCommerce.Services.Contracts
{
    public interface IShoppingService
    {
        public Task<ServiceResponse<IEnumerable<ShoppingCartResponse>>> GetShoppingCart(GetShoppingCartRequest request);
        public Task<ServiceResponse<bool>> AddShoppingCart(CreateShoppingCartRequest request);
        public Task<ServiceResponse<bool>> UpdateShoppingCart(UpdateShoppingCartRequest request);
        public Task<ServiceResponse<bool>> DeleteShoppingCart(DeleteShoppingCartRequest request);
    }
}
