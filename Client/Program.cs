using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using VersaControl.Client;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "VersaControlTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<VersaControl.Client.versacontrolService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("VersaControl.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("VersaControl.Server"));
builder.Services.AddScoped<VersaControl.Client.SecurityService>();
builder.Services.AddScoped<AuthenticationStateProvider, VersaControl.Client.ApplicationAuthenticationStateProvider>();
var host = builder.Build();
await host.RunAsync();