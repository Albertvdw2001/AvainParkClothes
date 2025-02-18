using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Constants;
using AvianParkKlere.ServerUser.Models;
using Microsoft.JSInterop;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AvianParkKlere.ServerUser.Components.Pages
{
    public partial class DownloadAll
    {
        private List<StudentGetDto>? Students = new();
        private List<ClothingGetDto>? Clothes = new();
        private List<StudentClothingGetDto>? StudentClothes = new(); 
        private List<CombinedDto>? Combined = new();    

        protected override async Task OnInitializedAsync()
        {
            await GetData();
            StateHasChanged();
        }


        private async Task GetData()
        {
            Combined = new();

            Students = await _apiService.GetStudents();
            Clothes = await _apiService.GetClothing();
            StudentClothes = await _apiService.GetStudentClothingAsync();   

            if (Students != null && Clothes != null && StudentClothes != null)
            {
                foreach (var student in Students)
                {
                    var studentClothing = StudentClothes.Where(sc => sc.StudentId == student.Id).ToList();

                    foreach (var sc in studentClothing)
                    {
                        var clothing = Clothes.FirstOrDefault(c => c.Id == sc.ClothingId);

                        if (clothing != null)
                        {
                            Combined.Add(new CombinedDto
                            {
                                StudentId = student.Id,
                                ClothingId = clothing.Id,
                                StudentName = student.Name,
                                StudentSurname = student.Surname,
                                Age = student.Age,
                                Grade = student.Grade,
                                ClothingName = clothing.Name,
                                Price = clothing.Price,
                                Size = sc.Size,
                                SizeMeasurement = clothing.SizeMeasurement,
                            });
                        }
                    }
                }
            }   
        }

        private async Task DownloadGroupByGrade(List<CombinedDto> itemsToExport)
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
                        var groupedItems = itemsToExport
                            .GroupBy(item => item.Grade)
                            .OrderBy(group => group.Key);

                        foreach (var group in groupedItems)
                        {
                            // Add grade header
                            column.Item().Text($"Grade: {group.Key?.ToString() ?? "N/A"}").FontSize(14).Bold();

                            // Add table for the group
                            column.Item().Table(table =>
                            {
                                // Define table columns
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Name
                                    columns.RelativeColumn(); // Surname
                                    columns.RelativeColumn(); // Age
                                    columns.RelativeColumn(); // Clothing Name
                                    columns.RelativeColumn(); // Price
                                    columns.RelativeColumn(); // Size
                                });

                                // Add table headers
                                table.Cell().LabelCell("Name");
                                table.Cell().LabelCell("Surname");
                                table.Cell().LabelCell("Age");
                                table.Cell().LabelCell("Clothing Name");
                                table.Cell().LabelCell("Price");
                                table.Cell().LabelCell("Size");

                                // Add data rows for the group
                                foreach (var item in group)
                                {
                                    table.Cell().ValueCell(item.StudentName);
                                    table.Cell().ValueCell(item.StudentSurname);
                                    table.Cell().ValueCell(item.Age?.ToString() ?? "N/A");
                                    table.Cell().ValueCell(item.ClothingName);
                                    table.Cell().ValueCell(item.Price?.ToString("C") ?? "N/A");
                                    table.Cell().ValueCell($"{item.Size} ({item.SizeMeasurement})");
                                }
                            });

                            // Add spacing after each group
                            column.Item().PaddingVertical(10);
                        }
                    });
                });
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);
            using var streamRef = new DotNetStreamReference(memoryStream);
            await _js.InvokeVoidAsync("exportPDF", "test.pdf", streamRef);

        }


        private async Task DownloadGroupByStudent(List<CombinedDto> itemsToExport)
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

                            // Group items by student within the grade
                            var groupedByStudent = gradeGroup
                                .GroupBy(item => new { item.StudentId, item.StudentName, item.StudentSurname })
                                .OrderBy(group => group.Key.StudentName)
                                .ThenBy(group => group.Key.StudentSurname);

                            foreach (var studentGroup in groupedByStudent)
                            {
                                // Add student header
                                var studentName = $"{studentGroup.Key.StudentName} {studentGroup.Key.StudentSurname}";
                                column.Item().Text($"{studentName}").FontSize(14).Bold();

                                // Add table for the student's clothing details
                                column.Item().Table(table =>
                                {
                                    // Define table columns
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(); // Clothing Name
                                        columns.RelativeColumn(); // Price
                                        columns.RelativeColumn(); // Size
                                        columns.RelativeColumn(); // Size Measurement
                                    });

                                    // Add table headers
                                    table.Cell().LabelCell("Clothing Name");
                                    table.Cell().LabelCell("Price");
                                    table.Cell().LabelCell("Size");
                                    table.Cell().LabelCell("Size Measurement");

                                    // Add data rows for the student's clothing
                                    foreach (var item in studentGroup)
                                    {
                                        table.Cell().ValueCell(item.ClothingName);
                                        table.Cell().ValueCell(item.Price?.ToString("C") ?? "N/A");
                                        table.Cell().ValueCell(item.Size);
                                        table.Cell().ValueCell(item.SizeMeasurement);
                                    }
                                });

                                // Add spacing after each student
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
            await _js.InvokeVoidAsync("exportPDF", "StudentClothingDetailsByGrade.pdf", streamRef);
        }


        private async Task DownloadGroupByGradeAndClothing(List<CombinedDto> itemsToExport)
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
                                column.Item().Text($"{clothingGroup.Key}").FontSize(14).Bold();

                                // Add table for the clothing details
                                column.Item().Table(table =>
                                {
                                    // Define table columns
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(); // Student Name
                                        columns.RelativeColumn(); // Student Surname
                                        columns.RelativeColumn(); // Price
                                        columns.RelativeColumn(); // Size
                                        columns.RelativeColumn(); // Size Measurement
                                    });

                                    // Add table headers
                                    table.Cell().LabelCell("Student Name");
                                    table.Cell().LabelCell("Student Surname");
                                    table.Cell().LabelCell("Price");
                                    table.Cell().LabelCell("Size");
                                    table.Cell().LabelCell("Size Measurement");

                                    // Add data rows for the clothing
                                    foreach (var item in clothingGroup)
                                    {
                                        table.Cell().ValueCell(item.StudentName);
                                        table.Cell().ValueCell(item.StudentSurname);
                                        table.Cell().ValueCell(item.Price?.ToString("C") ?? "N/A");
                                        table.Cell().ValueCell(item.Size);
                                        table.Cell().ValueCell(item.SizeMeasurement);
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
            await _js.InvokeVoidAsync("exportPDF", "GradeAndClothingDetails.pdf", streamRef);
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
                                column.Item().Text($"{clothingGroup.Key}").FontSize(14).Bold();

                                // Group items by size within the clothing group
                                var groupedBySize = clothingGroup
                                    .GroupBy(item => $"{item.Size}".Trim())
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
