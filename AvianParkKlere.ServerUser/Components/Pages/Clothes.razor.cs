using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.ServerUser.Components.Shared;

namespace AvianParkKlere.ServerUser.Components.Pages
{
    public partial class Clothes
    {
        GenericDataGrid<ClothingGetDto, ClothingPostDto, ClothingPutDto> Table;
        List<ClothingGetDto> ClothingList = new();
    }
}
