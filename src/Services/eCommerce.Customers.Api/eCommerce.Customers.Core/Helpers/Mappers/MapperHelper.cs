
using eCommerce.Customers.Core.Objects.DbTypes;
using eCommerce.Customers.Core.Objects.Dtos;
using eCommerce.Customers.Core.Objects.Responses;

namespace eCommerce.Customers.Core.Helpers.Mappers
{
    public class MapperHelper : IMapperHelper
    {
        public CustomerDto MappToCustomerDto(CustomerEntity customerEntity)
        {
            if (customerEntity == null)
                return null;
            var customerDto = new CustomerDto(customerEntity.Id, customerEntity.Identification, customerEntity.IdentTypeId, customerEntity.FirstName, customerEntity.SecondName, customerEntity.LastName, customerEntity.SecondLastName, customerEntity.Email, customerEntity.Phone1, customerEntity.Phone2, customerEntity.UserName, customerEntity.Password, customerEntity.AutenticationTypeId, customerEntity.Status, customerEntity.CreatedAt);
            
            return customerDto;
        }

        public CustomerResponse MappToCustomerResponse(CustomerDto customerDto,string Auth)
        {
            if (customerDto == null)
                return null;
            var customerreponse = new CustomerResponse(customerDto.Id, customerDto.Identification, customerDto.IdentTypeId, customerDto.FirstName, customerDto.SecondName, customerDto.LastName, customerDto.SecondLastName, customerDto.Email, customerDto.Phone1, customerDto.Phone2, customerDto.UserName, Auth.ToString(), customerDto.Status, customerDto.CreatedAt);
         return customerreponse;
        }
    }
}
