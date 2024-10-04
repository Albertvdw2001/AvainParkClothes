using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.Components.Dialogs
{
    public partial class CreateUpdateDialog
    {
        [Parameter] public EventCallback SubmitMethod { get; set; }
        [Parameter] public RenderFragment? FormTemplate { get; set; }
        [Parameter] public bool SubmitButtonEnabled { get; set; } = true;
        [Parameter] public string TitleText { get; set; } = "";
        [Parameter] public string SubmitText { get; set; } = "Save";
        [Parameter] public bool AddSecondTab { get; set; } = false;
        [Parameter] public RenderFragment? FormTemplate2 { get; set; }
        [Parameter] public string TabOneText { get; set; } = "Tab 1";
        [Parameter] public string TabTwoText { get; set; } = "Tab 2";
        [Parameter] public EventCallback OnClose { get; set; }

        private MudDialog Dialog;


        public async Task Show()
        {
            await Dialog.ShowAsync();
        }


        private async Task OnSubmit()
        {
            if (SubmitMethod.HasDelegate)
            {
                await SubmitMethod.InvokeAsync();
            }
        }


        public async Task Close()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
            await Dialog.CloseAsync();
        }
    }
}
