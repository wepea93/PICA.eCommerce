
using eCommerce.Customers.Core.Objects.DbTypes;
using eCommerce.Customers.Core.Objects.Dtos;
using eCommerce.Customers.Core.Objects.Responses;

namespace eCommerce.Customers.Core.Helpers.Mappers
{
    public interface IMapperHelper
    {
        CustomerDto MappToCustomerDto(CustomerEntity customerEntity);
        CustomerResponse MappToCustomerResponse(CustomerDto customerDto, string Auth);
    }
}
