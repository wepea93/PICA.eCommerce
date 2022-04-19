
using Newtonsoft.Json;

namespace eCommerce.Commons.Utilities
{
    public static class JsonUtilities
    {
        public static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}
