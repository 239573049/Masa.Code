using System.Text.Json;
using Code.Shared.Model;

namespace Code.Shared.Utility;

public class ConfigUtility
{
    private static string ConfigPath = Path.Combine(AppContext.BaseDirectory, "CodeSetting.json");

    private static CodeSettingModel? codeSettingModel;

    public static async Task<CodeSettingModel?> GetCodeSettingAsync()
    {
        return codeSettingModel ??= JsonSerializer.Deserialize<CodeSettingModel>(await File.ReadAllTextAsync(ConfigPath));
    }

    /// <summary>
    /// 保存配置
    /// </summary>
    /// <param name="model"></param>
    public static async Task SaveCodeSettingAsync(CodeSettingModel model)
    {
        await using var stream = File.Create(ConfigPath);
        stream.Write(JsonSerializer.SerializeToUtf8Bytes(model));
        await stream.FlushAsync();
        stream.Close();
        codeSettingModel = model;
    }
}