/*using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ScanAndGo.Contracts.Models.Dtos.Auth;
using ScanAndGo.Contracts.Models.Dtos.Tenant;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Update;

public partial class UpdateUserDialog
{
    *//* Parameters *//*

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public UpdateAdminDto UserDto { get; set; } = new();
    [Parameter] public EventCallback<UpdateAdminDto> OnSubmit { get; set; }
    [Parameter] public bool IsTenantSelectable { get; set; } = true;

    *//* Helper Variables *//*

    private List<GetTenantDto>? Tenants = new();
    private GetTenantDto? SelectedTenant { get; set; }

    private bool showPassword = false;

    *//* Error Handling *//*

    private bool FileTooLarge = false;
    private bool EmptyEmail = false;
    private bool EmptyFirstName = false;
    private bool EmptyLastName = false;
    private bool TenantUnselected = false;
    private bool GenericError = false;


    protected override async Task OnInitializedAsync()
    {
        Tenants = await _tenantApiService.GetAllAsync();
        SelectedTenant = Tenants.FirstOrDefault(t => t.Id == UserDto.TenantId);
    }


    private async Task Submit()
    {
        bool areThereErrors = await AreThereErrors();

        if (areThereErrors)
        {
            return;
        }
        else
        {
            if (SelectedTenant is not null && IsTenantSelectable)
            {
                UserDto.TenantId = SelectedTenant.Id;
            }

            await OnSubmit.InvokeAsync(UserDto);
        }
    }


    private async Task<bool> AreThereErrors()
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
        else if (SelectedTenant is null && IsTenantSelectable)
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
    }
}*/