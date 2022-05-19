using eCommerce.Blazor.Demo.LocalStorage;
using eCommerce.Commons.Objects.Responses.AuthorizerUser;
using eCommerce.Services.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace eCommerce.Blazor.Demo.Auth
{
    public class AuthIDentityJwtProvider : AuthenticationStateProvider, IAuthProvider
    {
        private TokenJwtLocalStorage _localStorage;
       // private readonly AuthorizerUserService _service;

        public AuthIDentityJwtProvider(TokenJwtLocalStorage localStorage)//, AuthorizerUserService service)
        {
            _localStorage = localStorage;
          //  _service = service;
        }

        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetToken();
            var now = DateTime.UtcNow;

            if (token == null)
            {
                return Anonimo;
            }

            //if (token.ExpirationToken > now)
            //{
            //    if (token.ExpirationRefreshToken < now)
            //    {
            //        token = await _service.Refresh(token);
            //        await _localStorage.SaveToken(token);
            //    }
            //    else
            //    {
            //        await _localStorage.RemoveToken();
            //        return Anonimo;
            //    }
            //}

            return ConstruirAuthenticationState(token.Token);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public async Task Login(UserToken token)
        {
            await _localStorage.SaveToken(token);
            var authState = ConstruirAuthenticationState(token.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await _localStorage.RemoveToken();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var claims = new List<Claim>();
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
