using eCommerce.Orders.Infraestructure.Contexts.DbOrder;


namespace eCommerce.Orders.Infraestructure.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbOrderContext _dbContext;

        public UnitOfWork(DbOrderContext dbContext)
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
