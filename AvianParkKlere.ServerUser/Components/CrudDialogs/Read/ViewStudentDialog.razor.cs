using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Constants;
using AvianParkKlere.ServerUser.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Read
{
    public partial class ViewStudentDialog
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public StudentGetDto Student { get; set; }  

        List<ClothingStudentClothingComposite> ClothesList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetClothesList();
        }


        private async Task GetClothesList()
        {
            ClothesList = new();
            var studentClothing = await _apiService.GetClothingForStudent(Student.Id);

            foreach (var item in studentClothing)
            {
                var clothing = await _apiService.GetClothing(item.ClothingId);
                if (clothing == null)
                {
                    //show error snackbar
                    return;
                }

                var composite = new ClothingStudentClothingComposite
                {
                    Name = clothing.Name,
                    Size = item.Size,
                    Price = clothing.Price
                };
                ClothesList.Add(composite);
            }
        }


        private async Task DownloadPdf(List<ClothingStudentClothingComposite> itemsToExport, string filename)
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
                            columns.RelativeColumn(); // Size
                            columns.RelativeColumn(); // Price
                        });

                        // Add table headers
                        table.Cell().LabelCell("Clothing");
                        table.Cell().LabelCell("Size");
                        table.Cell().LabelCell("Price");

                        // Add data rows
                        foreach (var item in itemsToExport)
                        {
                            table.Cell().ValueCell(item.Name);
                            table.Cell().ValueCell(item.Size);
                            table.Cell().ValueCell($"R {item.Price:F2}");
                        }
                    });
                });
            }).GeneratePdf(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);
            using var streamRef = new DotNetStreamReference(memoryStream);
            await _js.InvokeVoidAsync("exportPDF", filename + ".pdf", streamRef);
        }

    }
}
