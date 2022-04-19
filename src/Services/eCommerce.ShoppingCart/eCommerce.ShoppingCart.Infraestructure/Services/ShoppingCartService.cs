using eCommerce.ShoppingCart.Core.Contracts.Repositories;
using eCommerce.ShoppingCart.Core.Contracts.Services;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;
using eCommerce.ShoppingCart.Core.Objects.Dtos;
using eCommerce.ShoppingCart.Infraestructure.Contexts.UnitOfWorks;

namespace eCommerce.ShoppingCart.Infraestructure.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IShoppingCartWriteRepository _shoppingCartWriteRepository;
        private IShoppingCartReadRepository _shoppingCartReadRepository;
        private IUnitOfWork _unitOfWork;

        public ShoppingCartService(IShoppingCartWriteRepository shoppingCartWriteRepository,
            IShoppingCartReadRepository shoppingCartReadRepository, IUnitOfWork unitOfWork)
        {
            _shoppingCartWriteRepository = shoppingCartWriteRepository;
            _shoppingCartReadRepository = shoppingCartReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetShoppingCartByUserId(string userId)
        {
            var shoppingCartResult = await _shoppingCartReadRepository.GetShoppingCartByUser(userId);

            if (shoppingCartResult == null) return null;

            var productsId = shoppingCartResult.Select(x => new ProductEntity() { Id = x.ProductId });
            var productsResult = await _shoppingCartReadRepository.GetShoppingCartProducts(productsId);

            var result = (from shoppingCart in shoppingCartResult
                          join product in productsResult on shoppingCart.ProductId equals product.Id
                          select new ShoppingCartDto(shoppingCart.Id, shoppingCart.CustomerId,
                                       new ProductDto(product.Id, product.Name, product.Image, product.Status, product.Stock),
                                           shoppingCart.InitialPrice, shoppingCart.Quantity)
                          { Price = product.Price,
                          });
            return result;
        }


        public async Task<bool> CreateShoppingCart(IEnumerable<ShoppingCartDto> shoppingCart)
        {
            var entities = shoppingCart.Select(x=> new ShoppingCartEntity(x.CustomerId, x.ProductId, x.InitialPrice, x.Quantity));
            var result = await _shoppingCartWriteRepository.CreateShoppingCart(entities);
            _unitOfWork.Confirm();

            return result;
        }

        public async Task<bool> UpdateShoppingCart(ShoppingCartDto shoppingCart)
        {
            var entity = new ShoppingCartEntity(shoppingCart.CustomerId, shoppingCart.ProductId, shoppingCart.InitialPrice, shoppingCart.Quantity);
            var result = await _shoppingCartWriteRepository.UpdateShoppingCart(entity);
            _unitOfWork.Confirm();

            return result;
        }

        public async Task<bool> DeleteShoppingCartItem(ShoppingCartDto shoppingCart)
        {
            var entity = new ShoppingCartEntity(shoppingCart.CustomerId, shoppingCart.ProductId);
            var result = await _shoppingCartWriteRepository.DeleteShoppingCartItem(entity);
            _unitOfWork.Confirm();

            return result;
        }

        public async Task<bool> DeleteShoppingCartByUser(string userId)
        {
            var result = await _shoppingCartWriteRepository.DeleteShoppingCartByUser(userId);
            _unitOfWork.Confirm();

            return result;
        }


    }
}
