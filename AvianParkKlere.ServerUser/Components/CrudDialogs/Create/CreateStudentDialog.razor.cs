using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Create
{
    public partial class CreateStudentDialog
    {
        [Parameter] public EventCallback<StudentPostDto> OnSubmit { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private StudentPostDto Student = new();
        private bool Validated = false;


        private async Task Submit()
        {
            if (OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync(Student);
            }
        }


        private void ValidateForm() 
        {
            if (Student.Name is not null && Student.Name.Length > 1
                && Student.Surname is not null && Student.Surname.Length > 1
                && Student.Grade >= 0 && Student.Grade <= 12
                && Student.Age is not null && Student.Age > 0)
            {
                Validated = true;
            }
            else
            {
                Validated = false;
            }
        }

    }
}
