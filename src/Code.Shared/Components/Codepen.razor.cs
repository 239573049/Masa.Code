using BlazorComponent;

namespace Code.Shared;

public partial class Codepen
{
    StringNumber selectValue;

    private MonacoEdit MonacoEdit { get; set; }
        
    private string Code { get; set; }

    private async Task Render()
    {
        Code = await MonacoEdit.SMonacoEditor.GetValue();
    }
}