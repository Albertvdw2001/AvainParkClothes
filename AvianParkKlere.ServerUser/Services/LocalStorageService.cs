using Blazored.LocalStorage;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Services
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task ClearStorage()
        {
            await _localStorageService.ClearAsync();
        }

        public async Task SetStudentDataGridHasInitialized(bool hasInitialized)
        {
            await _localStorageService.SetItemAsync("StudentDataGridHasInitialized", hasInitialized);
        }

        public async Task<bool> GetStudentDataGridHasInitialized()
        {
            bool? result =  await _localStorageService.GetItemAsync<bool>("StudentDataGridHasInitialized");
            if (result is null)
            {
                await SetStudentDataGridHasInitialized(false);
                return false;
            }
            return (bool)result;
        }

        public async Task SetClothingDataGridHasInitialized(bool hasInitialized)
        {
            await _localStorageService.SetItemAsync("ClothingDataGridHasInitialized", hasInitialized);
        }

        public async Task<bool> GetClothingDataGridHasInitialized()
        {
            bool? result = await _localStorageService.GetItemAsync<bool>("ClothingDataGridHasInitialized");
            if (result is null)
            {
                await SetClothingDataGridHasInitialized(false);
                return false;
            }
            return (bool)result;
        }

    }
}
