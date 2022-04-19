
namespace eCommerce.Commons.Utilities
{
    public static class ProductUtilities
    {
        public enum ORDERBY
        {
            MinPrice,
            MaxPrice,
            Relevance
        }

        public enum CONDITION
        {
            New,
            Used,
            Returned
        }

        public enum CONDITIONCODE
        {
            NUV,
            USA,
            DEV
        }
    }
}
