using AvianParkKlere.Contracts.Dtos.Student;

namespace AvianParkKlere.ServerUser.Models
{
    public class StudentSelection
    {
        public int ClothingId { get; set; } 
        public StudentGetDto Student { get; set; }
        public string Size { get; set; } = "Not Specified"; // Default value Not Specified
        public bool Selected { get; set; }  
    }
}
