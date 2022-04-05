using Newtonsoft.Json;

namespace eCommerce.Blazor.Demo.Common.Utilities
{
    public static class UtilitiesHelper
    {
        public enum ORDERBY
        {
            MinPrice,
            MaxPrice,
            Relevance
        }

        public enum PRICERANGE
        {
            Under,
            Range1,
            Range2,
            Range3,
            Above,
            Other
        }
        public enum viewCard
        {
            module, list
        }

        public static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}
