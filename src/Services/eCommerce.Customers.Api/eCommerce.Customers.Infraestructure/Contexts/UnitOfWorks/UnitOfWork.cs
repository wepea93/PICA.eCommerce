using eCommerce.Customers.Infraestructure.Contexts.DbCustomers;


namespace eCommerce.Customers.Infraestructure.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbCustomerContext _dbContext;

        public UnitOfWork(DbCustomerContext dbContext)
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
