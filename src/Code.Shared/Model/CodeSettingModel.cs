using System.ComponentModel.DataAnnotations;

namespace Code.Shared.Model;

public class CodeSettingModel
{
    /// <summary>
    /// 默认加载目录
    /// </summary>
    [Required]
    [MaxLength(ErrorMessage = "请输入默认加载目录")]
    public string? Path { get; set; }

    public bool Dark { get; set; }
}