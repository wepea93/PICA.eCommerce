using eCommerce.Commons.ExtensionMethods.DataTableExt;
using eCommerce.Products.Reports.Core.Contracts.Repositories;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using eCommerce.Products.Reports.Infraestructure.Contexts.DbCustomer;

namespace eCommerce.Products.Reports.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbCustomerContext _dbcontext;

        public CustomerRepository(DbCustomerContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<CustomerEntity>?> GetCustomersByIdAsync(IEnumerable<CustomerEntity> customerEntities)
        {
            var dataTable = customerEntities.CopyToDataTable();
            return await _dbcontext.SpGetCustomersByIdAsync(dataTable);
        }
    }
}
