using Microsoft.AspNetCore.Components;

namespace apb97.github.io.Shared.Extensions;

public static class NavigationManagerExtensions
{
    private const string DestinationKey = "destination";

    public static async Task NavigateToStoredDestinationIfAnyAsync(this NavigationManager navigation, UtilityService utilities)
    {
        var destination = await utilities.GetSessionSettingAsync<string>(DestinationKey);
        if (string.IsNullOrEmpty(destination)) return;
        await utilities.RemoveSessionSettingAsync(DestinationKey);
        navigation.NavigateTo(destination[1..]);
    }
}
