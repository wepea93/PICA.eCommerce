using Blazored.LocalStorage;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.Commons.Utilities;
using System.Text.Json;

namespace eCommerce.Blazor.Demo.LocalStorage
{
    public class ShoppingLocalStorage
    {
        private const string key = "ShoppingCart";

        private readonly ISyncLocalStorageService LocalStorage;

        public ShoppingLocalStorage(ISyncLocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task<IEnumerable<ShoppingCartResponse>> GetShoppingCart()
        {
            if (LocalStorage.ContainKey(key))
            {
                var data = LocalStorage.GetItem<string>(key);
                return JsonSerializer.Deserialize<IEnumerable<ShoppingCartResponse>>(Decode.Base64(data), JsonUtilities.jsonSettings);
            }
            else
                return Enumerable.Empty<ShoppingCartResponse>();
        }

        public async Task SaveShoppingCart(IEnumerable<ShoppingCartResponse> list)
        {
            var data = JsonSerializer.Serialize(list, JsonUtilities.jsonSettings);
            LocalStorage.SetItem(key, Encode.Base64(data));
        }
    }
}
