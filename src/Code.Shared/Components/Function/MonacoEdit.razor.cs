using Microsoft.AspNetCore.Components;
using Semi.Design.Blazor;

namespace Code.Shared;

public partial class MonacoEdit
{
    /// <summary>
    /// 本地文件加载模式
    /// </summary>
    [Parameter]
    public string? Path { get; set; }

    [Parameter]
    public IDictionary<string, object>? Parameters { get; set; }
    
    public SMonacoEditor SMonacoEditor { get; private set; }

    private static MonacoRegisterCompletionItemOptions[] RegisterCompletionItemProvider()
    {
        var trigger = new[]
        {
            "<"
        };
        var completionItems = new CompletionItem[]
            { };

        return new MonacoRegisterCompletionItemOptions[]
        {
            new()
            {
                Language = "razor",
                items = completionItems,
                TriggerCharacters = trigger
            }
        };
    }

    private async Task<object?> MonacoAsync()
    {
        try
        {
            return new
            {
                language = "razor",
                value = "",
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