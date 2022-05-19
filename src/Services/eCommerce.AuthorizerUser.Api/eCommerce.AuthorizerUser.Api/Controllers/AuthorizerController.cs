using eCommerce.AuthorizerUser.Api.Models;
using eCommerce.Commons.Objects.Requests.AuthorizerUser;
using eCommerce.Commons.Objects.Responses.AuthorizerUser;
using eCommerce.Commons.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.AuthorizerUser.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizerController : ControllerBase
    {

        private readonly UserManager<IdentityUserCustom> _userManager;
        private readonly SignInManager<IdentityUserCustom> _signInManager;

        public AuthorizerController(
            UserManager<IdentityUserCustom> userManager,
            SignInManager<IdentityUserCustom> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            //string key = AppConfiguration.Configuration["AppConfiguration:TokenService"].ToString();
            //string password = Decode.Decrypt(key, model.Password);
            var user = new IdentityUserCustom { UserName = model.Email, Email = model.Email, RefreshToken = GenerateRefreshToken(), RefreshTokenExpiryTime = DateTime.Now.AddHours(2) };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(BuildToken(user));
            }
            else
            {
                return BadRequest("Username or password invalid");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            //string key = AppConfiguration.Configuration["AppConfiguration:TokenService"].ToString();
            //string password = Decode.Decrypt(key, model.Password);
            //var result = await _signInManager.PasswordSignInAsync(model.Email, password, isPersistent: false, lockoutOnFailure: false);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddHours(2);
                _userManager.UpdateAsync(user);

                return Ok(BuildToken(user));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<UserToken>> Refresh([FromBody] UserToken model)
        {

            if (model is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid client request.");
                return BadRequest(ModelState);
            }

            var principal = GetPrincipalFromExpiredToken(model.Token);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Invalid client request.");
                return BadRequest(ModelState);
            }

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(2);
            _userManager.UpdateAsync(user);

            return Ok(BuildToken(user));
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
            else
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                _userManager.UpdateAsync(user);
                return NoContent();
            }
        }

        private UserToken BuildToken(IdentityUserCustom model)
        {
            var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim("TokenApi", "Lo que yo quiera"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.Configuration["AppConfiguration:ApiCode"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = model.RefreshTokenExpiryTime?.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationToken = expiration,
                RefreshToken = model.RefreshToken,
                ExpirationRefreshToken = model.RefreshTokenExpiryTime

            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.Configuration["AppConfiguration:ApiCode"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}