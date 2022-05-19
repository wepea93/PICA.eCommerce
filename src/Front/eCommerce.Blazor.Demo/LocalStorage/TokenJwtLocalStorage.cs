using Blazored.LocalStorage;
using eCommerce.Commons.Objects.Requests.ShoppingCart;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.AuthorizerUser;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.Commons.Utilities;
using eCommerce.Services.Contracts;
using eCommerce.Services.Exceptions;
using System.Text.Json;

namespace eCommerce.Blazor.Demo.LocalStorage
{
    public class TokenJwtLocalStorage
    {
        private const string key = "Token";

        private readonly ISyncLocalStorageService LocalStorage;

        public TokenJwtLocalStorage(ISyncLocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task<UserToken> GetToken()
        {
            UserToken token = null;
            if (LocalStorage.ContainKey(key))
            {
                var data = LocalStorage.GetItem<string>(key);
                token = JsonSerializer.Deserialize<UserToken>(Decode.Base64(data), JsonUtilities.jsonSettings);
            }
            return token;
        }

        public async Task SaveToken(UserToken token)
        {
            var data = JsonSerializer.Serialize(token, JsonUtilities.jsonSettings);
            LocalStorage.SetItem(key, Encode.Base64(data));
        }

        public async Task RemoveToken() => LocalStorage.RemoveItem(key);


    }
}
