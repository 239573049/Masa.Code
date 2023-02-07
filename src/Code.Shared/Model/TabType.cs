namespace Code.Shared.Model;

public enum TabType
{
    None =-1,
    
    /// <summary>
    /// 编辑器（Monaco）
    /// </summary>
    Edit = 0,
    
    /// <summary>
    /// 组件（Razor）
    /// </summary>
    Component
}