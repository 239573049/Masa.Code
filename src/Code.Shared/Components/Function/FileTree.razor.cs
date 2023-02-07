using Code.Shared.Model;
using Code.Shared.Utility;
using Microsoft.AspNetCore.Components;
using Token.Events;

namespace Code.Shared;

public partial class FileTree
{
    #region Inject

    [Inject] public IKeyLoadEventBus KeyLoadEventBus { get; set; }

    #endregion

    private static List<FileTreeModel> Files = new();

    private List<string> initiallyOpen = new() { "public" };

    protected override async Task OnInitializedAsync()
    {
        Files = FileTrreUtility.GetFileTree((await ConfigUtility.GetCodeSettingAsync()).Path);

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
}