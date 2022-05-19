using eCommerce.Customers.Core.Contracts.Repositories;
using eCommerce.Customers.Core.Objects.DbTypes;
using eCommerce.Customers.Infraestructure.Contexts.DbCustomers;
using eCommerce.Customers.Infraestructure.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Customers.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbCustomerContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerRepository(DbCustomerContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCustomer(CustomerEntity customerEntity)
        {
            var result = await _dbcontext.SpCreateAsync(customerEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
        public async Task<bool> UpdateCustomer(CustomerUpdateEntity customerEntity)
        {
            var result = await _dbcontext.SpUpdateAsync(customerEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
        public CustomerEntity GetCustomerById(string customerId)
        {

            IQueryable<Customer> query = _dbcontext.Customers;
            if (customerId == String.Empty)
                return null;
            var customer = query.Where(x => x.Id == customerId).FirstOrDefault();
            if (customer==null)
                return null;
            var typeId = (from type in _dbcontext.IdentificationTypes
                             where type.Id == customer.IdentTypeId
                             select type.Name).FirstOrDefault();
            var statusName= (from status in _dbcontext.CustomerStatuses
                             where status.Status == customer.Status
                         select status.Description).FirstOrDefault();
            var result = customer;
            var customerEntity = new CustomerEntity(result.Id, typeId==null?String.Empty:typeId, result.Identification, result.FirstName, result.SecondName, result.LastName, result.SecondLastName, result.Email, result.Phone1, result.Phone2, result.UserName, result.Password, result.AutenticationTypeId, statusName.ToString(), result.CreatedAt) ;
            return customerEntity;
        }
        public string GetNameAuthentication(int id)
        {
            var authenticationName = from x in _dbcontext.AuthenticationTypes
                                     where x.Id == id
                                     select x.Name;
            return authenticationName.FirstOrDefault().ToString();
        }
    }
}
