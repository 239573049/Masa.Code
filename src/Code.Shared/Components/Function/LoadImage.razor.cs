using Microsoft.AspNetCore.Components;

namespace Code.Shared;

public partial class LoadImage : IAsyncDisposable
{
    [Parameter] public string Path { get; set; }

    private string Img;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var bytes = await File.ReadAllBytesAsync(Path);
            Img = await HelperJsInterop.ByteToUrl(bytes);
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async ValueTask DisposeAsync()
    {
        await HelperJsInterop.RevokeUrl(Img);
    }
}