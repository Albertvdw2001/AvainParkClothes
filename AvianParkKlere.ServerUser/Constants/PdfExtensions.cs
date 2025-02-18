using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AvianParkKlere.ServerUser.Constants
{
    public static class PdfExtensions
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border(1)
                .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
                .Padding(5);
        }

        public static void LabelCell(this IContainer container, string text)
        {
            container.Cell(true).Text(text).FontSize(8);
        }

        public static void ValueCell(this IContainer container, string text)
        {
            container.Cell(false).Text(text).FontSize(8); // Smaller font size for value cells
        }
    }
}
