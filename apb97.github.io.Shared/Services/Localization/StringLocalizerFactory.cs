using apb97.github.io.SimpleResxToJson.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace apb97.github.io.Shared.Services.Localization;

public class StringLocalizerFactory(IServiceScopeFactory scopeFactory, IOptions<LocalizationOptions> localizationOptions)
{
    private readonly IOptions<LocalizationOptions> localizationOptions = localizationOptions;

    public async Task<Dictionary<string, string>?> GetLocalizationAsync<T>(string? cultureName)
    {
        using var stream = await RequestLocalizationStreamAsync<T>(cultureName);

        return stream is null ? null : await RetrieveLocalizationAsync(stream);
    }

    private async Task<Stream?> RequestLocalizationStreamAsync<T>(string? cultureName)
    {
        if (cultureName is null)
        {
            return null;
        }

        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();
            var http = scope.ServiceProvider.GetRequiredService<HttpClient>();
            switch (localizationOptions.Value.DataFormat)
            {
                case DataFormat.JSON:
                    if (cultureName.StartsWith("en"))
                        return await http.GetStreamAsync(GetDefaultJsonFilePath<T>());

                    return await http.GetStreamAsync(GetJsonFilePath<T>(cultureName));
                case DataFormat.RESX:
                    if (cultureName.StartsWith("en"))
                        return await http.GetStreamAsync(GetDefaultResourceFilePath<T>());

                    return await http.GetStreamAsync(GetResourceFilePath<T>(cultureName));
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private string GetDefaultResourceFilePath<T>()
    {
        return $"{GetResourcePathWithNoExtension<T>(localizationOptions.Value.ResourcesPath)}.resx";
    }

    private string GetResourceFilePath<T>(string cultureName)
    {
        return $"{GetResourcePathWithNoExtension<T>(localizationOptions.Value.ResourcesPath)}.{cultureName}.resx";
    }

    private string GetDefaultJsonFilePath<T>()
    {
        return $"{GetResourcePathWithNoExtension<T>(localizationOptions.Value.ResourcesPath)}.json";
    }

    private string GetJsonFilePath<T>(string cultureName)
    {
        return $"{GetResourcePathWithNoExtension<T>(localizationOptions.Value.ResourcesPath)}.{cultureName}.json";
    }

    private string GetResourcePathWithNoExtension<T>(string resourcesPath)
    {
        var type = typeof(T);
        var builder = new StringBuilder();
        if (type.Namespace?.StartsWith("apb97.github.io.Shared") == true)
        {
            builder.Append("_content/apb97.github.io.Shared/").Append(resourcesPath).Append(type.Namespace.Replace("apb97.github.io", string.Empty).Replace('.', '/'));
        }
        else if (type.Namespace?.StartsWith(localizationOptions.Value.ProjectNamespace) == true)
        {
            builder.Append(resourcesPath).Append(type.Namespace[localizationOptions.Value.ProjectNamespace.Length..].Replace('.', '/'));
        }
        else if (type.Namespace != null)
        {
            builder.Append(resourcesPath).Append(type.Namespace.Replace('.', '/'));
        }
        if (builder[^1] != '/')
        {
            builder.Append('/');
        }
        builder.Append(type.Name);

        return builder.ToString();
    }

    private async Task<Dictionary<string, string>> RetrieveLocalizationAsync(Stream stream)
    {
        return localizationOptions.Value.DataFormat switch
        {
            DataFormat.JSON => await RetrieveJsonLocalization(stream),
            DataFormat.RESX => await RetrieveResxLocalization(stream),
            _ => [],
        };
    }

    private static async Task<Dictionary<string, string>> RetrieveJsonLocalization(Stream stream)
    {
        var data = await JsonSerializer.DeserializeAsync(stream, ResxDataContext.Default.ResxData);
        return data?.Strings ?? [];
    }

    private static async Task<Dictionary<string, string>> RetrieveResxLocalization(Stream stream)
    {
        using var memoryStream = await CreateInMemoryStreamCopyAsync(stream);
        using var xml = XmlReader.Create(memoryStream);
        var results = new Dictionary<string, string>();
        while (xml.ReadToFollowing("data"))
        {
            var key = xml.GetAttribute("name");
            xml.ReadToDescendant("value");
            var value = xml.ReadElementContentAsString();
            if (key == null || value == null) continue;
            results[key] = value;
        }
        return results;
    }

    private static async Task<MemoryStream> CreateInMemoryStreamCopyAsync(Stream stream)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }
}
