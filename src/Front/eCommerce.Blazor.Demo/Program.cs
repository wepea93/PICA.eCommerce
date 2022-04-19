using eCommerce.Blazor.Demo;
using eCommerce.Blazor.Demo.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.Extensions.Http;
using eCommerce.Services.Services;
using Blazored.Toast;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["Service:BaseUrl"].ToString())
});

builder.Services.AddHttpClient<ProductService>(config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["Service:BaseUrl"].ToString());
                config.Timeout = TimeSpan.FromSeconds(10000);
                config.DefaultRequestHeaders.Clear();
                config.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
    );

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
