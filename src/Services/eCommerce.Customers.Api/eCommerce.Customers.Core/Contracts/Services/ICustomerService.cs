using eCommerce.Customers.Core.Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Customers.Core.Contracts.Services
{
    public interface  ICustomerService
    {
        Task<bool> Create(CustomerDto Customer);
        CustomerDto GetCustomerById(string customerId);
        string GetNameAuthentication(int Id);
        Task<bool> Update(CustomerUpdateDto Customer);
    }
}
