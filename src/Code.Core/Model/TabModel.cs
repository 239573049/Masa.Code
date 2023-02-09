using System.Collections.Generic;

namespace Code.Core;

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
    public object? Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IDictionary<string, object>? Parameters { get; set; }

    public TabModel(string key, string name, TabType type, object data = null,
        IDictionary<string, object>? parameters = null)
    {
        Key = key;
        Name = name;
        Type = type;
        Data = data;
        Parameters = parameters ?? new Dictionary<string, object>();
    }
}