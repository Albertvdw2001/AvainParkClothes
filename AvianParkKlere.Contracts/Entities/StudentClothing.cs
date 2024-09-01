
namespace AvainParkKlere.Api.EntityFrameworkCore.Entities
{
    public class StudentClothing
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClothingId { get; set; } 
        public string Size { get; set; }    
    }
}
