using Code.Shared.JsInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Semi.Design.Blazor;
using System.Text;
using Masa.Blazor.Presets;

namespace Code.Shared;

public partial class MonacoEdit : IAsyncDisposable
{
    #region Inject

    [Inject] public LanguageOptions[] LanguageOptions { get; set; }

    #endregion

    /// <summary>
    /// 本地文件加载模式
    /// </summary>
    [Parameter]
    public string? Path { get; set; }

    [Parameter] public string? Language { get; set; }

    [Parameter] public string? Key { get; set; }

    [Parameter] public string Height { get; set; } = "100%";

    [Parameter] public string Width { get; set; }

    [Parameter] public IDictionary<string, object>? Parameters { get; set; }

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    public SMonacoEditor SMonacoEditor { get; private set; }

    private DotNetObjectReference<MonacoEdit> _reference;

    private async Task<string> ReadCode()
    {
        if (!File.Exists(Path)) return string.Empty;

        try
        {
            return await File.ReadAllTextAsync(Path);
        }
        catch
        {
            return string.Empty;
        }
    }

    [JSInvokable("OnKeydown")]
    public async Task<bool> OnKeydown(int code, bool ctrl)
    {
        if (code == 83 && ctrl)
        {
            // 保存文件内容
            _ = Task.Run(SaveCode);
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }

    protected override void OnInitialized()
    {
        Key ??= Guid.NewGuid().ToString("N");
        _reference = DotNetObjectReference.Create(this);
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await HelperJsInterop.onKeydown(Key, _reference);
        }

        await base.OnAfterRenderAsync(firstRender);
    }


    private static MonacoRegisterCompletionItemOptions[] RegisterCompletionItemProvider()
    {
        return Array.Empty<MonacoRegisterCompletionItemOptions>();
    }

    private async Task<object?> MonacoAsync()
    {
        var code = await ReadCode();
        await ValueChanged.InvokeAsync(code);
        try
        {
            return new
            {
                language = !string.IsNullOrEmpty(Language)
                    ? Language
                    : LanguageOptions
                        .FirstOrDefault(x => x.FileSuffix.Any(f => Path?.EndsWith(f) == true))?.Languages ?? "text",
                value = code,
                automaticLayout = true, // 跟随父容器大小
                theme = "vs-dark"
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task SaveCode()
    {
        if (File.Exists(Path))
        {
            _ = InvokeAsync(async () => { await PopupService.ToastInfoAsync("保存中..."); });
            var code = await SMonacoEditor.GetValue();
            await using var stream = File.Create(Path);
            await stream.WriteAsync(Encoding.UTF8.GetBytes(code));
            await stream.FlushAsync();
            stream.Close();
            _ = InvokeAsync(async () => { await PopupService.ToastSuccessAsync("保存文件成功"); });
        }
    }

    public async ValueTask DisposeAsync()
    {
        await SaveCode();
    }
}