using Code.Shared;
using Code.Shared.JsInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Code.MarkDown;

public partial class MarkDownEdit
{
    public MonacoEdit monacoEdit { get; private set; }

    private DotNetObjectReference<MarkDownEdit> _reference;

    private string Value;

    private string Html;

    [Parameter]
    public string Path { get; set; }

    [JSInvokable("OnDidChangeModelContent")]
    public async Task OnDidChangeModelContent(dynamic e)
    {        
        
        Value = await monacoEdit.SMonacoEditor.Module.GetValue(monacoEdit.SMonacoEditor.Monaco);
        _ = InvokeAsync(StateHasChanged);
    }
    protected override void OnInitialized()
    {
        _reference = DotNetObjectReference.Create(this);
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(500);
                await HelperJsInterop.onDidChangeModelContent(monacoEdit.SMonacoEditor.Monaco, _reference, "OnDidChangeModelContent");
            });
        }
        await base.OnAfterRenderAsync(firstRender);
    }
}