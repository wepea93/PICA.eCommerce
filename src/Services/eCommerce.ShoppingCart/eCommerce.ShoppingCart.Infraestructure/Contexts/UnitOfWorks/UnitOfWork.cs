
namespace eCommerce.ShoppingCart.Infraestructure.Contexts.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbShoppingCartReadContext _dbContext;

        public UnitOfWork(DbShoppingCartReadContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Confirm()
        {
            _dbContext.SaveChanges();
        }

        public async Task ConfirmAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
