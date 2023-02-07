namespace Code.Shared.Model;

public class TabModel
{
    public string Key { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public TabType Type { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// 参数 （Edit会携带“Path”，文件的地址 ）
    /// </summary>
    public IDictionary<string, object>? Parameters { get; set; }
}