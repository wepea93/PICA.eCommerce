using eCommerce.Customers.Core.APIS.Contracts;
using eCommerce.Customers.Core.Contracts.Repositories;
using eCommerce.Customers.Core.Contracts.Services;
using eCommerce.Customers.Core.Helpers.Mappers;
using eCommerce.Customers.Core.Objects.Dtos;
using eCommerce.Customers.Infraestructure.Models.UnitOfWorks;

namespace eCommerce.Customers.Infraestructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperHelper _mapper;
        private readonly IAuthorizerUserService _authorizerUserService;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapperHelper mapperHelper,
            IAuthorizerUserService authorizerUserService)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapperHelper;
            _authorizerUserService = authorizerUserService;
        }

        public async Task<bool> Create(CustomerDto Customer)
        {
            var userResponse = await _authorizerUserService.CreateUser(Customer.Email, Customer.Password);

            if(userResponse == null) return false;

            Customer.UserName = Customer.Email;
            
            var customerEntity = new Core.Objects.DbTypes.CustomerEntity(userResponse.UserId, Customer.Identification, Customer.IdentTypeId, 
                Customer.FirstName, Customer.SecondName, Customer.LastName, Customer.SecondLastName, Customer.Email, Customer.Phone1, 
                Customer.Phone2, Customer.UserName, Customer.Password, Customer.AutenticationTypeId, Customer.Status, Customer.CreatedAt);
            
            var result = await _customerRepository.CreateCustomer(customerEntity);
            await _unitOfWork.ConfirmAsync();
            
            return result;
        }
        public async Task<bool> Update(CustomerUpdateDto Customer)
        {
            var customerEntity = new Core.Objects.DbTypes.CustomerUpdateEntity(Customer.Id, Customer.Identification, Customer.IdentTypeId, Customer.FirstName, Customer.SecondName, Customer.LastName, Customer.SecondLastName, Customer.Email, Customer.Phone1, Customer.Phone2, Customer.UserName, Customer.Password, Customer.AutenticationTypeId, Customer.Status, Customer.CreatedAt);
            var result = await _customerRepository.UpdateCustomer(customerEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
        public CustomerDto GetCustomerById(string customerId)
        {
            var customers = _customerRepository.GetCustomerById(customerId);
            var result = customers != null ? _mapper.MappToCustomerDto(customers) : null;
            return result;
        }
        public string GetNameAuthentication(int Id)
        {
            var result = _customerRepository.GetNameAuthentication(Id);
            return result;
        }
    }
}
