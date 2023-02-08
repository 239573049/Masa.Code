namespace Code.Shared.Options;

public class SettingOptions
{
    public string Code { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Type Component { get; set; }

    public SettingOptions(string code, string title, string description, Type component)
    {
        Code = code;
        Title = title;
        Description = description;
        Component = component;
    }
}
