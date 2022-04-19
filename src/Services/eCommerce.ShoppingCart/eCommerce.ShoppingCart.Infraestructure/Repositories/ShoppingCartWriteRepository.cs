using eCommerce.Commons.ExtensionMethods.DataTableExt;
using eCommerce.ShoppingCart.Core.Contracts.Repositories;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;
using eCommerce.ShoppingCart.Infraestructure.Contexts;
using eCommerce.ShoppingCart.Infraestructure.Contexts.UnitOfWorks;

namespace eCommerce.ShoppingCart.Infraestructure.Repositories
{
    public class ShoppingCartWriteRepository : IShoppingCartWriteRepository
    {
        private readonly DbShoppingCartWriteContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartWriteRepository(DbShoppingCartWriteContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateShoppingCart(IEnumerable<ShoppingCartEntity> shoppingCartEntities)
        {
            var dataTable = shoppingCartEntities.CopyToDataTable();
            var result = await _dbcontext.SpCreateShoppingCartAsync(dataTable);
            await _unitOfWork.ConfirmAsync();
            return result;
        }

        public async Task<bool> UpdateShoppingCart(ShoppingCartEntity shoppingCartEntity)
        {
            var result = await _dbcontext.SpUpdateShoppingCartAsync(shoppingCartEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }

        public async Task<bool> DeleteShoppingCartItem(ShoppingCartEntity shoppingCartEntity)
        {
            var result = await _dbcontext.SpDeleteShoppingCartItemAsync(shoppingCartEntity.CustomerId, shoppingCartEntity.ProductId);
            await _unitOfWork.ConfirmAsync();
            return result;
        }

        public async Task<bool> DeleteShoppingCartByUser(string userId)
        {
            var result = await _dbcontext.SpDeleteShoppingCartByUserAsync(userId);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
    }
}
