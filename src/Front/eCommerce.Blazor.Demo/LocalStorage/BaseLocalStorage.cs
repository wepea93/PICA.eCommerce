using Blazored.LocalStorage;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.Commons.Utilities;
using System.Text.Json;

namespace eCommerce.Blazor.Demo.LocalStorage
{
    public class BaseLocalStorage
    {
        public string key;

        private readonly ISyncLocalStorageService LocalStorage;

        public BaseLocalStorage(ISyncLocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task<T> Get<T>()
        {
            var data = LocalStorage.GetItem<string>(key);
            return JsonSerializer.Deserialize<T>(Decode.Base64(data), JsonUtilities.jsonSettings);
        }

        public async Task Save<T>(T item)
        {
            var data = JsonSerializer.Serialize(item, JsonUtilities.jsonSettings);
            LocalStorage.SetItem(key, Encode.Base64(data));
        }

        public async Task<bool> Contain() => LocalStorage.ContainKey(key);
        public async Task Remove() => LocalStorage.RemoveItem(key);
    }
}
