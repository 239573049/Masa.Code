using Code.Shared.Options;

namespace Code.Shared;

public partial class Header
{
    private List<HeaderOptions> _headerOptions = new();

    protected override void OnInitialized()
    {
        var file = new HeaderOptions(Guid.NewGuid().ToString(), "文件", (v) => { }, new List<HeaderOptions>()
        {
            new (Guid.NewGuid().ToString(), "打开文件", (v) => { })
        });
        _headerOptions.Add(file);
        base.OnInitialized();
    }

    private async Task DarkChange(bool v)
    {
        LayoutOptions.Dark = v;
        await KeyLoadEventBus.PushAsync(Constant.Dark, v);
    }
}