using AvianParkKlere.Contracts.Dtos.Student;

namespace AvianParkKlere.Components.Pages
{
    public partial class Students
    {
        GenericDataGrid<StudentGetDto, StudentPostDto, StudentPutDto> Table;
        List<StudentGetDto> StudentList = new();
    }
}
