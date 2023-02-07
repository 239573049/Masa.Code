using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Code.Shared;

public partial class RegardingOur
{
    #region Inject

    [Inject] public IJSRuntime JsRuntime { get; set; }

    #endregion

    private async Task GoHref(string href)
    {
        await JsRuntime.InvokeVoidAsync("window.open",href,"_blank");
    }
}