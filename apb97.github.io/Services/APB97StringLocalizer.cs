using System.Globalization;

namespace apb97.github.io.Services
{
    public class APB97StringLocalizer<T>
    {
        private Dictionary<string, string>? localization;

        public Dictionary<string, string>? Localization => localization;

        private readonly APB97StringLocalizerFactory factory;

        public APB97StringLocalizer(APB97StringLocalizerFactory factory)
        {
            this.factory = factory;
        }

        public async Task InitializeAsync(CultureInfo? cultureInfo)
        {
            localization ??= await factory.GetLocalization<T>(cultureInfo ?? CultureInfo.CurrentUICulture);
        }

        public string Localize(string key)
        {
            if (localization?.TryGetValue(key, out var result) != true)
                return key;

            return result ?? key;
        }
    }
}
