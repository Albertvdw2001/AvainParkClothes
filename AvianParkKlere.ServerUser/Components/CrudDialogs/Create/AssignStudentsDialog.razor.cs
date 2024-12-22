using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Create
{
    public partial class AssignStudentsDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter, Required] public ClothingGetDto Clothing { get; set; }  
        [Parameter, Required] public List<StudentClothingGetDto> StudentClothingList { get; set; }
        [Parameter, Required] public List<StudentGetDto> AllStudents { get; set; }
        [Parameter] public EventCallback<List<StudentSelection>> OnAssign { get; set; }

        private List<StudentSelection> SelectedStudents = new();


        protected override async Task OnInitializedAsync()
        {
            SelectedStudents = new();
            foreach (var student in AllStudents)
            {
                var studentClothing = StudentClothingList.FirstOrDefault(sc => sc.StudentId == student.Id);
                if (studentClothing is null)
                {
                    SelectedStudents.Add(new StudentSelection { ClothingId = Clothing.Id, Student = student, Size = "", Selected = false });
                }
                else
                {
                    SelectedStudents.Add(new StudentSelection { ClothingId = Clothing.Id, Student = student, Size = studentClothing.Size, Selected = true });
                }
            }
        }


        private async Task Assign()
        {
            if (SelectedStudents.Count != 0 && OnAssign.HasDelegate)
            {
                await OnAssign.InvokeAsync(SelectedStudents);
            }
        }
    }
}
