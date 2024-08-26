namespace AvianParkKlere.Contracts.Dtos.Clothing
{
    public class ClothingBaseDto
    {
        public string Name { get; set; }   // shirt, jersey, pants, socks, shoes  
        public decimal? Price { get; set; }
        public string SizeMeasurement { get; set; }
    }
}
