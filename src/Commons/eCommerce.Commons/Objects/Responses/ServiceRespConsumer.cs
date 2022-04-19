using eCommerce.Commons.Utilities;
using Newtonsoft.Json;

namespace eCommerce.Commons.Objects.Responses
{
    public class ServiceRespConsumer
    {

        [JsonProperty(Order = 1, PropertyName = "message")]
        public string Message { get; private set; }

        [JsonProperty(Order = 2, PropertyName = "response")]
        public string Response { get; private set; }

        public ServiceRespConsumer(string message, string response)
        {
            Message = message;
            Response = response;
        }

        public ServiceRespConsumer(string message)
        {
            Message = message;
            Response = string.Empty;
        }

        public T GetResponse<T>()
        {
            return JsonConvert.DeserializeObject<T>(Response);
        }

        public ServiceRespConsumer SetResponse<T>(T response)
        {
            Response = JsonConvert.SerializeObject(response, JsonUtilities.jsonSettings);
            return this;
        }
    }
}
