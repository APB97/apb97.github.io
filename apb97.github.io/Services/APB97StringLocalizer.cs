using System.Globalization;

namespace apb97.github.io.Services
{
    public class APB97StringLocalizer<T>(APB97StringLocalizerFactory factory)
    {
        private Dictionary<string, string>? localization;

        public bool IsReady => localization != null;

        public async Task InitializeAsync(CultureInfo? cultureInfo)
        {
            localization = await factory.GetLocalization<T>(cultureInfo ?? CultureInfo.CurrentUICulture);
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
