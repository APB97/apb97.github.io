using Microsoft.JSInterop;

namespace apb97.github.io.Extensions;

public static class JSObjectReferenceExtensions
{
    private const string AlertFunction = "alert";
    private const string SetSettingFunction = "setSetting";
    private const string GetSettingFunction = "getSetting";
    private const string RemoveSettingFunction = "removeSetting";
    private const string GetSessionSettingFunction = "getSessionSetting";
    private const string RemoveSessionSettingFunction = "removeSessionSetting";

    /// <summary>
    /// Show simple alert message
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    /// <param name="message">Message to be shown</param>
    public static async Task AlertAsync(this IJSObjectReference utilitiesModule, string message)
    {
        await utilitiesModule.InvokeVoidAsync(AlertFunction, message);
    }

    /// <summary>
    /// Set setting in localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static async Task SetSettingAsync<T>(this IJSObjectReference utilitiesModule, string key, T value)
    {
        await utilitiesModule.InvokeVoidAsync(SetSettingFunction, key, value);
    }

    /// <summary>
    /// Get setting from localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static async Task<T?> GetSettingAsync<T>(this IJSObjectReference utilitiesModule, string key)
    {
        return await utilitiesModule.InvokeAsync<T>(GetSettingFunction, key);
    }

    /// <summary>
    /// Remove setting from localStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static async Task RemoveSettingAsync(this IJSObjectReference utilitiesModule, string key)
    {
        await utilitiesModule.InvokeVoidAsync(RemoveSettingFunction, key);
    }

    /// <summary>
    /// Get setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static async Task<T?> GetSessionSettingAsync<T>(this IJSObjectReference utilitiesModule, string key)
    {
        return await utilitiesModule.InvokeAsync<T>(GetSessionSettingFunction, key);
    }

    /// <summary>
    /// Remove setting from sessionStorage
    /// </summary>
    /// <param name="utilitiesModule">JavaScript Module "./js/utilities.js"</param>
    public static async Task RemoveSessionSettingAsync(this IJSObjectReference utilitiesModule, string key)
    {
        await utilitiesModule.InvokeVoidAsync(RemoveSessionSettingFunction, key);
    }
}
