/*using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ScanAndGo.AdminPortal.Localization.Models;
using ScanAndGo.Contracts.Constants;
using ScanAndGo.Contracts.Models.Dtos.Branch;
using ScanAndGo.Contracts.Models.Dtos.Tenant;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Update;

public partial class UpdateBranchDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    [Parameter, Required] public UpdateBranchDto BranchDto { get; set; }
    [Parameter] public EventCallback<UpdateBranchDto> OnUpdate { get; set; }

    [Parameter] public bool IsTenantSelectable { get; set; } = true;

    private List<Culture> Cultures = new();
    private Culture? SelectedCulture = null;
    private List<Currency> Currencies = new();
    private Currency? SelectedCurrency = null;

    private List<GetTenantDto>? Tenants = new();
    private GetTenantDto? SelectedTenant { get; set; }


    private bool TenantUnselected = false;

    private bool FileTooLarge = false;

    private bool EmptyName = false;
    private bool EmptyEmail = false;
    private bool EmptyQRCode = false;
    private bool EmptyAddress = false;
    private bool EmptyPhone = false;
    private bool GenericError = false;


    protected override async Task OnInitializedAsync()
    {
        Cultures = ls.GetAvailableCultures();
        SelectedCulture = Cultures.FirstOrDefault(culture => culture.Code == BranchDto.DefaultLanguageCode);

        Currencies = Currency.GetCurrencies();
        SelectedCurrency = Currencies.FirstOrDefault(currency => currency.Symbol == BranchDto.CurrencySymbol); // Name is the unique currency property

        Tenants = await _tenantApiService.GetAllAsync();
        SelectedTenant = Tenants.FirstOrDefault(t => t.Id == BranchDto.TenantId);
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
                BranchDto.TenantId = SelectedTenant.Id;
            }
            BranchDto.DefaultLanguageCode = SelectedCulture?.Code;
            BranchDto.CurrencySymbol = SelectedCurrency?.Symbol;
            BranchDto.LastUpdaterId = 1; // Get the current user -> use their ID!
            await OnUpdate.InvokeAsync(BranchDto);
        }
    }


    private async Task<bool> AreThereErrors()
    {
        snackbar.Clear();

        if (BranchDto.QRCode is null || BranchDto.QRCode.Length == 0)
        {
            EmptyQRCode = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyQRCode = false;
        }
        else if (string.IsNullOrWhiteSpace(BranchDto.Name))
        {
            EmptyName = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyName = false;
        }
        else if (string.IsNullOrWhiteSpace(BranchDto.Email))
        {
            EmptyEmail = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyEmail = false;
        }
        else if (SelectedTenant is null && IsTenantSelectable)
        {
            TenantUnselected = true;
            StateHasChanged();
            await Task.Delay(3000);
            TenantUnselected = false;
        }
        else if (string.IsNullOrWhiteSpace(BranchDto.Phone))
        {
            EmptyPhone = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyPhone = false;
        }

        else if (string.IsNullOrWhiteSpace(BranchDto.Address))
        {
            EmptyAddress = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyAddress = false;
        }
        else
        {
            return false;
        }
        StateHasChanged();
        return true;
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
                BranchDto.QRCode = fileBytes;
            }
        }
        StateHasChanged();
    }


    private void RemoveQRCode()
    {
        BranchDto.QRCode = null;
        StateHasChanged();
    }
}*/