using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.Dialogs
{
    public partial class DeleteDialog
    {
        [Parameter] public EventCallback DeleteMethod { get; set; }
        [Parameter] public string TitleText { get; set; } = "Delete";
        [Parameter] public string SubmitText { get; set; } = "Delete";
        [Parameter] public string BodyText { get; set; }
        [Parameter] public bool AddTextConfirmation { get; set; } = false;
        [Parameter] public EventCallback OnClose { get; set; }

        private MudDialog Dialog;

        private string DeleteConfirmationText;


        public async Task Show()
        {
            await Dialog.ShowAsync();
        }

        private async Task OnDelete()

        {
            await DeleteMethod.InvokeAsync();
            await Close();
        }


        public async Task Close()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
            DeleteConfirmationText = "";
            await Dialog.CloseAsync();
        }

    }
}
