/*using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ScanAndGo.Contracts.Models.Dtos.Tenant;
using ScanAndGo.AdminPortal.Localization.Models;
using ScanAndGo.Contracts.Constants;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Update;

public partial class UpdateTenantDialog
{
    *//* Parameters *//*

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public UpdateTenantDto TenantDto { get; set; } = new();
    [Parameter] public EventCallback<UpdateTenantDto> OnSubmit { get; set; }

    *//* Helper Variables *//*

    private List<Culture> Cultures = new();
    private Culture? SelectedCulture = null;
    private List<Currency> Currencies = new();
    private Currency? SelectedCurrency = null;

    *//* Error Handling *//*

    private bool FileTooLarge = false;
    private bool EmptyName = false;
    private bool EmptyEmail = false;
    private bool EmptyLogo = false;
    private bool EmptyAddress = false;
    private bool EmptyLanguageCode = false;
    private bool EmptyPhone = false;
    private bool EmptyPrimaryColor = false;
    private bool EmptySecondaryColor = false;
    private bool EmptyCurrency = false;
    private bool GenericError = false;


    // Infer the CurrencySymbol field from the CurrencyCode field

    protected override async Task OnInitializedAsync()
    {
        Cultures = ls.GetAvailableCultures();
        SelectedCulture = Cultures.FirstOrDefault(culture => culture.Code == TenantDto.DefaultLanguageCode);
        Currencies = Currency.GetCurrencies();
        SelectedCurrency = Currencies.FirstOrDefault(currency => currency.Symbol == TenantDto.CurrencySymbol); // Name is the unique currency property
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
            TenantDto.LastUpdaterId = 1; // Change this!!
            TenantDto.DefaultLanguageCode = SelectedCulture.Code;
            TenantDto.CurrencySymbol = SelectedCurrency.Symbol;

            await OnSubmit.InvokeAsync(TenantDto);
        }
    }


    private async Task<bool> AreThereErrors()
    {
        snackbar.Clear();

        if (string.IsNullOrWhiteSpace(TenantDto.Name))
        {
            EmptyName = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyName = false;
            StateHasChanged();
            return true;
        }
        else if (string.IsNullOrWhiteSpace(TenantDto.Email))
        {
            EmptyEmail = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyEmail = false;
            StateHasChanged();
            return true;
        }
        else if (TenantDto.Logo is null)
        {
            EmptyLogo = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyLogo = false;
            StateHasChanged();
            return true;
        }
        else if (string.IsNullOrWhiteSpace(TenantDto.Address))
        {
            EmptyAddress = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyAddress = false;
            StateHasChanged();
            return true;
        }
        else if (SelectedCulture is null)
        {
            EmptyLanguageCode = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyLanguageCode = false;
            StateHasChanged();
            return true;
        }
        else if (SelectedCurrency is null)
        {
            EmptyCurrency = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyCurrency = false;
            StateHasChanged();
            return true;
        }
        else if (string.IsNullOrWhiteSpace(TenantDto.Phone))
        {
            EmptyPhone = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyPhone = false;
            StateHasChanged();
            return true;
        }
        else if (string.IsNullOrWhiteSpace(TenantDto.PrimaryColor))
        {
            EmptyPrimaryColor = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptyPrimaryColor = false;
            StateHasChanged();
            return true;
        }
        else if (string.IsNullOrWhiteSpace(TenantDto.SecondaryColor))
        {
            EmptySecondaryColor = true;
            StateHasChanged();
            await Task.Delay(3000);
            EmptySecondaryColor = false;
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
                TenantDto.Logo = fileBytes;
            }
        }
        StateHasChanged();
    }


    private void RemoveLogo()
    {
        TenantDto.Logo = null;
        StateHasChanged();
    }
}*/