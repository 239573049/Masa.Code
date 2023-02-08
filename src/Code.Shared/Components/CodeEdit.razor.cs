using BlazorComponent;
using Code.Shared.Model;
using Microsoft.AspNetCore.Components;
using Token.Events;

namespace Code.Shared;

public partial class CodeEdit
{
    #region Inject

    [Inject] public IKeyLoadEventBus KeyLoadEventBus { get; set; }

    #endregion

    private List<TabModel> Tabs = new()
    {
        new TabModel(Guid.NewGuid().ToString("N"), "首页", TabType.None)
    };

    private StringNumber selectTabModel { get; set; }

    private string Key;

    private string? functionWidth = "260px";
    protected override async Task OnInitializedAsync()
    {
        KeyLoadEventBus.Subscription(Constant.AddTab, async (value) =>
        {
            if (value is not TabModel model) return;

            if (model.Name.EndsWith(".png"))
            {
                model.Type = TabType.Component;
                model.Data = typeof(LoadImage); // 使用LoadImage组件加载图片
                Tabs.Add(model);
                selectTabModel = model.Key;
            }
            else if (Tabs.All(x => x.Key != model.Key))
            {
                model.Type = TabType.Edit;
                Tabs.Add(model);
                selectTabModel = model.Key;
            }

            _ = InvokeAsync(StateHasChanged);
        });
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task CloseFile(TabModel model)
    {
        selectTabModel = Tabs.FirstOrDefault()?.Key;
        Tabs.Remove(model);
        _ = InvokeAsync(StateHasChanged);
    }
}