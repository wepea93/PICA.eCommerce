using Authorizer.Core.Contracts.Repositories;
using Authorizer.Core.Objects.DbTypes;
using Authorizer.Infraestructure.Models.DbAuthentication;
using Authorizer.Infraestructure.Models.UnitOfWorks;

namespace Authorizer.Infraestructure.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly DbAuthenticationContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizeRepository(DbAuthenticationContext dbcontext, IUnitOfWork unitOfWork) 
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsValidAccessToken(string token, string appCodeOrigen, string appCodeDestiny)
        {
            return await _dbcontext.SpValidateAccessToken(token, appCodeOrigen, appCodeDestiny);
        }

        public async Task<bool> RegisterAccessToken(AccessTokenEntity accessTokenEntity)
        {
            var result = await _dbcontext.SpCreateAccessTokenAsync(accessTokenEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }

        public async Task<AppUserEntity> GetAppUser(string apiUser, string apiKey)
        {
            return await _dbcontext.SpGetAppUsernAsync(apiUser, apiKey);
        }
    }
}
