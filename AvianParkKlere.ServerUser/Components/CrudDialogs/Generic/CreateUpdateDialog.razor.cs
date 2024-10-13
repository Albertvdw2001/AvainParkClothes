using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;

public partial class CreateUpdateDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public RenderFragment? FormTemplate { get; set; }
    [Parameter] public bool SubmitButtonEnabled { get; set; } = true;
    [Parameter] public string SubmitText { get; set; } = "Save";
    [Parameter] public EventCallback OnSubmit { get; set; }
    [Parameter] public bool TabTwoDisabled { get; set; } = false;


    private async Task Submit()
    {
        await OnSubmit.InvokeAsync();
    }
}