using apb97.github.io.SimpleResxToJson.Shared;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace apb97.github.io.Services.Localization;

public class StringLocalizerFactory(HttpClient http, IOptions<LocalizationOptions> localizationOptions)
{
    public async Task<Dictionary<string, string>?> GetLocalizationAsync<T>(string? cultureName)
    {
        using var stream = await RequestLocalizationStreamAsync<T>(cultureName);

        return stream is null ? null : RetrieveLocalization(stream);
    }

    private async Task<Stream?> RequestLocalizationStreamAsync<T>(string? cultureName)
    {
        if (cultureName is null)
        {
            return null;
        }

        try
        {
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
        return $"{localizationOptions.Value.ResourcesPath}{GetResourcePathWithNoExtension<T>()}.resx";
    }

    private string GetResourceFilePath<T>(string cultureName)
    {
        return $"{localizationOptions.Value.ResourcesPath}{GetResourcePathWithNoExtension<T>()}.{cultureName}.resx";
    }

    private string GetDefaultJsonFilePath<T>()
    {
        return $"{localizationOptions.Value.ResourcesPath}{GetResourcePathWithNoExtension<T>()}.json";
    }

    private string GetJsonFilePath<T>(string cultureName)
    {
        return $"{localizationOptions.Value.ResourcesPath}{GetResourcePathWithNoExtension<T>()}.{cultureName}.json";
    }

    private string GetResourcePathWithNoExtension<T>()
    {
        var type = typeof(T);
        var builder = new StringBuilder();
        if (type.Namespace?.StartsWith(localizationOptions.Value.ProjectNamespace) == true)
        {
            builder.Append(type.Namespace.Remove(0, localizationOptions.Value.ProjectNamespace.Length).Replace('.', '/'));
        }
        else if (type.Namespace != null)
        {
            builder.Append(type.Namespace.Replace('.', '/'));
        }
        if (builder[^1] != '/')
        {
            builder.Append('/');
        }
        builder.Append(type.Name);

        return builder.ToString();
    }

    private Dictionary<string, string> RetrieveLocalization(Stream stream)
    {
        switch (localizationOptions.Value.DataFormat)
        {
            case DataFormat.JSON:
                return RetrieveJsonLocalization(stream);
            case DataFormat.RESX:
                return RetrieveResxLocalization(stream);
            default:
                return [];
        }
    }

    private Dictionary<string, string> RetrieveJsonLocalization(Stream stream)
    {
        return JsonSerializer.Deserialize(stream, ResxDataContext.Default.ResxData)?.Strings ?? [];
    }

    private static Dictionary<string, string> RetrieveResxLocalization(Stream stream)
    {
        using var xml = XmlReader.Create(stream);
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
}
