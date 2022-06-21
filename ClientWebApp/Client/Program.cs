using ClientWebApp.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSyncfusionBlazor(opt =>
{
#pragma warning disable CS0618 // Type or member is obsolete
    opt.IgnoreScriptIsolation = true;
#pragma warning restore CS0618 // Type or member is obsolete
});
await builder.Build().RunAsync();
