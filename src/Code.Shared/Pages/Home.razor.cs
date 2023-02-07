using Code.Shared.Model;

namespace Code.Shared.Pages;

public partial class Home
{
    private Type Component;

    private static IEnumerable<ToolsModel> _toolsModels = new List<ToolsModel>()
    {
        new("mdi-file-multiple", typeof(CodeEdit)),
        new("mdi-cogs", typeof(Setting))
    };

    protected override void OnInitialized()
    {
        Component = typeof(CodeEdit);
        base.OnInitialized();
    }
}