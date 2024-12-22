using AvianParkKlere.Contracts.Dtos.Clothing;

namespace AvianParkKlere.ServerUser.Models
{
    public class ClothesSelection
    {
        public int StudentId { get; set; }
        public ClothingGetDto Clothing { get; set; }    
        public string Size { get; set; }
        public bool Selected { get; set; }
    }
}
