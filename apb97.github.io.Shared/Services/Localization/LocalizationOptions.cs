namespace apb97.github.io.Shared.Services.Localization;

public class LocalizationOptions
{
    public string ResourcesPath { get; set; } = string.Empty;
    public required string ProjectNamespace { get; set; }
    public DataFormat DataFormat { get; set; } = DataFormat.JSON;
}
