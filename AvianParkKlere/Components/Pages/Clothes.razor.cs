using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;

namespace AvianParkKlere.Components.Pages
{
    public partial class Clothes
    {
        GenericDataGrid<ClothingGetDto, ClothingPostDto, ClothingPutDto> Table;
        List<ClothingGetDto> ClothingList = new();
    }
}
