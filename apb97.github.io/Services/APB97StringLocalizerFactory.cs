using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Xml;

namespace apb97.github.io.Services
{
    public class APB97StringLocalizerFactory
    {
        private readonly HttpClient http;
        private readonly IOptions<LocalizationOptions> options;

        public APB97StringLocalizerFactory(HttpClient http, IOptions<LocalizationOptions> localizationOptions)
        {
            this.http = http;
            this.options = localizationOptions;
        }
        
        public async Task<Dictionary<string, string>> GetLocalization<T>(CultureInfo cultureInfo)
        {
            var results = new Dictionary<string, string>();

            Stream localizationStream;
            try
            {
                localizationStream = await http.GetStreamAsync(options.Value.ResourcesPath + typeof(T).Namespace?.Replace("apb97.github.io", string.Empty)?.Replace('.', '/') + '/' + typeof(T).Name + '.' + cultureInfo.Name + ".resx");
            }
            catch
            {
                try
                {
                    localizationStream = await http.GetStreamAsync(options.Value.ResourcesPath + typeof(T).Namespace?.Replace("apb97.github.io", string.Empty)?.Replace('.', '/') + '/' + typeof(T).Name + ".resx");
                }
                catch
                {
                    return results;
                }
            }
            using var stream = localizationStream;

            if (stream is null)
                return [];

            using var xml = XmlReader.Create(stream);
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
