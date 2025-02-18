using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Constants;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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


        private async Task DownloadPdf(List<StudentStudentClothingComposite> itemsToExport, string filename)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            using var memoryStream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                    .PaddingVertical(0, Unit.Centimetre)
                    .Table(table =>
                    {
                        // Define table columns
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Name
                            columns.RelativeColumn(); // Surname
                            columns.RelativeColumn(); // Grade
                            columns.RelativeColumn(); // Age
                            columns.RelativeColumn(); // Size
                        });

                        // Add table headers
                        table.Cell().LabelCell("Name");
                        table.Cell().LabelCell("Surname");
                        table.Cell().LabelCell("Grade");
                        table.Cell().LabelCell("Age");
                        table.Cell().LabelCell("Size");

                        // Add data rows
                        foreach (var item in itemsToExport)
                        {
                            table.Cell().ValueCell(item.Name);
                            table.Cell().ValueCell(item.Surname);
                            table.Cell().ValueCell($"Gr. {item.Grade}");
                            table.Cell().ValueCell(item.Age.ToString());
                            table.Cell().ValueCell($"{item.Size} ({item.SizeMeasurement})");
                        }
                    });
                });
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);
            using var streamRef = new DotNetStreamReference(memoryStream);
            await _js.InvokeVoidAsync("exportPDF", filename + ".pdf", streamRef);
        }


        private async Task DownloadGroupByGradeClothingAndSize(List<CombinedDto> itemsToExport)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            using var memoryStream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().PaddingVertical(0, Unit.Centimetre).Column(column =>
                    {
                        // Group items by grade
                        var groupedByGrade = itemsToExport
                            .GroupBy(item => item.Grade)
                            .OrderBy(group => group.Key);

                        foreach (var gradeGroup in groupedByGrade)
                        {
                            // Add grade subheading
                            var gradeText = gradeGroup.Key.HasValue ? $"Grade: {gradeGroup.Key}" : "Grade: Unspecified";
                            column.Item().Text(gradeText).FontSize(16).Bold();

                            // Group items by clothing name within the grade
                            var groupedByClothing = gradeGroup
                                .GroupBy(item => item.ClothingName)
                                .OrderBy(group => group.Key);

                            foreach (var clothingGroup in groupedByClothing)
                            {
                                // Add clothing subheading
                                column.Item().Text($"Clothing: {clothingGroup.Key}").FontSize(14).Bold();

                                // Group items by size within the clothing group
                                var groupedBySize = clothingGroup
                                    .GroupBy(item => item.Size)
                                    .OrderBy(group => group.Key);

                                // Add table for size counts
                                column.Item().Table(table =>
                                {
                                    // Define table columns
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(); // Size
                                        columns.RelativeColumn(); // Count
                                    });

                                    // Add table headers
                                    table.Cell().LabelCell("Size");
                                    table.Cell().LabelCell("Count");

                                    // Add data rows for the size counts
                                    foreach (var sizeGroup in groupedBySize)
                                    {
                                        table.Cell().ValueCell(sizeGroup.Key ?? "Unspecified");
                                        table.Cell().ValueCell(sizeGroup.Count().ToString());
                                    }
                                });

                                // Add spacing after each clothing group
                                column.Item().PaddingVertical(5);
                            }

                            // Add spacing after each grade group
                            column.Item().PaddingVertical(10);
                        }
                    });
                });
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);
            using var streamRef = new DotNetStreamReference(memoryStream);
            await _js.InvokeVoidAsync("exportPDF", "GradeClothingSizeCounts.pdf", streamRef);
        }



    }
}
