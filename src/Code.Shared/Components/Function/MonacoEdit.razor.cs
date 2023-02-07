using Code.Shared.JsInterop;
using Code.Shared.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Semi.Design.Blazor;

namespace Code.Shared;

public partial class MonacoEdit
{
    #region Inject
    
    [Inject] public LanguageOptions[] LanguageOptions { get; set; }

    #endregion

    /// <summary>
    /// 本地文件加载模式
    /// </summary>
    [Parameter]
    public string? Path { get; set; }

    [Parameter] public IDictionary<string, object>? Parameters { get; set; }

    public SMonacoEditor SMonacoEditor { get; private set; }
    
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

    private static MonacoRegisterCompletionItemOptions[] RegisterCompletionItemProvider()
    {
        return Array.Empty<MonacoRegisterCompletionItemOptions>();
    }

    private async Task<object?> MonacoAsync()
    {
        try
        {
            return new
            {
                language = LanguageOptions
                    .FirstOrDefault(x => x.FileSuffix.Any(f => Path.EndsWith(f)))?.Languages ?? "text",
                value = await ReadCode(),
                automaticLayout = true, // 跟随父容器大小
                theme = "vs-dark"
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    private void GetCode()
    {
    }
}