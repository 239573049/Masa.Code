using Microsoft.AspNetCore.Components;

namespace Code.Shared;

public class CodeComponentBase : ComponentBase
{
    [CascadingParameter(Name = nameof(LayoutOptions))]
    public LayoutOptions LayoutOptions { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string Style { get; set; }
}