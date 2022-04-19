using Newtonsoft.Json;

namespace eCommerce.Commons.Objects.Responses
{
    public class ServiceResponseError: ServiceResponse<string>
    {
        public ServiceResponseError(string message, string response): base(message, response)
        {

        }
    }
}
