using Code.Shared.Model;

namespace Code.Shared.Utility;

public class FileTrreUtility
{
    /// <summary>
    /// 文件渲染图标
    /// </summary>
    static Dictionary<string, string?> Icons = new()
    {
        { ".html", "mdi-language-html5" },
        { ".js", "mdi-nodejs" },
        { ".json", "mdi-code-json" },
        { ".md", "mdi-language-markdown" },
        { ".pdf", "mdi-file-pdf" },
        { ".png", "mdi-file-image" },
        { ".txt", "mdi-file-document-outline" },
        { ".xls", "mdi-file-excel" }
    };

    public static List<FileTreeModel> GetFileTree(string path)
    {
        var tree = new List<FileTreeModel>();

        if (File.Exists(path))
        {
            return tree;
        }
        else if (Directory.Exists(path))
        {
            tree.AddRange(Directory.GetFiles(path).Select(x =>
            {
                var file = new FileInfo(x);
                var icon = Icons.FirstOrDefault(x => file.Name.EndsWith(x.Key)).Value ?? "mdi-file-code";
                
                return new FileTreeModel(file.Name, file.FullName, icon, true);
            }));

            tree.AddRange(Directory.GetDirectories(path).Select(x =>
            {
                var file = new DirectoryInfo(x);
                return new FileTreeModel(file.Name, file.FullName, false);
            }));
        }

        return tree;
    }
}