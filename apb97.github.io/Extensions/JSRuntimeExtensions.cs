using Microsoft.JSInterop;

namespace apb97.github.io.Extensions;

public static class JSRuntimeExtensions
{
    private const string ImportFunction = "import";

    public static ValueTask<IJSObjectReference> ImportAsync(this IJSRuntime js, string path)
    {
        return js.InvokeAsync<IJSObjectReference>(ImportFunction, path);
    }
}
