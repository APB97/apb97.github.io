using System.Globalization;

namespace apb97.github.io.Services.Localization
{
    public class StringLocalizer<T>(StringLocalizerFactory factory)
    {
        private Dictionary<string, string>? localization;

        public bool IsReady => localization != null;

        public async Task InitializeAsync(CultureInfo? cultureInfo)
        {
            localization = await factory.GetLocalizationAsync<T>(cultureInfo?.Name ?? CultureInfo.CurrentUICulture.Name);
        }

        public async Task InitializeAsync(string cultureName)
        {
            localization = await factory.GetLocalizationAsync<T>(cultureName);
        }

        public string Localize(string key)
        {
            if (localization == null)
                return string.Empty;

            if (localization.TryGetValue(key, out var result) != true)
                return key;

            return result ?? key;
        }
    }
}
