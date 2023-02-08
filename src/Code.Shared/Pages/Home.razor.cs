using Code.Shared.Model;
using Code.Shared.Options;
using Code.Shared.Utility;

namespace Code.Shared.Pages;

public partial class Home
{
    private Type Component = typeof(CodeEdit);

    public LayoutOptions LayoutOptions { get; set; } = new();

    private static IEnumerable<ToolsModel> _toolsModels = new List<ToolsModel>()
    {
        new("mdi-file-multiple", typeof(CodeEdit)),
        new("mdi-cogs", typeof(Setting))
    };

    protected override async Task OnInitializedAsync()
    {
        var model = await ConfigUtility.GetCodeSettingAsync();
        LayoutOptions.Dark = model.Dark;
        KeyLoadEventBus.Subscription(Constant.Dark, (value) =>
        {
            if (value is not bool dark) return;

            LayoutOptions.Dark = dark;
            _ = InvokeAsync(StateHasChanged);
        });
        await base.OnInitializedAsync();
    }
}