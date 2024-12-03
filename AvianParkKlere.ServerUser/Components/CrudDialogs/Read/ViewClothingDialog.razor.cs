using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Read
{
    public partial class ViewClothingDialog
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }

        [Parameter] public ClothingGetDto Clothing { get; set; }   
        List<StudentStudentClothingComposite> StudentList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetStudentList();
        }


        private async Task GetStudentList()
        {
            StudentList = new();
            var clothingStudents = await _apiService.GetStudentForClothing(Clothing.Id);

            StudentList = new();
            foreach (var item in clothingStudents)
            {
                var student = await _apiService.GetStudent(item.StudentId);
                var clothing = await _apiService.GetClothing(item.ClothingId);  

                if (student == null || clothing == null)
                {
                    //show error snackbar
                    return;
                }   

                var composite = new StudentStudentClothingComposite
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    Age = (int)student.Age,
                    Grade = (int)student.Grade,
                    Size = item.Size,
                    SizeMeasurement = clothing.SizeMeasurement  
                };
                StudentList.Add(composite);
            }
        }


        private void DownloadPdf()
        {

        }
    }
}
