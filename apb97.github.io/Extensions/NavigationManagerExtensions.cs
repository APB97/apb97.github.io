using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace apb97.github.io.Extensions;

public static class NavigationManagerExtensions
{
    private const string DestinationKey = "destination";

    public static async Task NavigateToStoredDestinationIfAnyAsync(this NavigationManager navigation, IJSRuntime js)
    {
        var destination = await js.InvokeAsync<string>("sessionStorage.getItem", DestinationKey);
        if (string.IsNullOrEmpty(destination)) return;
        await js.InvokeVoidAsync("sessionStorage.removeItem", DestinationKey);
        navigation.NavigateTo(destination[1..]);
    }
}
