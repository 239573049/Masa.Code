using BlazorComponent;
using Code.Shared.Options;

namespace Code.Shared;

public partial class Setting
{
    public List<StringNumber> Panel = new()
    {
        "setting"
    };

    private List<SettingOptions> settingOptions = new();

    protected override void OnInitialized()
    {
        settingOptions.Add(new SettingOptions("code", "系统设置", "设置系统基本功能", typeof(EditorSetting)));

        base.OnInitialized();
    }
}
