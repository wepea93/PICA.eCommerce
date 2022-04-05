namespace eCommerce.Blazor.Demo.Common.Responses
{
    public class ValueResponse
    {
        public bool Result { get; set; }

        public ValueResponse(bool result)
        {
            Result = result;
        }
    }
}
