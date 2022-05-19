using eCommerce.Commons.Objects.Requests.AuthorizerUser;
using eCommerce.Commons.Objects.Responses.AuthorizerUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Contracts
{
    public interface IAuthorizerUserService
    {

        public Task<UserToken> Refresh(UserToken request);
        public Task<UserToken> Login(UserInfo request);
        public Task<UserToken> CreateUser(UserInfo request);
    }
}
