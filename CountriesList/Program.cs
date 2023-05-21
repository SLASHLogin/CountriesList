using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CountriesList;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
.AddContinentsClient()
.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://countries.trevorblades.com/graphql"))
.ConfigureWebSocketClient(client => client.Uri = new Uri("wss://countries.trevorblades.com/graphql"));

await builder.Build().RunAsync();
