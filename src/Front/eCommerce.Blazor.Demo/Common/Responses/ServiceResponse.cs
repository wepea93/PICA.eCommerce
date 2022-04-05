using eCommerce.Blazor.Demo.Common.Utilities;
using Newtonsoft.Json;

namespace eCommerce.Blazor.Demo.Common.Responses
{
    public class ServiceResponse 
    {       

        [JsonProperty(Order = 1, PropertyName = "message")]
        public string Message { get; private set; }

        [JsonProperty(Order = 2, PropertyName = "response")]
        public string Response { get; private set; }

        public ServiceResponse(string message, string response)
        {
            Message = message;
            Response = response;
        }

        public ServiceResponse(string message) 
        {
            Message = message;
            Response = string.Empty;
        }

        public T GetResponse<T>()
        {
            return JsonConvert.DeserializeObject<T>(Response);
        }

        public ServiceResponse SetResponse<T>(T response)
        {
            Response = JsonConvert.SerializeObject(response, UtilitiesHelper.jsonSettings);
            return this;
        }
    }
}
