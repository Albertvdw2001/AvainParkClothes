using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;

namespace AvainParkKlere.Api.Repositories
{
    public class StudentClothingRepository : GenericRepository<StudentClothing>, IStudentClothingRepository
    {
        public StudentClothingRepository(AvianParkDbContext ApDbContext) : base(ApDbContext)
        {
        }
    }
}
