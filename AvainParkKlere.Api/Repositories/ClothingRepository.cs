using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;

namespace AvainParkKlere.Api.Repositories
{
    public class ClothingRepository : GenericRepository<Clothing>, IClothingRepository
    {
        public ClothingRepository(AvianParkDbContext ApDbContext) : base(ApDbContext)
        {

        }
    }
   
}
