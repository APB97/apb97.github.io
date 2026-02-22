using apb97.github.io.Shared;
using System.ComponentModel.DataAnnotations;

namespace apb97.github.io.WebSudoku.Services;

public sealed class SettingsService(UtilityService utilityService)
{
    private readonly Lazy<Task> settingsTask = new(() => LoadSettingsAsync(utilityService));

    public const string DesiredBlankCells = "WebSudoku-desiredBlankCells";
    public const string CellBlankingAttempts = "WebSudoku-cellBlankingAttempts";
    public const string PrintFilledValues = "WebSudoku-printFilledValues";
    public const string PrintHighlightedErrors = "WebSudoku-printHighlightedErrors";
    public const string SudokuStateKey = "WebSudoku-sudokuState";
    public const string TimerStateKey = "WebSudoku-timerState";
    public const string BoardWidthPercentage = "WebSudoku-PrintMultiple-boardWidthPercentage";
    public const string UseMoreResources = "WebSudoku-PrintMultiple-useMoreResources";
    public const string BoardsToGenerate = "WebSudoku-PrintMultiple-boardsToGenerate";
    
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

    public async Task<IReadOnlyDictionary<string, string>> GetTypesAsync()
    {
        await settingsTask.Value;
        return settingTypes;
    }

    private static readonly Dictionary<string, object> settingValues = new()
    {
        { DesiredBlankCells, 35 },
        { CellBlankingAttempts, 32 },
        { BoardsToGenerate, 4 },
        { UseMoreResources, false },
        { BoardWidthPercentage, 100 },
        { PrintFilledValues, false },
        { PrintHighlightedErrors, false },
    };

    private static readonly Dictionary<string, ValidationAttribute[]> settingValidations = new()
    {
        { DesiredBlankCells, [new RangeAttribute(20, 45)] },
        { CellBlankingAttempts, [new RangeAttribute(4, 128)] },
        { BoardsToGenerate, [new RangeAttribute(1, 32)] },
        { BoardWidthPercentage, [new RangeAttribute(10, 100)] },
    };

    private static readonly Dictionary<string, string> settingTypes = new()
    {
        { BoardWidthPercentage, nameof(Single) },
        { UseMoreResources, nameof(Boolean) },
        { PrintFilledValues, nameof(Boolean) },
        { PrintHighlightedErrors, nameof(Boolean) },
    };

    private static async Task LoadSettingsAsync(UtilityService utilityService)
    {
        foreach (var setting in settingValues.ToArray())
        {
            var settingAsString = await utilityService.GetSettingAsync<string>(setting.Key);
            if (string.IsNullOrEmpty(settingAsString)) continue;
            switch (settingTypes.TryGetValue(setting.Key, out var type) ? type : setting.Value.GetType().Name)
            {
                case nameof(Int32):
                    {
                        if (int.TryParse(settingAsString, out int settingValue))
                            settingValues[setting.Key] = settingValue;
                        break;
                    }
                case nameof(Single):
                    {
                        if (float.TryParse(settingAsString, out float settingValue))
                            settingValues[setting.Key] = settingValue;
                        break;
                    }
                case nameof(Boolean):
                    {
                        if (bool.TryParse(settingAsString, out bool settingValue))
                            settingValues[setting.Key] = settingValue;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    public async Task UpdateSettingAsync(string key, object value)
    {
        await settingsTask.Value;
        if (settingValidations.TryGetValue(key, out var validations) && validations.Any(v => !v.IsValid(value))) return;
        settingValues[key] = value;
        await utilityService.SetSettingAsync(key, value);
    }

    public async Task<int> GetIntAsync(string key)
    {
        await settingsTask.Value;
        return settingValues.TryGetValue(key, out var setting) ? (int.TryParse(setting.ToString(), out var value) ? value : default) : default;
    }

    public async Task<float> GetFloatAsync(string key)
    {
        await settingsTask.Value;
        return settingValues.TryGetValue(key, out var setting) ? (float.TryParse(setting.ToString(), out var value) ? value : default) : default;
    }

    public async Task<bool> GetBoolAsync(string key)
    {
        await settingsTask.Value;
        return settingValues.TryGetValue(key, out var setting) && bool.TryParse(setting.ToString(), out var value) && value;
    }

    public async Task<string> GetStringAsync(string key)
    {
        await settingsTask.Value;
        return settingValues.TryGetValue(key, out var value) ? value?.ToString() ?? string.Empty : string.Empty;
    }
}
