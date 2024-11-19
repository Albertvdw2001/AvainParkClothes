using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Read
{
    public partial class ViewStudentDialog
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public StudentGetDto Student { get; set; }  

        List<ClothingGetDto> ClothesList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetClothesList();
        }   


        private async Task GetClothesList()
        {
            ClothesList = new();
            var studentClothing = await _apiService.GetClothingForStudent(Student.Id);   

            foreach(var item in studentClothing)
            {
                var clothing = await _apiService.GetClothing(item.ClothingId);
                ClothesList.Add(clothing);
            }
        }


        private void DownloadPdf()
        {

        }
    }
}
