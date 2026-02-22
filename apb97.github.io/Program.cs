using apb97.github.io;
using apb97.github.io.Services;
using apb97.github.io.Shared;
using apb97.github.io.Shared.Services.Localization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<SayService>();
builder.Services.AddSingleton<CountAndSayService>();

builder.Services.AddSingleton(_ => Options.Create(new LocalizationOptions
{
    ResourcesPath = "Resources",
    ProjectNamespace = "apb97.github.io",
    DataFormat = DataFormat.JSON,
}));
builder.Services.AddSingleton<StringLocalizerFactory>();
builder.Services.AddSingleton(typeof(StringLocalizer<>));

builder.Services.AddScoped<UtilityService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

await app.RunAsync();
