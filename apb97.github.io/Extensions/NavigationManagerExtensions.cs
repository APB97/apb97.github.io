using apb97.github.io.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace apb97.github.io.Extensions;

public static class NavigationManagerExtensions
{
    private const string DestinationKey = "destination";

    public static async Task NavigateToStoredDestinationIfAnyAsync(this NavigationManager navigation, IJSRuntime js)
    {
        await using var utiltiesModule = await js.ImportAsync(JSModules.UtilitiesModule);
        var destination = await utiltiesModule.GetSessionSettingAsync<string>(DestinationKey);
        if (string.IsNullOrEmpty(destination)) return;
        await utiltiesModule.RemoveSessionSettingAsync(DestinationKey);
        navigation.NavigateTo(destination[1..]);
    }
}
