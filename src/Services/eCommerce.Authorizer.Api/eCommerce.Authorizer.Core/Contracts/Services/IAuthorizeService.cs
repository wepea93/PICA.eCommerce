
using Authorizer.Core.Objects.Dtos;

namespace Authorizer.Core.Contracts.Services
{
    public interface IAuthorizeService
    {
        public Task<AccessTokenDto> GenerateAccessToken(string apiUser, string apiKey);
        public Task<bool> ValidateAccessToken(string token, string appCode);
    }
}
