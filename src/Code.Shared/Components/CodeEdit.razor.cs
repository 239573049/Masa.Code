using BlazorComponent;
using Code.MarkDown;
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
        KeyLoadEventBus.Subscription(Constant.AddTab, value =>
        {
            if (value is not TabModel model) return;

            if (model.Name.EndsWith(".png"))
            {
                model.Type = TabType.Component;
                model.Data = typeof(LoadImage); // 使用LoadImage组件加载图片
            }else if (model.Name.EndsWith(".md"))
            {
                model.Type = TabType.Component;
                model.Data = typeof(MarkDownEdit); // 使用LoadImage组件加载图片
            }
            else if (Tabs.All(x => x.Key != model.Key))
            {
                model.Type = TabType.Edit;
            }

            // 当model被事件处理使用事件model
            var loadModel = CodeEventBus.LoadTabModelHandle?.Invoke(model);
            if (loadModel != null)
            {
                model = loadModel;
            }
            Tabs.Add(model);

            selectTabModel = model.Key;
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