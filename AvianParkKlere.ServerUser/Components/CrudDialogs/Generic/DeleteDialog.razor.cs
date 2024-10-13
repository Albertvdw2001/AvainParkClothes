using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;

public partial class DeleteDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter, Required] public string BodyText { get; set; }
    [Parameter] public string SubmitText { get; set; } = "Delete";
    [Parameter] public bool AddTextConfirmation { get; set; } = false;

    private string DeleteConfirmationText = "";


    private async Task Submit()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }
}
