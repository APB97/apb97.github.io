namespace apb97.github.io.Shared.Services.Localization;

public class StringLocalizer<T>(StringLocalizerFactory factory)
{
    private readonly StringLocalizerFactory factory = factory;
    private Dictionary<string, string>? localization;
    private string? culture;

    public bool IsReady => localization != null;

    public string? Culture => culture;

    public async Task InitializeAsync(string? cultureName)
    {
        if (cultureName == culture) return;
        localization = await factory.GetLocalizationAsync<T>(cultureName);
        culture = cultureName;
    }

    public string this[string key] => Localize(key);

    public string Localize(string key)
    {
        if (localization == null)
            return string.Empty;

        if (localization.TryGetValue(key, out var result) != true)
            return key;

        return result ?? key;
    }
}
