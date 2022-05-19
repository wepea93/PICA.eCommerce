
namespace eCommerce.Customers.Core.APIS.Contracts
{
    public interface IAuthenticateService
    {
        Task<string> GetBearerTokenAsync(string audience);
    }
}
