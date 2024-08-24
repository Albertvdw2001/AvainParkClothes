using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;

namespace AvainParkKlere.Api.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AvianParkDbContext ApDbContext) : base(ApDbContext)
        {
        } 
    }
}
