namespace Code.Shared.Model;

public class FileTreeModel
{
    public string Icon { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public bool File { get; set; }

    public List<FileTreeModel> Children { get; set; }

    public FileTreeModel(string name,string path,bool file)
    {
        Name = name;
        File = file;
        Path = path;
        if (!file)
        {
            Children = new List<FileTreeModel>();
        }
    }

    public FileTreeModel(string name,string path, string icon,bool file)
    {
        Name = name;
        Path = path;
        File = file;
        Icon = icon;
        if (!file)
        {
            Children = new List<FileTreeModel>();
        }
    }

    public FileTreeModel(string name,string path,bool file, List<FileTreeModel> children)
    {
        Name = name;
        Path = path;
        File = file;
        Children = children;
    }
}