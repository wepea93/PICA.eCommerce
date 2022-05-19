
using eCommerce.Commons.Objects.Responses.User;

namespace eCommerce.Customers.Core.APIS.Contracts
{
    public interface IAuthorizerUserService
    {
        Task<UserResponse?> CreateUser(string userName, string password);
    }
}
