namespace Code.Shared.Model;

public class ToolsModel
{
    /// <summary>
    /// 渲染dom key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 加载的组件
    /// </summary>
    public Type Component { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    public ToolsModel(string icon, Type component, string? key = null, string description = "")
    {
        Key = key ?? Guid.NewGuid().ToString("N");
        Icon = icon;
        Component = component;
        Description = description;
    }
}