using Microsoft.Identity.Client;

namespace AvainParkKlere.Api.EntityFrameworkCore.Entities
{
    public class Clothing
    {
        public int Id { get; set; }
        public string Name { get; set; }   // shirt, jersey, pants, socks, shoes  
        public decimal? Price { get; set; }  
        public string SizeMeasurement { get; set; } 
    }
}
