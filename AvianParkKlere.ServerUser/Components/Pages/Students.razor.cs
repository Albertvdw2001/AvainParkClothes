using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Create;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;
using AvianParkKlere.ServerUser.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.Pages
{
    public partial class Students
    {
        GenericDataGrid<StudentGetDto, StudentPostDto, StudentPutDto> Table;
        List<StudentGetDto> StudentList = new();

        // Create dialog
        IDialogReference CruDialog;
        CreateUpdateDialog CreateDialog;
        StudentPostDto CreateDialogStudent;
        private bool Validated = false;


        protected override async Task OnInitializedAsync()
        {
            await GetStudentList();
        }


        private async Task GetStudentList()
        {
            StudentList = await _apiService.GetStudents();

            if (StudentList == null)
            {
                StudentList = new();
                ShowErrorSnackbar("Failed to get student list");
            }

            StateHasChanged();
        }


        /* Dialogs */

        private async Task OpenCreateDialog()
        {
            var parameters = new DialogParameters<CreateStudentDialog>
            {
                { x => x.OnSubmit, EventCallback.Factory.Create<StudentPostDto>(this, HandleCreateStudent)}
            };

            CruDialog = await _dialogService.ShowAsync<CreateStudentDialog>(
                "New Student",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );
        }


        private async Task HandleCreateStudent(StudentPostDto studentPostDto)
        {
            var response = await _apiService.CreateStudent(studentPostDto);

            if (response == false)
            {
                ShowErrorSnackbar("Failed to create student");
                return;
            }

            ShowSuccessSnackbar("Student created successfully");
            if (CruDialog is not null)
            {
                await GetStudentList();
                CruDialog.Close();
            }
            
        }


        private void ValidateForm()
        {
            if (CreateDialogStudent.Name is not null && CreateDialogStudent.Name.Length > 1
                && CreateDialogStudent.Surname is not null && CreateDialogStudent.Surname.Length > 1
                && CreateDialogStudent.Grade >= 0 && CreateDialogStudent.Grade <= 12
                && CreateDialogStudent.Age is not null && CreateDialogStudent.Age > 0)
            {
                Validated = true;
            }
            else
            {
                Validated = false;
            }
        }

        /*  */


        /* Snackbar Methods */

        private void ShowErrorSnackbar(string message)
        {
            _snackbar.Clear();
            _snackbar.Add(message, Severity.Error, config =>
            {
                config.ShowCloseIcon = true;
            });
        }


        private void ShowSuccessSnackbar(string message)
        {
            _snackbar.Clear();
            _snackbar.Add(message, Severity.Success, config =>
            {
                config.ShowCloseIcon = true;
            });
        }


    }
}
