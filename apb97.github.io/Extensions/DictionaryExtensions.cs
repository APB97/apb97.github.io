namespace apb97.github.io.Extensions
{
    public static class DictionaryExtensions
    {
        public static string Localize(this Dictionary<string, string> localization, string name)
        {
            if (!localization.TryGetValue(name, out var result))
                return name;

            return result;
        }
    }
}
