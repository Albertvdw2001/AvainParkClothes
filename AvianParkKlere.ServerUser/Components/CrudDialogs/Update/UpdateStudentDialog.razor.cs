using AvianParkKlere.Contracts.Dtos.Student;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Update
{
    public partial class UpdateStudentDialog
    {
        [Parameter, Required] public StudentPutDto Student { get; set; }
        [Parameter] public EventCallback<StudentPutDto> OnUpdate { get; set; }

        StudentPutDto StudentCopy;
        bool StudentChanged = false;  
        bool Validated = false; 

        protected override Task OnInitializedAsync()
        {
            StudentCopy = _mapper.Map<StudentPutDto>(Student);

            return Task.CompletedTask;
        }


        private void ValidateForm()
        {
            Validated = Student.Name is not null && Student.Name.Length > 1
                && Student.Surname is not null && Student.Surname.Length > 1
                && Student.Grade >= 0 && Student.Grade <= 12
                && Student.Age is not null && Student.Age > 0;
        }


        private void SetStudentChanged()
        {
            StudentChanged = Student.Name != StudentCopy.Name ||
                Student.Age != StudentCopy.Age ||
                Student.Surname != StudentCopy.Surname ||
                Student.Grade != StudentCopy.Grade;
        }


        private async Task UndoFieldEdit(string field)
        {
            ValidateForm();
            if (StudentChanged == true)
            {
                switch (field)
                {
                    case "Name":
                        Student.Name = StudentCopy.Name;
                        break;
                    case "Surname":
                        Student.Surname = StudentCopy.Surname;
                        break;
                    case "Age":
                        Student.Age = StudentCopy.Age;
                        break;
                    case "Grade":
                        Student.Grade = StudentCopy.Grade;
                        break;
                }
            }
            ValidateForm();
            StateHasChanged();
        }


        private async Task Update()
        {
            await OnUpdate.InvokeAsync(Student);
        }       
    }
}
