using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Generic
{
    public partial class DeleteSelectedDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public string BodyText { get; set; }
        [Parameter] public string SubmitText { get; set; } = "Delete";
        [Parameter] public bool AddTextConfirmation { get; set; } = false;

        private string DeleteConfirmationText = "";
    }
}
