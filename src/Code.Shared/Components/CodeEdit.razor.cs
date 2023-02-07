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

    private static List<TabModel> Tabs = new();

    public StringNumber selectTabModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        KeyLoadEventBus.Subscription(Constant.AddTab, (value) =>
        {
            if (value is not TabModel model) return;

            Tabs.Add(model);
            selectTabModel = Tabs.IndexOf(model);
        });
        await base.OnInitializedAsync();
    }
}