using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Products.Infraestructure.Models;
   
namespace Products.Infraestructure.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProductsContext _dbContext;

        public UnitOfWork(DbProductsContext dbContext)
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
