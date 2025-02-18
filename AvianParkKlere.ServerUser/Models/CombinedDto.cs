namespace AvianParkKlere.ServerUser.Models
{
    public class CombinedDto
    {
        public int StudentId { get; set; }  
        public int ClothingId { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public int? Age { get; set; }
        public int? Grade { get; set; }
        public string ClothingName { get; set; }
        public decimal? Price { get; set; }  
        public string Size { get; set; }    
        public string SizeMeasurement { get; set; } 
    }
}
