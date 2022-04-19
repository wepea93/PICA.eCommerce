using Newtonsoft.Json;

namespace eCommerce.Commons.Objects.Responses
{
    public class ServiceResponse<T>
    {
        [JsonProperty(Order = 1, PropertyName = "message")]
        public string Message { get; private set; }

        [JsonProperty(Order = 2, PropertyName = "response")]
        public T Response { get; private set; }

        public ServiceResponse(string message, T response)
        {
            Message = message;
            Response = response;
        }
    }
}
