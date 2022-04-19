using Authorizer.Core.Contracts.Services;
using Authorizer.Core.Exceptions;
using eCommerce.Commons.Objects.Requests.Authorizer;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Authorizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiAuthorizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private IAuthorizeService _authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService) 
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Generación de token de acceso
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AccessToken")]
        public async Task<ActionResult<ServiceResponse<TokenResponse>>> GenerateAccessToken([FromBody] TokenRequest request) 
        {
            try
            {
                var result = await _authorizeService.GenerateAccessToken(request.ApiUser, request.ApiKey);
                var response = new TokenResponse(result.UserName, result.AccessToken, result.ExpiresAt);
                return Ok(new ServiceResponse<TokenResponse>("Access token generado", response));
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ServiceResponse<string>(ex.Message, null));
            }
        }

        /// <summary>
        /// Validación del token de acceso
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AccessToken/validate")]
        public async Task<ActionResult<ServiceResponse<ValidateTokenResponse>>> ValidateAccessToken([FromBody] ValidateTokenRequest request)
        {
            try
            {
                var result = await _authorizeService.ValidateAccessToken(request.AccessToken, request.AppCode);
                var response = new ValidateTokenResponse(result);

                return result ? Ok(new ServiceResponse<ValidateTokenResponse>("Access token validdo", response))
                    : Unauthorized(new ServiceResponse<ValidateTokenResponse>("Access token invalido", response));
            }
            catch (ValidateAccessTokenException ex) 
            {
                return Unauthorized(new ServiceResponse<string>(ex.Message, null));
            }
        }
    }
}
