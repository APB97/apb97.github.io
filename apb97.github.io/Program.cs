using apb97.github.io;
using apb97.github.io.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddScoped<IntegerToRomanService>();
builder.Services.AddScoped<SayService>();
builder.Services.AddScoped<CountAndSayService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en");
localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>() { new QueryStringRequestCultureProvider(), new CookieRequestCultureProvider() };

var app = builder.Build();

CultureInfo culture;
var js = app.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("en");
    await js.InvokeVoidAsync("blazorCulture.set", "en");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await app.RunAsync();
