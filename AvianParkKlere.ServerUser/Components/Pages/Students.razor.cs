using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Create;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Read;
using AvianParkKlere.ServerUser.Components.Shared;
using AvianParkKlere.ServerUser.Models;
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
            StateHasChanged();
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


        private async Task OpenAssignClothesDialog(StudentGetDto student)
        {
            var studentClothingList = await _apiService.GetClothingForStudent(student.Id);
            var allClothes = await _apiService.GetClothing();

            var parameters = new DialogParameters<AssignClothesDialog>
            {
                { x => x.Student, student},
                { x => x.StudentClothingList, studentClothingList},
                { x => x.AllClothes, allClothes},
                { x => x.OnAssign, EventCallback.Factory.Create<List<ClothesSelection>>(this, HandleAssignClothes)}
            };

            CruDialog = await _dialogService.ShowAsync<AssignClothesDialog>(
                "Assign Clothes",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );
        }


        private async Task HandleAssignClothes(List<ClothesSelection> clothesSelections)
        {
            foreach (var selection in clothesSelections)
            {
                var apiResponse = await _apiService.AddOrDeleteAssignedClothes(selection.StudentId, selection);
                if (apiResponse == false)
                {
                    ShowErrorSnackbar("Student assignment failed. Please contact Albert or try again.");
                    return;
                }
            }

            ShowSuccessSnackbar("Students assigned successfully");
            if (CruDialog is not null)
            {
                await GetStudentList();
                CruDialog.Close();
            }
        }


        private async Task OpenDeleteDialog(StudentGetDto student)
        {
            var parameters = new DialogParameters<DeleteDialog>
            {
                { x => x.BodyText, "Are you sure you want to remove this student from the database?"}
            };

            CruDialog = await _dialogService.ShowAsync<DeleteDialog>(
                "Delete Student",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );

            var result = await CruDialog.Result;
            if (!result.Canceled)
            {
                await HandleDeleteStudent(student.Id);
            }
        }


        private async Task HandleDeleteStudent(int id)
        {
            var apiResposnse = await _apiService.DeleteStudent(id);

            if (apiResposnse == false)
            {
                ShowErrorSnackbar("Failed to delete student");
                return;
            }
            ShowSuccessSnackbar("Student deleted successfully");
            await GetStudentList();
            StateHasChanged();
        }

        private async Task HandleDeleteStudents(List<int> ids)
        {
            foreach (int id in ids)
            {
                var apiResposnse = await _apiService.DeleteStudent(id);

                if (apiResposnse == false)
                {
                    ShowErrorSnackbar("Failed to delete students");
                    await GetStudentList();
                    return;
                }
            }
            ShowSuccessSnackbar("Students deleted successfully");
            await GetStudentList();
            StateHasChanged();
        }


        private async Task OpenViewDialog(StudentGetDto student)
        {
            var parameters = new DialogParameters<ViewStudentDialog>
            {
                { x => x.Student, student}
            };

            CruDialog = await _dialogService.ShowAsync<ViewStudentDialog>(
                "View Student Clothes",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );  

        }


        private async Task OpenDeleteSelectedDialog(IEnumerable<StudentGetDto> students)
        {
            var parameters = new DialogParameters<DeleteDialog>
            {
                { x => x.BodyText, "Are you sure you want to remove selected students from the database?"}
            };

            CruDialog = await _dialogService.ShowAsync<DeleteDialog>(
                "Delete Students",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );

            var result = await CruDialog.Result;
            if (!result.Canceled)
            {
                List<int> studentIds = students.Select(x => x.Id).ToList();
                await HandleDeleteStudents(studentIds); 
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
