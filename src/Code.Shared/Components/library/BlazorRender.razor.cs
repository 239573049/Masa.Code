using Masa.Blazor.Extensions.Languages.Razor;
using Microsoft.AspNetCore.Components;

namespace Code.Shared;

public partial class BlazorRender
{
    [Parameter] public string Class { get; set; }

    [Parameter] public string Style { get; set; }

    public Type? ComponentType { get; set; }

    private string code;

    [Parameter]
    public string Code
    {
        get => code;
        set
        {
            if (code == value)
            {
                return;
            }

            code = value;
            SetCode();
        }
    }

    private string Intro = @"# Masa Code

支持masa组件快速调试编辑的一款轻量级编辑器

项目结构：`Blazor`，`Masa Blazor`，`Semi Design Blazor Monaco`

支持md预览（虚拟渲染支持超大文件渲染编辑）

支持文本高亮编辑，支持图片预览，支持`Masa Blazor`组件快速预览";

    private async void SetCode()
    {
        await Task.Run(async () =>
        {
            try
            {
                ComponentType = RazorCompile.CompileToType(new CompileRazorOptions()
                {
                    ConcurrentBuild = true,
                    Code = Code // TODO: ConcurrentBuild is guaranteed to be false under WebAssembly because Webassembly does not support multithreading
                });
                _ = InvokeAsync(StateHasChanged);
            }
            catch (Exception e)
            {
                _ = InvokeAsync(async () => { await PopupService.ToastErrorAsync(e.Message); });
            }
        });
    }
}