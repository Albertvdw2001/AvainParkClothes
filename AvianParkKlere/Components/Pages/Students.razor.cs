using AvianParkKlere.Components.Dialogs;
using AvianParkKlere.Contracts.Dtos.Student;
using MudBlazor;

namespace AvianParkKlere.Components.Pages
{
    public partial class Students
    {
        GenericDataGrid<StudentGetDto, StudentPostDto, StudentPutDto> Table;
        List<StudentGetDto> StudentList = new();

        CreateUpdateDialog CreateDialog;
        StudentPostDto CreateDialogStudent;


        protected override async Task OnInitializedAsync()
        {
            await GetStudentList(); 
        }


        /* Dialogs */

        private async Task OpenCreateDialog()
        {
            await CreateDialog.Show();
        }   


        private async Task GetStudentList()
        {
            StudentList = await apiService.GetStudents();

            if (StudentList == null)
            {
                StudentList = new();
                ShowErrorSnackbar("Failed to get student list");    
            }
            
            StateHasChanged();
        }


        private async Task HandleCreateStudent()
        {
            var response = await apiService.CreateStudent(CreateDialogStudent);

            if (response == false)
            {
                ShowErrorSnackbar("Failed to create student");
                return;
            }

            ShowSuccessSnackbar("Student created successfully");
            await GetStudentList();
            await CreateDialog.Close();
        }


        /* Snackbar Methods */

        private void ShowErrorSnackbar(string message)
        {
            snackbar.Clear();
            snackbar.Add(message, Severity.Error, config =>
            {
                config.ShowCloseIcon = true;
            });
        }


        private void ShowSuccessSnackbar(string message)
        {
            snackbar.Clear();
            snackbar.Add(message, Severity.Success, config =>
            {
                config.ShowCloseIcon = true;
            });
        }


    }
}
