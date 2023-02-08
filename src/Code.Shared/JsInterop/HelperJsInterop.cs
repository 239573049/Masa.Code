using Microsoft.JSInterop;

namespace Code.Shared.JsInterop;

public class HelperJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public HelperJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Code.Shared/js/helper.js").AsTask());
    }

    public async ValueTask<string> ByteToUrl(byte[] bytes)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("byteToUrl", bytes);
    }

    public async ValueTask onKeydown<T>(string id,DotNetObjectReference<T> objRef)where T : class
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("onKeydown", id, objRef);
    }

    public async ValueTask<string> RevokeUrl(string url)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("revokeUrl", url);
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
