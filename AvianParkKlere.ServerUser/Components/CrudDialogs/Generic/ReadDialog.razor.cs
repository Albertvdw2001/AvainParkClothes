/*using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ScanAndGo.AdminPortal.Shared.CrudDialogs.Generic;

public partial class ReadDialog
{
    [CascadingParameter] private MudDialogInstance Dialog { get; set; }
    [Parameter] public string TitleText { get; set; } = "Read";
    [Parameter] public string BodyText { get; set; }
    [Parameter] public string SubmitText { get; set; } = "Close";
    [Parameter] public RenderFragment? FormTemplate { get; set; }
    [Parameter] public bool AddSecondTab { get; set; } = false;
    [Parameter] public RenderFragment? FormTemplate2 { get; set; }
    [Parameter] public string TabOneText { get; set; } = "Tab 1";
    [Parameter] public string TabTwoText { get; set; } = "Tab 2";

    DialogOptions options = new() { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter };

    public async Task Show()
    {
        await Dialog.ShowAsync();
    }


    public async Task Close()
    {
        await Dialog.CloseAsync();
    }
}*/