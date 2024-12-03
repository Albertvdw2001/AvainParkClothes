using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Read
{
    public partial class ViewStudentDialog
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public StudentGetDto Student { get; set; }  

        List<ClothingStudentClothingComposite> ClothesList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetClothesList();
        }


        private async Task GetClothesList()
        {
            ClothesList = new();
            var studentClothing = await _apiService.GetClothingForStudent(Student.Id);

            foreach (var item in studentClothing)
            {
                var clothing = await _apiService.GetClothing(item.ClothingId);
                if (clothing == null)
                {
                    //show error snackbar
                    return;
                }

                var composite = new ClothingStudentClothingComposite
                {
                    Name = clothing.Name,
                    Size = item.Size,
                    Price = clothing.Price
                };
                ClothesList.Add(composite);
            }
        }


        private void DownloadPdf()
        {

        }

    }
}
