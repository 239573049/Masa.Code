using System.ComponentModel.DataAnnotations;
using Code.Shared.Model;
using Code.Shared.Utility;
using Masa.Blazor;

namespace Code.Shared;

public partial class EditorSetting
{
    private bool _valid = true;
    private MForm _form;
    private CodeSettingModel _model = new();

    protected override async Task OnInitializedAsync()
    {
        _model = await ConfigUtility.GetCodeSettingAsync();
        await base.OnInitializedAsync();
    }

    private async void HandleOnValidSubmit()
    {
        await ConfigUtility.SaveCodeSettingAsync(_model);
    }

    private void HandleOnInvalidSubmit()
    {
        //invalid
    }
}