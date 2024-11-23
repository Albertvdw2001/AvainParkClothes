using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Create;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Generic;
using AvianParkKlere.ServerUser.Components.CrudDialogs.Read;
using AvianParkKlere.ServerUser.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvianParkKlere.ServerUser.Components.Pages
{
    public partial class Clothes
    {
        GenericDataGrid<ClothingGetDto, ClothingPostDto, ClothingPutDto> Table;
        List<ClothingGetDto> ClothingList = new();

        // Create dialog
        IDialogReference CruDialog;
        CreateUpdateDialog CreateDialog;
        StudentPostDto CreateDialogStudent;
        private bool Validated = false;


        protected override async Task OnInitializedAsync()
        {
            await GetClothesList();
        }


        private async Task GetClothesList()
        {
            ClothingList = await _apiService.GetClothing();
            
            if (ClothingList == null)
            {
                ClothingList = new();
                ShowErrorSnackbar("Failed to get clothes list");
            }

            StateHasChanged();
        }


        /* Dialogs */

        private async Task OpenCreateDialog()
        {
            var parameters = new DialogParameters<CreateClothingDialog>
            {
                { x => x.OnSubmit, EventCallback.Factory.Create<ClothingPostDto>(this, HandleCreateClothing)}
            };

            CruDialog = await _dialogService.ShowAsync<CreateClothingDialog>(
                "New Clothing",
                parameters,
                new DialogOptions { CloseButton = true, BackdropClick = false, Position = DialogPosition.TopCenter }
            );
        }


        private async Task HandleCreateClothing(ClothingPostDto clothingPostDto)
        {
            var response = await _apiService.CreateClothing(clothingPostDto);

            if (response == false)
            {
                ShowErrorSnackbar("Failed to add clothing");
                return;
            }

            ShowSuccessSnackbar("Clothing created successfully");
            if (CruDialog is not null)
            {
                await GetClothesList();
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
            await GetClothesList();
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
