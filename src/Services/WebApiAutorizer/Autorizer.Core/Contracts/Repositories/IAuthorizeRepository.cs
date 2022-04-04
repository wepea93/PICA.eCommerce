
using Authorizer.Core.Objects.DbTypes;

namespace Authorizer.Core.Contracts.Repositories
{
    public interface IAuthorizeRepository
    {
        Task<bool> IsValidAccessToken(string token, string appCodeOrigen, string appCodeDestiny);
        Task<bool> RegisterAccessToken(AccessTokenEntity accessTokenEntity);
        Task<AppUserEntity> GetAppUser(string apiUser, string apiKey);
    }
}
