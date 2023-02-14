using Microsoft.JSInterop;

namespace Code.Shared.JsInterop;

public class CodeComponentJsInterop: IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public CodeComponentJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Code.Shared/js/code-component.js").AsTask());
    }

    public async ValueTask Mousedown(string id)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("mousedown", id);
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