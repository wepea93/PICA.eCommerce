namespace eCommerce.Blazor.Demo.Services
{
    public class BaseHttpClient
    {
        private readonly HttpClient http;
        public BaseHttpClient(HttpClient http)
        {
            this.http = http;
        }
    }
}
