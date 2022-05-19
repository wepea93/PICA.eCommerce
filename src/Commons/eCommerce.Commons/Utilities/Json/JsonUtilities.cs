

using System.Text.Json;

namespace eCommerce.Commons.Utilities
{
    public static class JsonUtilities
    {
        public static JsonSerializerOptions jsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
        //{
        //    NullValueHandling = NullValueHandling.Ignore,
        //    MissingMemberHandling = MissingMemberHandling.Ignore
        //};
    }
}
