using Authorizer.Core.Contracts.Repositories;
using Authorizer.Core.Contracts.Services;
using Authorizer.Core.Exceptions;
using Authorizer.Core.Objects.DbTypes;
using Authorizer.Core.Objects.Dtos;
using Authorizer.Infraestructure.Models.UnitOfWorks;
using Autorizer.Core.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorizer.Infraestructure.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private IAuthorizeRepository _authorizeRepository;
        private IUnitOfWork _unitOfWork;

        public AuthorizeService(IAuthorizeRepository authorizeRepository, IUnitOfWork unitOfWork) 
        {
            _authorizeRepository = authorizeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccessTokenDto> GenerateAccessToken(string apiUser, string apiKey)
        {
            var appUser = await _authorizeRepository.GetAppUser(apiUser, apiKey);

            if(appUser == null)  throw new UnauthorizedUserException();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppConfiguration.Configuration["AppConfiguration:Authorize:SecretKey"].ToString());
            var seconds = Convert.ToInt32(AppConfiguration.Configuration["AppConfiguration:Authorize:Seconds"].ToString());
            var expiresAt = DateTime.UtcNow.AddSeconds(seconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("appCode", appUser.AppCode) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenBuilder = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenBuilder);

            var accessToken = new AccessTokenDto(apiUser, token, expiresAt.ToString());

            var result = await _authorizeRepository.RegisterAccessToken(new AccessTokenEntity(appUser.AppCode, token, expiresAt));
            _unitOfWork.Confirm();

            return result == true ? accessToken : throw new UnauthorizedUserException();
        }

        public async Task<bool> ValidateAccessToken(string token, string appCode)
        {
            try
            {
                var nToken = token.StartsWith("Bearer ") ? token.Substring(7) : token;
                nToken = nToken.Replace("\"\"", "");

                var jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: nToken);

                var jwtAppCode = jwtSecurityToken.Claims.First(c => c.Type == "appCode").Value;

                var isAccesTokenValid = await _authorizeRepository.IsValidAccessToken(nToken, jwtAppCode, appCode);
                return isAccesTokenValid;
            }
            catch (Exception ex)
            {
                throw new ValidateAccessTokenException();
            }
        }
    }
}
