using eCommerce.Customers.Core.Objects.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Customers.Core.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(CustomerEntity customerEntity);
        CustomerEntity GetCustomerById(string customerId);
        string GetNameAuthentication(int id);
        Task<bool> UpdateCustomer(CustomerUpdateEntity customerEntity);
    }
}
