
using eCommerce.Products.Availability.Infraestructure.Contexts.DbProduct;

namespace eCommerce.Products.Availability.Infraestructure.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProductContext _dbContext;

        public UnitOfWork(DbProductContext dbContext)
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
