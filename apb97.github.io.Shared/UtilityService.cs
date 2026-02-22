using Microsoft.JSInterop;

namespace apb97.github.io.Shared;

public sealed class UtilityService(IJSRuntime jsRuntime) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/apb97.github.io.Shared/js/utilities.js").AsTask());

    private const string SetSettingFunction = "setSetting";
    private const string GetSettingFunction = "getSetting";
    private const string GetSessionSettingFunction = "getSessionSetting";
    private const string RemoveSessionSettingFunction = "removeSessionSetting";
    private const string AlertFunction = "alert";
    private const string RemoveSettingFunction = "removeSetting";
    private const string DownloadFileFunction = "downloadFile";

    /// <summary>
    /// Set setting in localStorage
    /// </summary>
    public async Task SetSettingAsync<T>(string key, T value)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(SetSettingFunction, key, value);
    }

    /// <summary>
    /// Get setting from localStorage
    /// </summary>
    public async Task<T?> GetSettingAsync<T>(string key)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<T>(GetSettingFunction, key);
    }


    /// <summary>
    /// Get setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public async Task<T?> GetSessionSettingAsync<T>(string key)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<T>(GetSessionSettingFunction, key);
    }

    /// <summary>
    /// Remove setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public async Task RemoveSessionSettingAsync(string key)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(RemoveSessionSettingFunction, key);
    }

    /// <summary>
    /// Show simple alert message
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    /// <param name="message">Message to be shown</param>
    public async Task AlertAsync(string message)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(AlertFunction, message);
    }

    /// <summary>
    /// Remove setting from localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public async Task RemoveSettingAsync(string key)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(RemoveSettingFunction, key);
    }

    public async Task DownloadFileAsync(string name, string contents, string mimeType)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(DownloadFileFunction, name, contents, mimeType);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
