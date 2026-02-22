using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Extensions;

public static class JSObjectReferenceExtensions
{
    private const string AlertFunction = "alert";
    private const string SetSettingFunction = "setSetting";
    private const string GetSettingFunction = "getSetting";
    private const string RemoveSettingFunction = "removeSetting";
    private const string GetSessionSettingFunction = "getSessionSetting";
    private const string RemoveSessionSettingFunction = "removeSessionSetting";
    private const string DownloadFileFunction = "downloadFile";

    /// <summary>
    /// Show simple alert message
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    /// <param name="message">Message to be shown</param>
    public static ValueTask AlertAsync(this IJSObjectReference utilitiesModule, string message)
    {
        return utilitiesModule.InvokeVoidAsync(AlertFunction, message);
    }

    /// <summary>
    /// Set setting in localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static ValueTask SetSettingAsync<T>(this IJSObjectReference utilitiesModule, string key, T value)
    {
        return utilitiesModule.InvokeVoidAsync(SetSettingFunction, key, value);
    }

    /// <summary>
    /// Get setting from localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static ValueTask<T> GetSettingAsync<T>(this IJSObjectReference utilitiesModule, string key)
    {
        return utilitiesModule.InvokeAsync<T>(GetSettingFunction, key);
    }

    /// <summary>
    /// Remove setting from localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static ValueTask RemoveSettingAsync(this IJSObjectReference utilitiesModule, string key)
    {
        return utilitiesModule.InvokeVoidAsync(RemoveSettingFunction, key);
    }

    /// <summary>
    /// Get setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static ValueTask<T> GetSessionSettingAsync<T>(this IJSObjectReference utilitiesModule, string key)
    {
        return utilitiesModule.InvokeAsync<T>(GetSessionSettingFunction, key);
    }

    /// <summary>
    /// Remove setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static ValueTask RemoveSessionSettingAsync(this IJSObjectReference utilitiesModule, string key)
    {
        return utilitiesModule.InvokeVoidAsync(RemoveSessionSettingFunction, key);
    }

    public static ValueTask DownloadFileAsync(this IJSObjectReference utilitiesModule, string name, string contents, string mimeType)
    {
        return utilitiesModule.InvokeVoidAsync(DownloadFileFunction, name, contents, mimeType);
    }
}
