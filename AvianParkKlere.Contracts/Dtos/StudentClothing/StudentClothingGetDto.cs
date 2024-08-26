namespace AvianParkKlere.Contracts.Dtos.StudentClothing
{
    public class StudentClothingGetDto : StudentClothingBaseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClothingId { get; set; } 
    }
}
