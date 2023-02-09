using Code.Shared.Model;
using Code.Shared.Utility;

namespace Code.Shared;

public partial class FileTree
{
    private static List<FileTreeModel> Files;

    private List<string> initiallyOpen = new() { "public" };

    private string? Path;

    protected override async Task OnInitializedAsync()
    {
        await LoadFileTree();
        KeyLoadEventBus.Subscription(Constant.LoadFileTree, async (v) =>
        {
            await LoadFileTree();
            _ = InvokeAsync(StateHasChanged);
        });
    }

    private async Task LoadFileTree()
    {
        Path = (await ConfigUtility.GetCodeSettingAsync())?.Path;
        Files = FileTrreUtility.GetFileTree(Path);
        await base.OnInitializedAsync();
    }

    private async Task OpenFile(FileTreeModel? fileTreeModel)
    {
        await KeyLoadEventBus.PushAsync(Constant.AddTab,
            new TabModel(fileTreeModel.Path, fileTreeModel.Name, TabType.Edit, null,
                new Dictionary<string, object> { { "Path", fileTreeModel.Path } }));
    }

    private async Task FetchUsers(FileTreeModel item)
    {
        item.Children.Clear();
        item.Children.AddRange(FileTrreUtility.GetFileTree(item.Path));
    }

    private void SetWork()
    {
        KeyLoadEventBus.PushAsync(Constant.SetExtension, nameof(Setting));
    }
}