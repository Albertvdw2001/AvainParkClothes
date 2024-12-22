using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Create
{
    public partial class AssignClothesDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter, Required] public StudentGetDto Student { get; set; }
        [Parameter, Required] public List<StudentClothingGetDto> StudentClothingList { get; set; }
        [Parameter, Required] public List<ClothingGetDto> AllClothes { get; set; }
        [Parameter] public EventCallback<List<ClothesSelection>> OnAssign { get; set; }

        private List<ClothesSelection> SelectedClothes = new();


        protected override async Task OnInitializedAsync()
        {
            SelectedClothes = new();
            foreach (var clothing in AllClothes)
            {
                var studentClothing = StudentClothingList.FirstOrDefault(sc => sc.ClothingId == clothing.Id);
                if (studentClothing is null)
                {
                    SelectedClothes.Add(new ClothesSelection { StudentId = Student.Id, Clothing = clothing, Size = "", Selected = false });
                }
                else
                {
                    SelectedClothes.Add(new ClothesSelection { StudentId = Student.Id, Clothing = clothing, Size = studentClothing.Size, Selected = true });
                }
            }
        }


        private async Task Assign()
        {
            if (SelectedClothes.Count != 0 && OnAssign.HasDelegate)
            {
                await OnAssign.InvokeAsync(SelectedClothes);
            }
        }
    }
}
