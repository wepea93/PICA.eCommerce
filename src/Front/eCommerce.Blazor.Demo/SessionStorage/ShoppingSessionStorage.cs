using Blazored.SessionStorage;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.Commons.Utilities;
using System.Text.Json;

namespace eCommerce.Blazor.Demo.SessionStorage
{
    public class ShoppingSessionStorage
    {
        private const string key = "ShoppingCart";

        private readonly ISyncSessionStorageService SessionlStorage;

        public ShoppingSessionStorage(ISyncSessionStorageService sessionStorage)
        {
            SessionlStorage = sessionStorage;
        }

        public async Task<IEnumerable<ShoppingCartResponse>> GetShoppingCart()
        {
            if (SessionlStorage.ContainKey(key))
            {
                var data = SessionlStorage.GetItem<string>(key);
                return JsonSerializer.Deserialize<IEnumerable<ShoppingCartResponse>>(Decode.Base64(data), JsonUtilities.jsonSettings);
            }
            else
                return Enumerable.Empty<ShoppingCartResponse>();
        }

        public async Task SaveShoppingCart(IEnumerable<ShoppingCartResponse> list)
        {
            var data = JsonSerializer.Serialize(list, JsonUtilities.jsonSettings);
            SessionlStorage.SetItem(key, Encode.Base64(data));
        }
    }
}
