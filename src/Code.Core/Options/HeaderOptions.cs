using System;
using System.Collections.Generic;

namespace Code.Core;

public class HeaderOptions
{
    public string Key { get; set; }

    public string Title { get; set; }

    public Action<HeaderOptions>? OnClick { get; set; }

    public List<HeaderOptions>? Children { get; set; }

    public HeaderOptions(string key, string title, Action<HeaderOptions>? onClick = null,
        List<HeaderOptions>? children = null)
    {
        Key = key;
        Title = title;
        OnClick = onClick;
        Children = children;
    }
}