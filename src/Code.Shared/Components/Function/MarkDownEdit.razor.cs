using Code.Shared;
using Code.Shared.JsInterop;
using Code.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Code.MarkDown;

public partial class MarkDownEdit
{
    public MonacoEdit monacoEdit { get; private set; }

    private DotNetObjectReference<MarkDownEdit> _reference;

    private string Value;

    private string Html;

    [Parameter] public string Path { get; set; }

    [JSInvokable("OnDidChangeModelContent")]
    public async Task OnDidChangeModelContent(dynamic e)
    {
        await RenderMarkDown();
    }

    [JSInvokable("onDidScrollChange")]
    public async Task onDidScrollChange()
    {
        await RenderMarkDown();
    }

    /// <summary>
    /// 渲染MD
    /// </summary>
    public async Task RenderMarkDown()
    {
        // 得到可见行
        var visibles =
            await HelperJsInterop.InvokeAsync<VisibleModel[]>("getVisibleRanges", monacoEdit.SMonacoEditor.Monaco);
        if (visibles.Length >= 1)
        {
            var visible = visibles[0];
            int star = visible.startLineNumber;
            int end = visible.endLineNumber - visible.startLineNumber + 2;
            // 将数据拆分只显示可见内容的数据渲染
            Html = string.Join(Environment.NewLine,
                (await monacoEdit.SMonacoEditor.GetValue())
                .Split(Environment.NewLine).Skip(star > 5 ? star - 2 : star).Take(end));

            _ = InvokeAsync(StateHasChanged);
        }
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
                await RenderMarkDown();
                await HelperJsInterop.onDidChangeModelContent(monacoEdit.SMonacoEditor.Monaco, _reference,
                    "OnDidChangeModelContent");

                // 监听滚动条事件
                await HelperJsInterop.onDidScrollChange(monacoEdit.SMonacoEditor.Monaco, _reference,
                    "onDidScrollChange");
            });
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}