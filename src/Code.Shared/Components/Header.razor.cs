namespace Code.Shared;

public partial class Header
{
    private async Task DarkChange(bool v)
    {
        LayoutOptions.Dark = v;
        await KeyLoadEventBus.PushAsync(Constant.Dark, v);
    }
}