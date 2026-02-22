using apb97.github.io.Shared;
using System.ComponentModel.DataAnnotations;

namespace apb97.github.io.WebSudoku.Services;

public sealed class SettingsService(UtilityService utilityService)
{
    private readonly Lazy<Task> settingsTask = new(() => LoadSettingsAsync(utilityService));

    public const string DesiredBlankCells = "WebSudoku-desiredBlankCells";
    public const string CellBlankingAttempts = "WebSudoku-cellBlankingAttempts";

    public async Task<IReadOnlyDictionary<string, object>> GetSettingsAsync()
    {
        await settingsTask.Value;
        return settingValues;
    }

    public async Task<IReadOnlyDictionary<string, ValidationAttribute[]>> GetValidationsAsync()
    {
        await settingsTask.Value;
        return settingValidations;
    }

    private static readonly Dictionary<string, object> settingValues = new()
    {
        { DesiredBlankCells, 35 },
        { CellBlankingAttempts, 32 }
    };

    private static readonly Dictionary<string, ValidationAttribute[]> settingValidations = new()
    {
        { DesiredBlankCells, [new RangeAttribute(20, 45)] },
        { CellBlankingAttempts, [new RangeAttribute(4, 128)] }
    };

    private static async Task<Dictionary<string, object>> LoadSettingsAsync(UtilityService utilityService)
    {
        var settingValues = new Dictionary<string, object>();
        foreach (var setting in settingValues.ToArray())
        {
            var settingAsString = await utilityService.GetSettingAsync<string>(setting.Key);
            if (string.IsNullOrEmpty(settingAsString)) continue;
            switch (setting.Value.GetType().Name)
            {
                case nameof(Int32):
                    if (int.TryParse(settingAsString, out int settingValue))
                        settingValues[setting.Key] = settingValue;
                    break;
                default:
                    break;
            }
        }
        return settingValues;
    }

    public async Task UpdateSettingAsync(string key, object value)
    {
        await settingsTask.Value;
        if (!settingValidations.TryGetValue(key, out var validations) || validations.Any(v => !v.IsValid(value))) return;
        settingValues[key] = value;
        await utilityService.SetSettingAsync(key, value);
    }

    public async Task<int> GetIntAsync(string key)
    {
        await settingsTask.Value;
        return int.TryParse(settingValues[key].ToString(), out var value) ? value : default;
    }
}
