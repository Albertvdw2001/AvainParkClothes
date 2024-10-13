using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Read;

public partial class ReadTenantDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; }

    [Parameter]
   // public UpdateTenantDto TenantDto { get; set; } = new();

  //  [Parameter]
    public int TenantId { get; set; }

    private bool FileTooLarge = false;

    private bool EmptyEmail = false;
    private bool EmptyFirstName = false;
    private bool EmptyLastName = false;
    private bool TenantUnselected = false;
    private bool GenericError = false;


    /*protected override async Task OnInitializedAsync()
    {
        Tenants = await _tenantApiService.GetAllAsync();
        SelectedTenant = Tenants.FirstOrDefault(t => t.Id == UserDto.TenantId);
    }
*/

    private async Task Submit()
    {
        bool areThereErrors = false; //await AreThereErrors();

        if (areThereErrors)
        {
            return;
        }
        else
        {
            /*UserDto.TenantId = SelectedTenant.Id;
            bool response = await _authApiService.UpdateUserAsync(UserId, UserDto);

            if (response)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                GenericError = true;
                StateHasChanged();
                await Task.Delay(3000);
                GenericError = false;
                StateHasChanged();
            }*/
        }
    }


    /* private async Task<bool> AreThereErrors()
     {
         snackbar.Clear();

         if (string.IsNullOrWhiteSpace(UserDto.Email))
         {
             EmptyEmail = true;
             StateHasChanged();
             await Task.Delay(3000);
             EmptyEmail = false;
             StateHasChanged();
             return true;
         }
         else if (string.IsNullOrWhiteSpace(UserDto.FirstName))
         {
             EmptyFirstName = true;
             StateHasChanged();
             await Task.Delay(3000);
             EmptyFirstName = false;
             StateHasChanged();
             return true;
         }
         else if (string.IsNullOrWhiteSpace(UserDto.LastName))
         {
             EmptyLastName = true;
             StateHasChanged();
             await Task.Delay(3000);
             EmptyLastName = false;
             StateHasChanged();
             return true;
         }
         else if (SelectedTenant is null)
         {
             TenantUnselected = true;
             StateHasChanged();
             await Task.Delay(3000);
             TenantUnselected = false;
             StateHasChanged();
             return true;
         }

         return false;
     }


     private async Task UploadFile(IBrowserFile file)
     {
         long fileSize = file.Size;

         long maxSizeInBytes = 500 * 1024; // 500 KB

         if (fileSize > maxSizeInBytes)
         {
             FileTooLarge = true;
         }
         else
         {
             using (var memoryStream = new MemoryStream())
             {
                 await using (var stream = file.OpenReadStream())
                 {
                     await stream.CopyToAsync(memoryStream);
                 }

                 byte[] fileBytes = memoryStream.ToArray();
                 UserDto.Avatar = fileBytes;
             }
         }
         StateHasChanged();
     }


     private void RemoveAvatar()
     {
         UserDto.Avatar = null;
         StateHasChanged();
     }*/
}
