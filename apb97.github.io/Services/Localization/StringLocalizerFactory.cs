using Microsoft.Extensions.Options;
using System.Text;
using System.Xml;

namespace apb97.github.io.Services.Localization
{
    public class StringLocalizerFactory(HttpClient http, IOptions<LocalizationOptions> localizationOptions)
    {
        public async Task<Dictionary<string, string>> GetLocalizationAsync<T>(string cultureName)
        {
            using var stream = await RequestLocalizationStreamAsync<T>(cultureName);

            return stream is null ? [] : RetreiveLocalization(stream);
        }

        private async Task<Stream?> RequestLocalizationStreamAsync<T>(string cultureName)
        {
            try
            {
                if (cultureName.StartsWith("en"))
                    return await http.GetStreamAsync(GetDefaultResourceFilePath<T>());

                return await http.GetStreamAsync(GetResourceFilePath<T>(cultureName));
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

        private static Dictionary<string, string> RetreiveLocalization(Stream stream)
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
}
