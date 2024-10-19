using apb97.github.io;
using apb97.github.io.Services;
using apb97.github.io.Services.Localization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<SayService>();
builder.Services.AddScoped<CountAndSayService>();

builder.Services.AddSingleton(_ => Options.Create(new LocalizationOptions
{
    ResourcesPath = "Resources",
    ProjectNamespace = "apb97.github.io"
}));
builder.Services.AddScoped<StringLocalizerFactory>();
builder.Services.AddScoped(typeof(StringLocalizer<>));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

//CultureInfo culture;
//var js = app.Services.GetRequiredService<IJSRuntime>();
//var result = await js.InvokeAsync<string>("blazorCulture.get");

//if (result != null)
//{
//    culture = new CultureInfo(result);
//}
//else
//{
//    culture = CultureInfo.InvariantCulture;
//    await js.InvokeVoidAsync("blazorCulture.set", "en-US");
//}

//CultureInfo.DefaultThreadCurrentCulture = culture;
//CultureInfo.DefaultThreadCurrentUICulture = culture;

await app.RunAsync();
