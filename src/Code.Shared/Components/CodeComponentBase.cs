using Code.Shared.Options;
using Microsoft.AspNetCore.Components;

namespace Code.Shared;

public class CodeComponentBase : ComponentBase
{
    [CascadingParameter(Name = nameof(LayoutOptions))]
    public LayoutOptions LayoutOptions { get; set; }
    
    
}